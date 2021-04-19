using System;
using System.Linq;
using Windows.Admin.Domain;

namespace Windows.Admin.Infrastructure.EFCore.Extensions
{
    public static class ModuleExtension
    {
        /// <summary>
        /// 根据moduleIds获取Module的IQueryable
        /// </summary>
        /// <param name="query"></param>
        /// <param name="moduleIds"></param>
        /// <returns></returns>
        public static IQueryable<Module> WhereByModuleIds(this IQueryable<Module> query, params int [] moduleIds)
        {
            if(moduleIds !=null)
            {
                query = query.Where(x => moduleIds.Contains(x.Id));
            }
            return query;
        }
        /// <summary>
        /// 根据模块名称查找模块
        /// </summary>
        /// <param name="query"></param>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        public static IQueryable<Module> WhereByModuleName(this IQueryable<Module> query,string moduleName)
        {
            return query.Where(x => x.Name == moduleName);
        }
        /// <summary>
        /// 是否已存在模块存在
        /// </summary>
        /// <param name="query"></param>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        public static bool IsExist(this IQueryable<Module> query, string moduleName)
        {
            query = WhereByModuleName(query, moduleName);
            return query.Count() > 0;
            
        }
        /// <summary>
        /// 是否已存在模块存在
        /// </summary>
        /// <param name="query"></param>
        /// <param name="moduleName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IsExist(this IQueryable<Module> query, string moduleName, int id)
        {
            query = WhereByModuleName(query, moduleName).Where(x => x.Id != id);
            return query.Count() > 0;
        }
    }
}
