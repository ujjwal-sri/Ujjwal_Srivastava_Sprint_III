using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Ujjwal_Srivastava_SprintIII;
using ProjectRepository.Models;
using Xunit;
using System.Net.Http;

namespace TestProject
{
    public class UserTests : IClassFixture<TestClient>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        public UserTests(TestClient factory)
        {
            _factory = factory;
        }


        [Theory]
        [Trait("Category", "GetUser")]
        [InlineData("/getuser/12323")]
        [InlineData("/getuser/22121")]
        public async Task GetUserByIdNotExisting(string url)
        {
            var testClient = _factory.CreateClient();
            var acttualRes = await testClient.GetAsync(url);
            Assert.Equal(HttpStatusCode.BadRequest, acttualRes.StatusCode);
        }

        [Theory]
        [Trait("Category", "GetUser")]
        [InlineData("/getuser/1")]
        [InlineData("/getuser/2")]
        public async Task GetUserByAlreadyExistingId(string url)
        {
            var testClient = _factory.CreateClient();
            var actualRes = await testClient.GetAsync(url);
            Assert.NotNull(actualRes);
            Assert.Equal("application/json; charset=utf-8", actualRes.Content.Headers.ContentType.ToString());
        }


        [Theory]
        [Trait("Category", "GetUser")]
        [InlineData("/getuser")]
        public async Task GetAllUsers(string url)
        {
            var testClient = _factory.CreateClient();
            var actualRes = await testClient.GetAsync(url);
            Assert.Equal("application/json; charset=utf-8", actualRes.Content.Headers.ContentType.ToString());
        }


        [Theory]
        [Trait("Category", "CreateUser")]
        [InlineData("/createuser")]
        public async Task CreateAFreshUser(string url)
        {
            var testClient = _factory.CreateClient();
            User user = new User { ID = 2222, FirstName = "Bob", SecondName = "Mark", Email = "bob.mark@test.com", Password = "Password1" };
            var actualRes = await testClient.PostAsync(url, (HttpContent)user);
            Assert.Equal(HttpStatusCode.OK, actualRes.StatusCode);
            Assert.Equal("text/plain; charset=utf-8", actualRes.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [Trait("Category", "CreateUser")]
        [InlineData("/createuser")]
        public async Task CreateAUserwithAlreadyExistingId(string url)
        {
            var testClient = _factory.CreateClient();
            User user = new User { ID = 1, FirstName = "Bob", SecondName = "Mark", Email = "bob.mark@test.com", Password = "Password1" };
            var actualRes = await testClient.PostAsync(url, (HttpContent)user);
            Assert.Equal(HttpStatusCode.BadRequest, actualRes.StatusCode);
        }

        [Theory]
        [Trait("Category", "UpdateUser")]
        [InlineData("/updateuser")]
        public async Task UpdateAUserNotExisting(string url)
        {
            var testClient = _factory.CreateClient();
            User user = new User { ID = 900, FirstName = "Bob", SecondName = "Mark", Email = "bob.mark@test.com", Password = "Password1" };
            var actualRes = await testClient.PutAsync(url, (HttpContent)user);
            Assert.Equal(HttpStatusCode.BadRequest, actualRes.StatusCode);
        }

        [Theory]
        [Trait("Category", "UpdateUser")]
        [InlineData("/updateuser")]
        public async Task UpdateAUserExisting(string url)
        {
            var testClient = _factory.CreateClient();
            User user = new User { ID = 1, FirstName = "Bob", SecondName = "Mark", Email = "bob.mark@test.com", Password = "Password1" };
            var actualRes = await testClient.PutAsync(url, (HttpContent)user);
            Assert.Equal(HttpStatusCode.OK, actualRes.StatusCode);
        }

        [Theory]
        [Trait("Category","ValidateUser")]
        [InlineData("/loginuser/Naman/Pass@1")]
        public async Task TestWithWrongUsernamePassword(string url)
        {
            var testClient = _factory.CreateClient();
            var actualRes = await testClient.GetAsync(url);
            Assert.Equal(HttpStatusCode.BadRequest, actualRes.StatusCode);
        }
    }
}
