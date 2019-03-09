using System.Net.Http;
using System.Threading.Tasks;

namespace Mariowski.Common.Extensions
{
    public static class HttpContentExtensions
    {
        #region ReadAsAsync

        /// <summary>
        /// Serialize the HTTP content to a <typeparamref name="T"/> as an asynchronous operation.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task<T> ReadAsAsync<T>(this HttpContent @this)
        {
            string contentAsString = await @this.ReadAsStringAsync();
            return contentAsString.TryDeserialize<T>();
        }

        #endregion
    }
}