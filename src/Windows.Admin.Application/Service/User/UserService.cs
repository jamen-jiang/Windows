using AutoMapper;
using Jyz.Domain;
using Jyz.Domain.Enums;
using Jyz.Infrastructure;
using Jyz.Infrastructure.Data;
using Jyz.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Windows.Admin.Application
{
    public class UserService : BaseService,IUserService
    {
        private readonly IMapper _mapper;
        private readonly ICache _cache;
        public UserService(IMapper mapper, ICache cache)
        {
            _mapper = mapper;
            _cache = cache;
        }
        /// <summary>
        /// 登录(返回token)
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task<LoginResponse> Login(LoginRequest info)
        {
            using (var db = base.NewDB())
            {
                User user =  await db.User.FirstOrDefaultAsync(x => x.UserName == info.UserName && x.PassWord == info.PassWord);
                if (user == null)
                    throw new ApiException("用户名或密码错误!");
                Token ts = new Token();
                ts.UserId = user.Id;
                ts.UserName = user.Name;
                string token = JwtUtils.SetJwtEncode(ts);
                LoginResponse response = new LoginResponse();
                response.Token = token;
                return response;
            }
        }
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="info"></param>
        /// <param name="isEnable"></param>
        /// <returns></returns>
        public async Task<PageResponse<UserResponse>> Query(PageRequest<UserRequest> info)
        {
            using (var db = NewDB())
            {
                PageResponse<UserResponse> model = new PageResponse<UserResponse>();
                var query = db.User.AsNoTracking().Where(x => x.Id != AppSetting.SystemConfig.Admin.ToGuid());
                var organizations = await db.Organization.AsNoTracking().ToListAsync();
                var organizationDtos = _mapper.Map<List<OrganizationResponse>>(organizations);
                if (!info.Query.OrganizationId.IsEmpty())
                {
                    List<object> ids = new List<object>();
                    GetCurrentAndChildrenIds(organizationDtos, ids, info.Query.OrganizationId.ToString(), true);
                    query = query.GetByOrganizationIds(ids.Select(s => s.ToGuid()).ToArray());
                }
                if (!info.Query.Name.IsNullOrEmpty())
                    query = query.Where(x => x.Name.Contains(info.Query.Name));
                if (!info.Query.UserName.IsNullOrEmpty())
                    query = query.Where(x => x.UserName.Contains(info.Query.UserName));
                if (info.Query.CreatedOnStart != null)
                    query = query.Where(x => x.CreatedOn >= info.Query.CreatedOnStart);
                if (info.Query.CreatedOnEnd != null)
                    query = query.Where(x => x.CreatedOn <= info.Query.CreatedOnEnd);

                int totalCount = await query.CountAsync();
                List<User> list = await query.OrderBy(x => x.CreatedOn).Paging(info.PageIndex, info.PageSize).Include(x => x.Organization_User).ToListAsync();
                User user = new User();
                model.PageIndex = info.PageIndex;
                model.PageSize = info.PageSize;
                model.TotalCount = totalCount;
                model.List = _mapper.Map<List<UserResponse>>(list);
                foreach (var l in model.List)
                {
                    if (l.OrganizationIds == null)
                        continue;
                    foreach (var organizationId in l.OrganizationIds)
                    {
                        string organizationName = "";
                        List<OrganizationResponse> outList = new List<OrganizationResponse>();
                        var organization = organizationDtos.FirstOrDefault(x => x.Id.ToGuid() == organizationId);
                        GetCurrentAndParents(organizationDtos, outList, organization);
                        foreach (var obj in outList)
                        {
                            organizationName = obj.Name + "-" + organizationName;
                        }
                        if (organizationName.Length > 0)
                            l.OrganizationNames.Add(organizationName.Trim('-'));
                    }
                }
                return model;
            }
        }
        /// <summary>
        /// 根据角色Id获取用户列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<List<UserResponse>> GetRoleUsers(Guid roleId)
        {
            using (var db = NewDB())
            {
                var users = await (from a in db.User
                                   join b in db.Role_User on a.Id equals b.UserId
                                   where b.RoleId == roleId
                                   select a).ToListAsync();
                return _mapper.Map<List<UserResponse>>(users);
            }
        }
        /// <summary>
        /// 根据id获取user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserResponse> Detail(Guid id)
        {
            using (var db = NewDB())
            {
                var model = await db.User.Include(x => x.Organization_User).Include(x => x.Role_User).FindByIdAsync(id);
                //var organization = await db.Organization.GetByUserId(id).Select(s=>s.Id).ToListAsync();
                var dto = _mapper.Map<UserResponse>(model);
                var organizations = await db.Organization.AsNoTracking().ToListAsync();
                var organizationDtos = _mapper.Map<List<OrganizationResponse>>(organizations);
                var roles = await db.Role.Where(x => dto.RoleIds.Contains(x.Id)).ToListAsync();
                dto.RoleNames = roles.Select(s => s.Name).ToList();
                foreach (var organizationId in dto.OrganizationIds)
                {
                    string organizationName = "";
                    List<OrganizationResponse> outList = new List<OrganizationResponse>();
                    var organization = organizationDtos.FirstOrDefault(x => x.Id.ToGuid() == organizationId);
                    GetCurrentAndParents(organizationDtos, outList, organization);
                    foreach (var obj in outList)
                    {
                        organizationName = obj.Name + "-" + organizationName;
                    }
                    if (organizationName.Length > 0)
                        dto.OrganizationNames.Add(organizationName.Trim('-'));
                }
                return dto;
            }
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task Add(UserAddRequest info)
        {
            using (var db = NewDB())
            {
                User user = _mapper.Map<User>(info.User);
                BeforeAdd(user);
                //密码暂写死
                user.PassWord = user.UserName;
                await db.AddAsync(user);
                await db.SaveChangesAsync();
                await SetOtherInfo(db, user.Id, info.User.OrganizationIds, info.ModuleIds, info.OperateIds, info.RoleIds);
                await db.SaveChangesAsync();
            }
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task Modify(UserModifyRequest info)
        {
            using (var db = NewDB())
            {
                await db.ExecSqlNoQuery("delete Organization_User where UserId=@UserId", new SqlParameter("UserId", info.Id));
                await db.ExecSqlNoQuery("delete Role_User where UserId=@UserId", new SqlParameter("UserId",info.Id));
                await db.ExecSqlNoQuery("delete Privilege where MasterValue=@MasterValue", new SqlParameter("MasterValue", info.Id));
                var user = await db.User.FindByIdAsync(info.Id);
                _mapper.Map(info.User, user);
                BeforeModify(user);
                await SetOtherInfo(db, info.Id, info.User.OrganizationIds, info.ModuleIds, info.OperateIds, info.RoleIds);
                await db.SaveChangesAsync();
            }
        }
        /// <summary>
        /// 更新个人信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task ModifyProfile(ProfileRequest info)
        {
            using (var db = NewDB())
            {
                var user = await db.User.FindByIdAsync(info.Id);
                _mapper.Map(info, user);
                BeforeModify(user);
                await db.SaveChangesAsync();
            }
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task ChangePassWord(PassWordRequest info)
        {
            using (var db = NewDB())
            {
                var user = await db.User.FindByIdAsync(CurrentUser.UserId);
                if (info.OldPassWord != user.PassWord)
                    throw new ApiException("旧密码不正确!");
                else if(info.NewPassWord!= info.NewPassWordConfirm)
                    throw new ApiException("两次输入密码不一致!");
                user.PassWord = info.NewPassWord;
                BeforeModify(user);
                await db.SaveChangesAsync();
            }

        }
        /// <summary>
        /// 设置用户其他关联表信息
        /// </summary>
        /// <param name="db"></param>
        /// <param name="userId"></param>
        /// <param name="moduleIds"></param>
        /// <param name="operateIds"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        private async Task SetOtherInfo(JyzContext db,Guid userId,List<Guid> organizationIds, List<Guid> moduleIds, List<Guid> operateIds, List<Guid> roleIds)
        {
            foreach (Guid id in organizationIds)
            {
                Organization_User ou = new Organization_User()
                {
                    UserId = userId,
                    OrganizationId = id
                };
                await db.AddAsync(ou);
            }
            foreach (Guid id in moduleIds)
            {
                Privilege privilege = new Privilege(MasterEnum.User, userId, AccessEnum.Module, id);
                await db.AddAsync(privilege);
            }
            foreach (Guid id in operateIds)
            {
                Privilege privilege = new Privilege(MasterEnum.User, userId, AccessEnum.Operate, id);
                await db.AddAsync(privilege);
            }
            foreach (Guid id in roleIds)
            {
                Role_User model = new Role_User();
                model.UserId = userId;
                model.RoleId = id;
                await db.AddAsync(model);
            }
        }
    }
}
