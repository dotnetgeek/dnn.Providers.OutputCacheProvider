/*
The MIT License(MIT)

Copyright(c) 2015 Daniel Müller

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

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