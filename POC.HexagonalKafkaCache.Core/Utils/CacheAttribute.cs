namespace POC.HexagonalKafkaCache.Core.Utils
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class CacheAttribute : Attribute
    {
        public int Seconds { get; set; } = 30;
        public string? CacheKey { get; set; } = null;
    }
}
