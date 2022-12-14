using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using NetWeightCalculator.Test.Exceptions;

namespace NetWeightCalculator.Test.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<T?> ExtractContent<T>(this HttpResponseMessage httpResponse)
        {
            await httpResponse.EnsureStatusCode();

            if (httpResponse.StatusCode == HttpStatusCode.NoContent)
                return default;

            var contentString = await httpResponse.Content.ReadAsStringAsync();

            var content = contentString.Deserialize<T>();

            return content;
        }

        public static async Task EnsureStatusCode(this HttpResponseMessage response, HttpStatusCode? statusCode = null)
        {
            if (statusCode.HasValue && response.StatusCode == statusCode) return;

            if (new[] { HttpStatusCode.OK, HttpStatusCode.NoContent, HttpStatusCode.Created }.Contains(response.StatusCode))
                return;

            var content = await response.Content.ReadAsStringAsync();

            throw new HttpResponseMessageStatusCodeInvalidException(statusCode, response.StatusCode, content);
        }
        
        private static TResult? Deserialize<TResult>(this string jsonContent) =>
            JsonSerializer.Deserialize<TResult>(jsonContent, AssignDefaultOptions(new JsonSerializerOptions()));

        private static JsonSerializerOptions AssignDefaultOptions(JsonSerializerOptions options)
        {
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

            return options;
        }
    }
}

