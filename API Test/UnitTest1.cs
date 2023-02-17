
using System.Text.Json;
using System.Text;
using Xunit.Abstractions;
using FluentAssertions.Execution;
using FluentAssertions;
using Xunit.Sdk;
using static API_Test.PirateSpeakResponse;
using static API_Test.JsonResponseModel;

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
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);

            var response = await client.GetAsync("about/stats");
            var content = await response.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<SteamResponseModel>(content);
            var usersActive = int.Parse(data.users_online, System.Globalization.NumberStyles.AllowThousands);



            using (new AssertionScope())
            {
                response.IsSuccessStatusCode.Should().BeTrue();
                usersActive.Should().BeGreaterThan(0);

            }
        }

        //This is a PUT request test for Pirate Speak!
        //NOTE: Cannot attempt too many requests or locks out for an hour!
        [Fact]
        public async Task PirateSpeakTest()
        {
            string baseUrl = "https://api.funtranslations.com/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);

            PiratePostModel postModel = new PiratePostModel()
            {
                text = "Hello, I want to travel the ocean."
            };


            var serialize = System.Text.Json.JsonSerializer.Serialize(postModel);
            var response = await client.PostAsync("translate/pirate", new StringContent(serialize, encoding: System.Text.Encoding.UTF8, "application/json"));

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseAsModel = System.Text.Json.JsonSerializer.Deserialize<Root>(responseContent);

            using (new AssertionScope())
            {
                response.IsSuccessStatusCode.Should().BeTrue();
                responseAsModel.contents.text.Should().Be("Hello, I want to travel the ocean.");
                responseAsModel.contents.translated.Should().Be("Ahoy, I want t' travel th' briny deep.");
                // this will check if the translated line posts correctly after the response content is deserialized through
                // the httpclient

                //Note :cannot make too many requests or else locks out for at least an hour!
            }

        }

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

            using (new AssertionScope())
            {
                response.IsSuccessStatusCode.Should().BeFalse();
                respondModel.id.Should().Be(0);
                respondModel.token.Should().BeNullOrEmpty();
            }

        }

        // This is a post test with the body
        [Fact]
        public async Task ApiRequestBody()
        {
            string baseUrl = "https://reqres.in/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);

            RegisterUser registerUser = new RegisterUser()
            {
                email = "charles.morris@reqres.in",
                password = "Pistol"
            };
            var serialized = System.Text.Json.JsonSerializer.Serialize(registerUser);

            var response = await client.PostAsync("/api/register", new StringContent(serialized,
                encoding: System.Text.Encoding.UTF8, "application/json"));
            
            var content = await response.Content.ReadAsStringAsync();
            var responseModel = System.Text.Json.JsonSerializer.Deserialize<RegisterUserToken>(content);

            using (new AssertionScope())
            {
                response.IsSuccessStatusCode.Should().BeTrue();
                responseModel.id.Should().Be(5);
                responseModel.token.Should().NotBeNullOrEmpty();

            };
        }

        // This tests the Post 
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

            using (new AssertionScope())
            {
                response.IsSuccessStatusCode.Should().BeTrue();
                data.id.Should().Be(101); //it should be the 101st post entry
                data.title.Should().Be("Mario Bros");
                data.body.Should().Be("Mushroom");
                data.userId.Should().Be(1); // as shown on the json data, every userId should all be just 1
            }
        }

        // This test demonstrates the delete user request
        [Fact]
        public async Task UserDelete()
        {
            HttpClient client = new()
            {
                BaseAddress = new Uri("https://reqres.in/")
            };

            var result = await client.DeleteAsync("/api/users/2");
            var responseContent = await result.Content.ReadAsStringAsync();

            responseContent.Should().BeNullOrEmpty();
        }

        //This test will test the Patch jsonplaceholder
        // the result should replace/update the body with Bowser Castle
        [Fact]
        public async Task JsonPatch()
        {
            string baseUrl = "https://jsonplaceholder.typicode.com/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);

            using StringContent jContent = new(
                JsonSerializer.Serialize(new
                {
                    body = "Bowser Castle",
                }),
                Encoding.UTF8, "application/json"
                );

            var response = await client.PatchAsync("posts/2", jContent);
            var content = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<Post>(content);

            using (new AssertionScope())
            {
                response.IsSuccessStatusCode.Should().BeTrue();
                data.id.Should().Be(2);
                data.title.Should().Be("qui est esse");
                data.body.Should().Be("Bowser Castle");
                data.userId.Should().Be(1);
            }
        }

        // This is a negative test that will lead to a "404 Page Not Found)
        [Fact]
        public async Task NegativeTest()
        {
            string baseUrl = "https://reqres.in/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);

            var response = await client.GetAsync("api/unknown/23");
            var content = await response.Content.ReadAsByteArrayAsync();
            var data = JsonSerializer.Deserialize<RegisterUserToken>(content);

            using (new AssertionScope())
            {
                response.IsSuccessStatusCode.Should().BeFalse();
            }
            
        }

        [Fact]
        public async Task ApiParams()
        {
            string baseurl = "https://jsonplaceholder.typicode.com/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseurl);

            var response = await client.GetAsync("comments?postId=5");
            var content = await response.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize <Comment[]>(content);

            using (new AssertionScope())
            {
                response.IsSuccessStatusCode.Should().BeTrue();
                data.Length.Should().Be(5);
                data.First().email.Should().Be("Noemie@marques.me");
                data.Last().email.Should().Be("Isaias_Kuhic@jarrett.net");
            }
        }
    }
}