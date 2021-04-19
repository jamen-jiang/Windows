using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jyz.Infrastructure
{
    /// <summary>
    /// 缓存接口
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// 检查给定 key 是否存在
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        bool Exists(string key);

        /// <summary>
        /// 获取指定 key 的值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        string Get(string key);

        /// <summary>
        /// 获取指定 key 的值
        /// </summary>
        /// <typeparam name="T">byte[] 或其他类型</typeparam>
        /// <param name="key">键</param>
        /// <returns></returns>
        T Get<T>(string key) where T : class;

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiresIn"></param>
        /// <param name="isSliding"></param>
        /// <returns></returns>
        void Add(string key, object value, TimeSpan? expiresIn = null, bool isSliding = false);

        /// <summary>
        /// 用于在 key 存在时删除 key
        /// </summary>
        /// <param name="key">键</param>
        void Remove(string key);

        /// <summary>
        /// 全部清除
        /// </summary>
        void Clear();
    }
}
