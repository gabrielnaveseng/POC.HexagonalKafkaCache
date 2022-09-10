using Castle.DynamicProxy;
using Microsoft.Extensions.Caching.Memory;

namespace POC.HexagonalKafkaCache.Core.Utils
{
    public class CacheInterceptor : IInterceptor
    {
        private IMemoryCache _memoryCache;
        public CacheInterceptor(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void Intercept(IInvocation invocation)
        {
            if (invocation.MethodInvocationTarget.GetCustomAttributes(typeof(CacheAttribute), false).FirstOrDefault() 
                is CacheAttribute cacheAttribute)
            {
                var cacheKey = cacheAttribute.CacheKey;
                if (cacheKey is null)
                    cacheKey = GenerateCacheKey(invocation.Method.Name, invocation.Arguments);

                if (_memoryCache.TryGetValue(cacheKey, out object value))
                    invocation.ReturnValue = value;
                else
                {
                    invocation.Proceed();
                    var options = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(cacheAttribute.Seconds)
                    };
                    _memoryCache.Set(cacheKey, invocation.ReturnValue, options);
                }
            }
            else if (invocation.MethodInvocationTarget.GetCustomAttributes(typeof(DeleteCacheAttribute), false).FirstOrDefault()
                is DeleteCacheAttribute deleteCacheAttribute)
            {
                var cacheKey = deleteCacheAttribute.CacheKey;
                
                if (cacheKey is null)
                    cacheKey = GenerateCacheKey(invocation.Method.Name, invocation.Arguments);
                
                _memoryCache.Remove(cacheKey);

                invocation.Proceed();
            }
            else
            {
                invocation.Proceed();
            }
        }

        private static string GenerateCacheKey(string name, object[] arguments)
        {
            if (arguments == null || arguments.Length == 0)
                return name;
            return name + "--" +
                string.Join("--", arguments.Select(a =>
                    a == null ? "**NULL**" : a.ToString()).ToArray());
        }
    }
}
