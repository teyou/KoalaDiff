using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace KoalaDiff.Library
{
    public class CacheHelper
    {
        private static ObjectCache cache = MemoryCache.Default;

        public static bool IsEnableCache
        {
            get { return SettingsHelper.EnableCache; }
        }

        const int CACHE_LENGTH = 10;

        /// <summary>
        /// Adding Cache
        /// </summary>
        public static bool Add(string key, object value)
        {
            if (IsEnableCache)
            {
                if (cache.Get(key) != null) return true; //skip if existed
                var timespan = DateTimeOffset.Now.AddMinutes(10);
                return cache.Add(key, value, timespan);
            }
            return false;
        }

        /// <summary>
        /// Retrieve Cache object
        /// </summary>
        public static object Get(string key)
        {
            if (IsEnableCache)
            {
                return cache.Get(key);
            }
            return null;
        }

        /// <summary>
        /// Remove All Cache
        /// </summary>
        public static void RemoveAllCache()
        {
            foreach (var item in cache)
            {
                cache.Remove(item.Key);
            }
            GC.Collect();
        }

    }
}
