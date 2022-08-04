using Newtonsoft.Json;

namespace Mimirorg.Setup
{
    public static class TestExtensions
    {
        public static async Task<T> GetAndDeserialize<T>(this HttpClient client, string requestUri)
        {
            var response = await client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(result);
        }
    }
}