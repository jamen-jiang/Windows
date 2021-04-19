using AutoMapper;
using Jyz.Domain;
using Jyz.Domain.Enums;
using Jyz.Infrastructure;
using Jyz.Infrastructure.Data;
using Jyz.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Windows.Admin.Application
{
    public class PrivilegeService : BaseService, IPrivilegeService
    {
        private readonly IMapper _mapper;

        public PrivilegeService(IMapper mapper)
        {
            _mapper = mapper;
        }
        /// <summary>
        /// 获取全部url并且此用户Id是否已授权
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<OperateUrlResponse>> GetOperateUrlsByUserId(Guid userId)
        {
            using (var db = NewDB())
            {
                //全部url(controller+action)
                var operates = await db.Operate.Include(x => x.Module).AsNoTracking().ToListAsync();
                var operateUrls =   _mapper.Map<List<OperateUrlResponse>>(operates);
                if (userId == AppSetting.SystemConfig.Admin.ToGuid())
                {
                    operateUrls.ForEach(item => item.IsAuthorize = true);
                    return  operateUrls;
                }
                else
                {
                    var user = await db.User.FindByIdAsync(userId);
                    var userRoleIds = await db.Role.GetByUserId(userId).Select(s => s.Id).ToArrayAsync();
                    //var organizationRoleIds = await db.Role.GetByOrganizationId(user.OrganizationId).Select(s => s.Id).ToArrayAsync();
                    //List<Privilege> privilegeList = await db.Privilege.GetByMasterValues(userId, user.OrganizationId, userRoleIds, organizationRoleIds).ToListAsync();
                    //Guid[] operateIds = privilegeList.Where(x => x.Access == AccessEnum.Operate.ToString()).Select(s => s.AccessValue).ToArray();

                    //List<PrivilegeResponse> list = new List<PrivilegeResponse>();
                    //var authOperateUrls = operateUrls.Where(x => operateIds.Contains(x.OperateId)).ToList();
                    //foreach (var o in authOperateUrls)
                    //{
                    //    o.IsAuthorize = true;
                    //}
                    //return authOperateUrls;
                    return null;
                }
            }
        }
    }
}
