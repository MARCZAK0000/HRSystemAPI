using Microsoft.Extensions.Caching.Memory;

namespace HumanResources.Domain.Cache
{
    public static class CacheOptions
    {
        public static MemoryCacheEntryOptions DefaultOptions()
        {
            return new MemoryCacheEntryOptions() 
            {
                Size = 1024,
                AbsoluteExpiration = DateTime.Now.AddSeconds(10),
                SlidingExpiration = TimeSpan.FromSeconds(5),
            };
        }

        public static MemoryCacheEntryOptions FastOptions()
        {
            return new MemoryCacheEntryOptions()
            {
                Size = 1024,
                AbsoluteExpiration = DateTime.Now.AddSeconds(5),
                SlidingExpiration = TimeSpan.FromSeconds(2),
            };
        }
    }
}
