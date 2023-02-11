
using Newtonsoft.Json;
using Xunit.Abstractions;
using FluentAssertions.Execution;
using FluentAssertions;
using Xunit.Sdk;

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
            string baseUrl = "https://www.valvesoftware.com/";
            HttpClient client= new HttpClient();
            client.BaseAddress = new Uri(baseUrl);

            var response = await client.GetAsync("about/stats");
            var content = await response.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<ResponseModel>(content);
            var usersActive = int.Parse(data.users_active, System.Globalization.NumberStyles.AllowThousands);

            using(new AssertionScope())
            {
                response.IsSuccessStatusCode.Should().BeTrue();
                usersActive.Should().BeGreaterThan(0);
            }
        }
    }
}