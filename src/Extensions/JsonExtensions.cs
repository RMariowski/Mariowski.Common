using Newtonsoft.Json;

namespace Mariowski.Common.Extensions
{
    public static class JsonExtensions
    {
        #region Serialize

        #region To Json

        /// <summary>
        /// Serializes the specified object to a JSON string using formatting or not.
        /// Same as <see cref="Serialize"/>.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="pretty">Use pretty formatting.</param>
        /// <returns>A JSON string representation of the object.</returns>
        public static string ToJson(this object @this, bool pretty = false)
            => JsonConvert.SerializeObject(@this, pretty ? Formatting.Indented : Formatting.None);

        #endregion

        #region Serialize

        /// <summary>
        /// Serializes the specified object to a JSON string using formatting or not.
        /// Same as <see cref="ToJson"/>.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="pretty">Use pretty formatting.</param>
        /// <returns>A JSON string representation of the object.</returns>
        public static string Serialize(this object @this, bool pretty = false)
            => ToJson(@this, pretty);

        #endregion

        #endregion

        #region Deserialize

        #region Deserialize

        /// <summary>
        /// Deserializes the JSON to the specified .NET type.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The deserialized object from the JSON string.</returns>
        public static T Deserialize<T>(this string @this)
            => JsonConvert.DeserializeObject<T>(@this);

        #endregion

        #region Try Deserialize

        /// <summary>
        /// Tries deserialize the JSON to the specified .NET type.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The deserialized object from the JSON string.</returns>
        public static T TryDeserialize<T>(this string @this)
        {
            if (string.IsNullOrWhiteSpace(@this))
                return default(T);

            var result = default(T);
            try
            {
                result = Deserialize<T>(@this);
            }
            catch
            {
                // Nothing
            }

            return result;
        }

        #endregion

        #endregion
    }
}