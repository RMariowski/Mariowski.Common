using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Mariowski.Common.AspNet.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Serializes object to JSON, then converts to <see cref="T:StringContent"></see>.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="encoding">The encoding to use for the content.</param>
        /// <returns>Object as string content.</returns>
        public static StringContent ToJsonContent(this object @this, Encoding encoding = null)
        {
            string json = JsonSerializer.Serialize(@this);
            return new StringContent(json, encoding ?? Encoding.UTF8, "application/json");
        }
    }
}