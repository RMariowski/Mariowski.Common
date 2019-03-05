using System.Net.Http;
using System.Threading.Tasks;

namespace Mariowski.Common.Extensions
{
    public static class HttpContentExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns></returns>
        public static async Task<T> ReadAsAsync<T>(this HttpContent @this)
        {
            string contentAsString = await @this.ReadAsStringAsync();
            return contentAsString.TryDeserialize<T>();
        }
    }
}