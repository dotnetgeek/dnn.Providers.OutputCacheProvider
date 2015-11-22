using DotNetNuke.Services.OutputCache;
using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace dnn.Providers.OutputCacheProvider
{
    public class InMemoryCache : OutputCachingProvider
    {

        #region Protected Fields

        protected const string cacheKeyPrefix = "DNN_CACHE_OUTPUT:";

        #endregion Protected Fields

        #region Private Fields

        private static Cache runtimeCache;

        #endregion Private Fields

        #region Internal Properties

        internal static Cache Cache
        {
            get
            {
                if (runtimeCache == null)
                    runtimeCache = HttpRuntime.Cache;

                return runtimeCache;
            }
        }

        #endregion Internal Properties

        #region Public Methods

        public override int GetItemCount(int tabId)
        {
            throw new NotImplementedException();
        }

        public override byte[] GetOutput(int tabId, string cacheKey)
        {
            return (byte[])InMemoryCache.Cache[cacheKey] ?? null;
        }

        public override OutputCacheResponseFilter GetResponseFilter(int tabId, int maxVaryByCount, Stream responseFilter, string cacheKey, TimeSpan cacheDuration)
        {
            throw new NotImplementedException();
        }

        public override void Remove(int tabId)
        {
            throw new NotImplementedException();
        }

        public override void SetOutput(int tabId, string cacheKey, TimeSpan duration, byte[] output)
        {
            throw new NotImplementedException();
        }

        public override bool StreamOutput(int tabId, string cacheKey, HttpContext context)
        {
            if (InMemoryCache.Cache[cacheKey] == null)
                return false;

            context.Response.BinaryWrite(Encoding.Default.GetBytes(InMemoryCache.Cache[cacheKey].ToString()));
            return true;
        }

        #endregion Public Methods

    }
}