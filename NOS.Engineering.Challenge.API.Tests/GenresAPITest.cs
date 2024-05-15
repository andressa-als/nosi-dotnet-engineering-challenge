using Newtonsoft.Json;
using NOS.Engineering.Challenge.API.Tests.Services;
using System.Data;
using System.Text;
using static NOS.Engineering.Challenge.API.Tests.Config;

namespace NOS.Engineering.Challenge.API.Tests
{
    public class GenresAPITest
    {
        [Fact]
        public async void GetByTerm()
        {
            string requestUri = "https://localhost:7140/api/v1/Content/Search";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "term", "Action" }
            };

            UrlService.BuildURL(ref requestUri, parameters);

            string apiResponse = await UrlService.CallRequest(requestUri, TypeHttp.GET);
            var content = JsonConvert.DeserializeObject<dynamic>(apiResponse);

            Assert.NotNull(content);
        }

        [Fact]
        public async void AddGenre()
        {
            List<string> genres = new List<string>();
            var guid = await GetContentGuid();

            string requestUri = $"https://localhost:7140/api/v1/Content/{guid}/genre";
 
            using StringContent jsonContent = new(JsonConvert.SerializeObject(
                                                    new List<string>() { "New Genre", "New Genre 2" }
            ), Encoding.UTF8, "application/json");

            string apiResponse = await UrlService.CallRequest(requestUri, TypeHttp.POST, jsonContent);

            var content = JsonConvert.DeserializeObject<dynamic>(apiResponse);
            var genreList = JsonConvert.DeserializeObject<dynamic>(apiResponse)["genreList"];
            
            foreach (var g in genreList)
                genres.Add(Convert.ToString(g));

            Assert.NotNull(content);
            Assert.Equal(guid, (Guid)content.id);
            Assert.True(genres.Contains("New Genre") && genres.Contains("New Genre 2"));
        }

        private async Task<Guid> GetContentGuid() 
        {
            string requestUri = $"https://localhost:7140/api/v1/Content";

            string apiResponse = await UrlService.CallRequest(requestUri, TypeHttp.GET);
            var content = JsonConvert.DeserializeObject<dynamic>(apiResponse);

            return (Guid)content[0].id; 
        }
    }
}
