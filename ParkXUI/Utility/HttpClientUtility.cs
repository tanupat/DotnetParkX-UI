using System.Net;

namespace ParkXUI.Utility
{
    public class HttpClientUtility
    {
        private readonly HttpClient client;

        public HttpClientUtility(IConfiguration configuration)
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(configuration["Settings:ParkXAPIUrl"])
            };
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<ApiResponse> GetAsync(string uri, string token = null)
        {
            AddAuthorizationHeader(token);
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                string data = await response.Content.ReadAsStringAsync();
                return new ApiResponse(response.StatusCode, null, data);
            }
            catch (Exception ex)
            {
                return new ApiResponse(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        public async Task<ApiResponse> PostAsync<T>(string uri, T item, string token = null)
        {
            AddAuthorizationHeader(token);
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(uri, item);
                string data = await response.Content.ReadAsStringAsync();
                return new ApiResponse(response.StatusCode, null, data);
            }
            catch (Exception ex)
            {
                return new ApiResponse(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }
 
        public async Task<ApiResponse> PutAsync<T>(string uri, T item, string token = null)
        {
            AddAuthorizationHeader(token);
            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync(uri, item);
                string data = await response.Content.ReadAsStringAsync();
                return new ApiResponse(response.StatusCode, null, data);
            }
            catch (Exception ex)
            {
                return new ApiResponse(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }
 
        public async Task<ApiResponse> DeleteAsync(string uri, string token = null)
        {
            AddAuthorizationHeader(token);
            try
            {
                HttpResponseMessage response = await client.DeleteAsync(uri);
                string data = await response.Content.ReadAsStringAsync();
                return new ApiResponse(response.StatusCode, null, data);
            }
            catch (Exception ex)
            {
                return new ApiResponse(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        private void AddAuthorizationHeader(string token)
        {
            client.DefaultRequestHeaders.Authorization = null;
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}
