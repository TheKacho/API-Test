
using Newtonsoft.Json;
using Xunit.Abstractions;

namespace API_Test
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper output;

        public UnitTest1(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public async Task ApiTest()
        {
            string baseUrl = "";
            HttpClient client= new HttpClient();
            client.BaseAddress = new Uri(baseUrl);

            var response = await client.GetAsync("Insert/info/here");
            var content = await response.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<ResponseModel>(content);
        }
    }
}