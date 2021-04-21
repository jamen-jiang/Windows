using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Windows.SeedWork;

namespace Windows.Infrastructure.EFCore
{
    public static class BaseEntityExtension
    {
        /// <summary>
        /// 根据Id获取实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T FindById<T>(this IQueryable<T> query, int id) where T : Entity<int>
        {
            return query.FirstOrDefault(x => x.Id == id);
        }
        /// <summary>
        /// 根据Id获取实体(异步)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<T> FindByIdAsync<T>(this IQueryable<T> query, int id) where T : Entity<int>
        {
            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }
        /// <summary>
        /// 忽视isEnable获取全部数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public static IQueryable<T> GetAll<T>(this IQueryable<T> query) where T : Entity<int>
        {
            return query.AsNoTracking().IgnoreQueryFilters();
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IQueryable<T> Paging<T>(this IQueryable<T> query,  int pageIndex, int pageSize) where T : Entity<int>
        {
            return query.Skip(pageSize * (pageIndex - 1)).Take(pageSize);
        }
    }
}
