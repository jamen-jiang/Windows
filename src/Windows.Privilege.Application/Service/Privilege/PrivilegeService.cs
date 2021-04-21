using AutoMapper;
using Windows.Application.Shared.Service;
using Windows.Privilege.Infrastructure.EFCore;

namespace Windows.Privilege.Application
{
    public class PrivilegeService : BaseService, IPrivilegeService
    {
        private readonly IMapper _mapper;
        private readonly PrivilegeDbContext _db;
        public PrivilegeService(PrivilegeDbContext db,IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }
        /// <summary>
        /// 获取全部url并且此用户Id是否已授权
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        //public async Task<List<OperateUrlResponse>> GetOperateUrlsByUserId(int userId)
        //{
        //    using (_db)
        //    {
        //        //全部url(controller+action)
        //        var operates = await _db.Operate.Include(x => x.Module).AsNoTracking().ToListAsync();
        //        var operateUrls =   _mapper.Map<List<OperateUrlResponse>>(operates);

        //        //if (userId == AppSetting.SystemConfig.Admin.ToGuid())
        //        if (userId == 1)
        //        {
        //            operateUrls.ForEach(item => item.IsAuthorize = true);
        //            return  operateUrls;
        //        }
        //        else
        //        {
        //            var user = await _db.User.FindByIdAsync(userId);
        //            var userRoleIds = await _db.Role.GetByUserId(userId).Select(s => s.Id).ToArrayAsync();
        //            //var organizationRoleIds = await db.Role.GetByOrganizationId(user.OrganizationId).Select(s => s.Id).ToArrayAsync();
        //            //List<Privilege> privilegeList = await db.Privilege.GetByMasterValues(userId, user.OrganizationId, userRoleIds, organizationRoleIds).ToListAsync();
        //            //Guid[] operateIds = privilegeList.Where(x => x.Access == AccessEnum.Operate.ToString()).Select(s => s.AccessValue).ToArray();

        //            //List<PrivilegeResponse> list = new List<PrivilegeResponse>();
        //            //var authOperateUrls = operateUrls.Where(x => operateIds.Contains(x.OperateId)).ToList();
        //            //foreach (var o in authOperateUrls)
        //            //{
        //            //    o.IsAuthorize = true;
        //            //}
        //            //return authOperateUrls;
        //            return null;
        //        }
        //    }
        //}
    }
}
