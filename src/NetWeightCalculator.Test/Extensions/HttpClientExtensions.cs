using System.Net.Http;
using System.Threading.Tasks;

namespace NetWeightCalculator.Test.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<T?> PostContentAsync<T>(this HttpClient httpClient, string url, HttpContent postData)
        {
            var response = await httpClient.PostAsync(url, postData);

            await response.EnsureStatusCode();

            var content = await response.ExtractContent<T>();

            return content;
        }
    }
}