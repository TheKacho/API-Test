
using System.Text.Json;
using System.Text;
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

            var data = JsonSerializer.Deserialize<SteamResponseModel>(content);
            var usersActive = int.Parse(data.users_online, System.Globalization.NumberStyles.AllowThousands);

            

            using(new AssertionScope())
            {
                response.IsSuccessStatusCode.Should().BeTrue();
                usersActive.Should().BeGreaterThan(0);

            }
        }

        // This is a PUT request test for Pirate Speak!
        [Fact]
        public async Task PirateSpeakTest()
        {
            string baseUrl = "https://api.funtranslations.com/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);

            PiratePostModel postModel= new PiratePostModel();
            {
                text = "There is a snake in my ship!";
            };

            var serialize = System.Text.Json.JsonSerializer.Serialize(postModel);
            var respond = await client.PostAsync("translate/pirate", new StringContent(serialize, encoding:System.Text.Encoding.UTF8, "application/json"));


        }
        //[Fact]
        //public async Task ApiEntryNumberCount()
        //{
        //    string baseurl = "";
        //    HttpClient client= new HttpClient();
        //    client.BaseAddress = new Uri(baseurl);

        //    var response = await client.GetAsync("entry");
        //    var content = await response.Content.ReadAsStringAsync();

        //    var data = JsonSerializer.Deserialize<ResponseApiModel>(content);
        //    int count = data.count;

        //    using(new AssertionScope())
        //    {
        //        response.IsSuccessStatusCode.Should().BeTrue();
        //        count.Should().BeGreaterThan(0);
        //    }
        //}
    }
}