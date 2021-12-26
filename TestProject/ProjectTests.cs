using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Ujjwal_Srivastava_SprintIII;
using ProjectRepository.Models;
using Xunit;
using System.Net.Http;

namespace TestProject
{
    public class ProjectTests :IClassFixture<TestClient>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        public ProjectTests(TestClient factory)
        {
            _factory = factory;
        }

        [Theory]
        [Trait("Category","GetProject")]
        [InlineData("/getproject/12323")]
        [InlineData("/getproject/22121")]
        public async Task GetProjectByIdNotExisting(string url)
        {
            var testClient = _factory.CreateClient();
            var acttualRes = await testClient.GetAsync(url);
            Assert.Equal(HttpStatusCode.BadRequest, acttualRes.StatusCode);
        }

        [Theory]
        [Trait("Category", "GetProject")]
        [InlineData("/getproject/1")]
        [InlineData("/getproject/2")]
        public async Task GetProjByAlreadyExistingId(string url)
        {
            var testClient = _factory.CreateClient();
            var actualRes = await testClient.GetAsync(url);
            Assert.NotNull(actualRes);
            Assert.Equal("application/json; charset=utf-8", actualRes.Content.Headers.ContentType.ToString());
        }


        [Theory]
        [Trait("Category", "GetProject")]
        [InlineData("/getproject")]
        public async Task GetAllProjects(string url)
        {
            var testClient = _factory.CreateClient();
            var actualRes = await testClient.GetAsync(url);
            Assert.Equal("application/json; charset=utf-8", actualRes.Content.Headers.ContentType.ToString());
        }


        [Theory]
        [Trait("Category","CreateProject")]
        [InlineData("/createproject")]
        public async Task CreateAFreshProject(string url)
        {
            var testClient = _factory.CreateClient();
            Project project = new Project { ID = 2222, Name = "TestProject2222", Detail = "This is a test project", CreatedOn = new System.DateTime(2021,4,4,4,4,4) };
            var actualRes = await testClient.PostAsync(url, (HttpContent)project);
            Assert.Equal(HttpStatusCode.OK, actualRes.StatusCode);
            Assert.Equal("text/plain; charset=utf-8", actualRes.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [Trait("Category", "CreateProject")]
        [InlineData("/createproject")]
        public async Task CreateAProjectwithAlreadyExistingId(string url)
        {
            var testClient = _factory.CreateClient();
            Project project = new Project { ID = 1, Name = "TestProject1", Detail = "This is a test project", CreatedOn = new System.DateTime(2021, 4, 4, 4, 4, 4) };
            var actualRes = await testClient.PostAsync(url, (HttpContent)project);
            Assert.Equal(HttpStatusCode.BadRequest, actualRes.StatusCode);
        }

        [Theory]
        [Trait("Category","UpdateProject")]
        [InlineData("/updateproject")]
        public async Task UpdateAProjectNotExisting(string url)
        {
            var testClient = _factory.CreateClient();
            Project project = new Project { ID = 900, Name = "TestProject1", Detail = "This is a test project", CreatedOn = new System.DateTime(2021, 4, 4, 4, 4, 4)  };
            var actualRes = await testClient.PutAsync(url, (HttpContent)project);
            Assert.Equal(HttpStatusCode.BadRequest, actualRes.StatusCode);
        }

        [Theory]
        [Trait("Category", "UpdateProject")]
        [InlineData("/updateproject")]
        public async Task UpdateAProjectExisting(string url)
        {
            var testClient = _factory.CreateClient();
            Project project = new Project { ID = 1, Name = "TestProject1", Detail = "This is a test project", CreatedOn = new System.DateTime(2021, 4, 4, 4, 4, 4) };
            var actualRes = await testClient.PutAsync(url, (HttpContent)project);
            Assert.Equal(HttpStatusCode.OK, actualRes.StatusCode);
        }
    }
}
