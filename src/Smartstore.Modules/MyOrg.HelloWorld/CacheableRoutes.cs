using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smartstore.Core.OutputCache;

namespace MyOrg.HelloWorld
{
    internal sealed class CacheableRoutes : ICacheableRouteProvider
    {
        public int Order => 0;
        public IEnumerable<string> GetCacheableRoutes()
        {
            return new string[]
            {
              "vc:MyOrg.HelloWorld/HelloWorld"
            };
        }
    }
}
