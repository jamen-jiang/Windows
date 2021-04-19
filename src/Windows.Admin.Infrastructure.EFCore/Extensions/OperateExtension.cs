using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Admin.Domain;

namespace Windows.Admin.Infrastructure.EFCore.Extensions
{
    public static class OperateExtension
    {
        /// <summary>
        /// 查找是否存在此url
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static async Task<bool> HasUrl(this IQueryable<Operate> query, string controller, string action)
        {
            var obj = from a in query.Include(x => x.Module)
                      where a.Module.Controller == controller && a.Action == action
                      select a;
            return await obj.CountAsync() > 0;
        }
    }
}
