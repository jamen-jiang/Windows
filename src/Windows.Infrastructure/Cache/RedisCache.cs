using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Infrastructure.Extensions;

namespace Jyz.Infrastructure
{
    /// <summary>
    /// Redis缓存
    /// </summary>
    public class RedisCache : ICache
    {
        protected IDatabase _cache;

        private ConnectionMultiplexer _connection;

        private readonly object connectionLock = new object();

        public RedisCache()
        {
            _connection = GetConnection();
            _cache = _connection.GetDatabase();
        }

        /// <summary>
        /// 核心代码，获取连接实例
        /// 通过双if 夹lock的方式，实现单例模式
        /// </summary>
        /// <returns></returns>
        private ConnectionMultiplexer GetConnection()
        {
            //如果已经连接实例，直接返回
            if (this._connection != null && this._connection.IsConnected)
            {
                return this._connection;
            }
            //加锁，防止异步编程中，出现单例无效的问题
            lock (connectionLock)
            {
                if (this._connection != null)
                {
                    //释放redis连接
                    this._connection.Dispose();
                }
                try
                {
                    //this._connection = ConnectionMultiplexer.Connect(AppSetting.SystemConfig.RedisConnectionString);
                }
                catch (Exception)
                {
                    //throw new Exception("Redis服务未启用，请开启该服务，并且请注意端口号，本项目使用的的6319，而且我的是没有设置密码。");
                }
            }
            return this._connection;
        }

        public bool Exists(string key)
        {
            if (key.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException(nameof(key));
            }
            return _cache.KeyExists(key);
        }
        public string Get(string key)
        {
            return _cache.StringGet(key).ToString();
        }

        public T Get<T>(string key) where T : class
        {
            var value = _cache.StringGet(key);

            if (!value.HasValue)
            {
                return null;
            }
            return JsonConvert.DeserializeObject<T>(value);
        }
        public void Add(string key, object value, TimeSpan? expiresIn = null, bool isSliding = false)
        {
             _cache.StringSet(key, JsonConvert.SerializeObject(value), expiresIn);
        }

        public void Remove(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            _cache.KeyDelete(key);
        }

        public void Clear()
        {
            foreach (var endPoint in this.GetConnection().GetEndPoints())
            {
                var server = this.GetConnection().GetServer(endPoint);
                foreach (var key in server.Keys())
                {
                    _connection.GetDatabase().KeyDelete(key);
                }
            }
        }
        
        public void Dispose()
        {
            if (_connection != null)
                _connection.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
