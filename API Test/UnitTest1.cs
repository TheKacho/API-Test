
using System.Text.Json;
using System.Text;
using Xunit.Abstractions;
using FluentAssertions.Execution;
using FluentAssertions;
using Xunit.Sdk;
using static API_Test.PirateSpeakResponse;
using static API_Test.DummyResponseModel;

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
        // NOTE: Cannot attempt too many requests or locks out for an hour!
        //[Fact]
        //public async Task PirateSpeakTest()
        //{
        //    string baseUrl = "https://api.funtranslations.com/";
        //    HttpClient client = new HttpClient();
        //    client.BaseAddress = new Uri(baseUrl);

        //    PiratePostModel postModel = new PiratePostModel()
        //    {
        //        text = "Hello, I want to travel the ocean."
        //    };
            
            
        //    var serialize = System.Text.Json.JsonSerializer.Serialize(postModel);
        //    var response = await client.PostAsync("translate/pirate", new StringContent(serialize, encoding:System.Text.Encoding.UTF8, "application/json"));

        //    var responseContent = await response.Content.ReadAsStringAsync();
        //    var responseAsModel = System.Text.Json.JsonSerializer.Deserialize<Root>(responseContent);

        //    using(new AssertionScope())
        //    {
        //        response.IsSuccessStatusCode.Should().BeTrue();
        //        responseAsModel.contents.text.Should().Be("Hello, I want to travel the ocean.");
        //        responseAsModel.contents.translated.Should().Be("Ahoy, I want t' travel th' briny deep.");
        //        // this will check if the translated line posts correctly after the response content is deserialized through
        //        // the httpclient

        //        //Note :cannot make too many requests or else locks out for at least an hour!
        //    }

        //}

        [Fact]

        // Test for Create User feature, possibly a negative test also?
        public async Task CreateUserTest()
        {
            //string baseUrl = "https://reques.in";
            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri(baseUrl);
            HttpClient client = new()
            {
                BaseAddress = new("https://reqres.in/")
            };

            RegisterUser postUser = new RegisterUser()
            {
                user = "donkey.kong@reques.in",
                email = "donkey.kong@reques.in",
                password = "banana"
            };

            var serialized = System.Text.Json.JsonSerializer.Serialize(postUser);
            var response = await client.PostAsync("api/register",
                new StringContent(serialized, encoding: System.Text.Encoding.UTF8, "application/json"));

            var respondCall = await response.Content.ReadAsStringAsync();
            var respondModel = System.Text.Json.JsonSerializer.Deserialize<RegisterUserToken>(respondCall);

            using(new AssertionScope())
            {
                response.IsSuccessStatusCode.Should().BeFalse();
                respondModel.id.Should().Be(0);
                respondModel.token.Should().BeNullOrEmpty();
            }

        }

        [Fact]
        public async Task DigimonEntry()
        {
            string baseUrl = "https://digimon-api.vercel.app/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);

            var response = await client.GetAsync("api/digimon");
            //var content = await response.Content.ReadAsStringAsync();

            //var data = JsonSerializer.Deserialize<DigimonPostModel>(content);

            using (new AssertionScope())
            {
                response.IsSuccessStatusCode.Should().BeTrue();
                
            }
        }

        [Fact]
        public async Task JsonPost()
        {
            string baseUrl = "https://jsonplaceholder.typicode.com/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);

            using StringContent jContent = new(JsonSerializer.Serialize(
                new
                {
                    title = "Mario Bros",
                    body = "Mushroom",
                    userId = 1

                }),
                Encoding.UTF8, "application/json"
            );

            var response = await client.PostAsync("posts", jContent);
            var content = await response.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<Post>(content);

            using(new AssertionScope())
            {
                response.IsSuccessStatusCode.Should().BeTrue();
                data.id.Should().Be(101); //it should be the 101st post entry
                data.title.Should().Be("Mario Bros");
                data.body.Should().Be("Mushroom");
                data.userId.Should().Be(1); // as shown on the json data, every userId should all be just 1
            }
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