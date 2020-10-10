namespace BookShop.Common.Utils
{
    using System.Text.Json;

    internal static class SerializationExtensions
    {
        public static byte[] ToByteArray(this object obj)
        {
            if (obj is null)
            {
                return null;
            }

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

            return JsonSerializer.SerializeToUtf8Bytes(obj, options);
        }

        public static T FromByteArray<T>(this byte[] byteArray)
            where T : class
        {
            if (byteArray is null)
            {
                return default;
            }

            var utf8Reader = new Utf8JsonReader(byteArray);
            return JsonSerializer.Deserialize<T>(ref utf8Reader);
        }
    }
}
