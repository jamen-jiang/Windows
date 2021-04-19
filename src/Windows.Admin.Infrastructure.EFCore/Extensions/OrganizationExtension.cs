using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Admin.Domain;

namespace Windows.Admin.Infrastructure.EFCore.Extensions
{
    public static class OrganizationExtension
    {
        //public static Task<List<Guid>> GetCurrentAndChildrenIdList(this IQueryable<Organization> query,Guid id)
        //{
        //    var list = query.AsNoTracking().ToListAsync();
        //}
        public static IQueryable<Organization> GetByUserId(this IQueryable<Organization> query, int userId)
        {
            var obj = from a in query
                      from b in a.Organization_User
                      where b.UserId == userId
                      select a;
            return obj.Distinct();
        }
    }
}
