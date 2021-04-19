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
        public static Entity<T> FindById<T>(this IQueryable<Entity<T>> query, int id) where T : struct
        {
            return query.FirstOrDefault(x => x.Id.Equals(id));
        }
        /// <summary>
        /// 根据Id获取实体(异步)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<Entity<T>> FindByIdAsync<T>(this IQueryable<Entity<T>> query, int id) where T : struct
        {
            return await query.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }
        /// <summary>
        /// 忽视isEnable获取全部数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public static IQueryable<Entity<T>> GetAll<T>(this IQueryable<Entity<T>> query) where T : struct
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
        public static IQueryable<Entity<T>> Paging<T>(this IQueryable<Entity<T>> query,  int pageIndex, int pageSize) where T : struct
        {
            return query.Skip(pageSize * (pageIndex - 1)).Take(pageSize);
        }
    }
}
