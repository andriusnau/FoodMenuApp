
namespace FoodMenuApi.Api
{
    public class ApiClient
    {
        private HttpClient _client { get; set; }

        public ApiClient()
        {
            _client = new HttpClient();
        }

        public async Task<string> GetRequest(string url)
        {
            if (url == null)
            {
                throw new Exception("No endpoint specified");
            }

            using (HttpResponseMessage response = await _client.GetAsync(url))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }

                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
