using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB.Utility.Extensions
{
    public static class UriExtensions
    {
        public static void Open(this Uri url) => StaticMethods.OpenURL(url);
    }
}
