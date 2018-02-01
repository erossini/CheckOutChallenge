using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.WebApi.Helpers
{
    /// <summary>
    /// Class SecurityHelpers.
    /// </summary>
    public static class SecurityHelpers
    {
        /// <summary>
        /// Shareds the key.
        /// </summary>
        /// <returns>System.Byte[].</returns>
        public static byte[] GetSharedKey()
        {
            return Encoding.UTF8.GetBytes("H38DLSIEKD8EKDOS");
        }
    }
}
