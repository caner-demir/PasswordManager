using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace PasswordManager.Web.Utilities
{
    public class HttpRequestManager
    {
        private HttpClient _client = new HttpClient();

        public async Task<string> GetRequest(string url, string token)
        {
            if (token != "")
            {
                _client.DefaultRequestHeaders.Add("Authorization", token);
            }
            var response = await _client.GetStringAsync(url);
            return response;
        }

        public async Task<string> PostRequest(string url, Object content)
        {
            var postContent = new StringContent(JsonConvert.SerializeObject(content),
                Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(url, postContent);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            return null;
        }
    }
}
