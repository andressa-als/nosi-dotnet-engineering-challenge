using static NOS.Engineering.Challenge.API.Tests.Config;

namespace NOS.Engineering.Challenge.API.Tests.Services
{
    public static class UrlService
    {
        public static void BuildURL(ref string url, Dictionary<string, object> parameters)
        {
            foreach (var parameter in parameters)
            {
                if (parameter.Value != null)
                {
                    if (url.Contains("?"))
                        url += $"&{parameter.Key}={parameter.Value}";
                    else
                        url += $"?{parameter.Key}={parameter.Value}";
                }
            }
        }

        public static async Task<string> CallRequest(string requestUri, TypeHttp typeHttp, StringContent? contentJson = null)
        {
            try
            {
                switch (typeHttp)
                {
                    case TypeHttp.GET:
                        return await GetRequest(requestUri);
                    case TypeHttp.PUT:
                        return await PutRequest(requestUri, contentJson);
                    case TypeHttp.POST:
                        return await PostRequest(requestUri, contentJson);
                    case TypeHttp.DELETE:
                        return await DeleteRequest(requestUri);
                    case TypeHttp.PATCH:
                        return await PatchRequest(requestUri, contentJson);
                    default:
                        return String.Empty;
                }
            }
            catch
            {
                throw;
            }
        }

        private static async Task<string> GetRequest(string requestUri)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(requestUri))
                        return response.Content.ReadAsStringAsync().Result;
                }
            }
            catch
            {
                throw;
            }
        }

        private static async Task<string> PostRequest(string requestUri, StringContent contentJson)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsync(requestUri, contentJson))
                        return await response.Content.ReadAsStringAsync();
                }
            }
            catch
            {
                throw;
            }
        }

        private static async Task<string> PutRequest(string requestUri, StringContent contentJson)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PutAsync(requestUri, contentJson))
                        return await response.Content.ReadAsStringAsync();
                }
            }
            catch
            {
                throw;
            }
        }

        private static async Task<string> DeleteRequest(string requestUri)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.DeleteAsync(requestUri))
                        return await response.Content.ReadAsStringAsync();
                }
            }
            catch
            {
                throw;
            }
        }

        private static async Task<string> PatchRequest(string requestUri, StringContent contentJson)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PatchAsync(requestUri, contentJson))
                        return await response.Content.ReadAsStringAsync();
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
