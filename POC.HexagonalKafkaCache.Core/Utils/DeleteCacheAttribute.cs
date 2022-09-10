namespace POC.HexagonalKafkaCache.Core.Utils
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class DeleteCacheAttribute : Attribute
    {
        public string? CacheKey { get; set; } = null;
    }
}
