using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Ujjwal_Srivastava_SprintIII;
using ProjectRepository.Models;
using Xunit;
using System.Net.Http;

namespace TestProject
{
    public class TasksTests: IClassFixture<TestClient>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        public TasksTests(TestClient factory)
        {
            _factory = factory;
        }

        [Theory]
        [Trait("Category", "GetTasks")]
        [InlineData("/gettask/12323")]
        [InlineData("/gettask/22121")]
        public async Task GetTasksByIdNotExisting(string url)
        {
            var testClient = _factory.CreateClient();
            var acttualRes = await testClient.GetAsync(url);
            Assert.Equal(HttpStatusCode.BadRequest, acttualRes.StatusCode);
        }

        [Theory]
        [Trait("Category", "GetTasks")]
        [InlineData("/gettask/1")]
        [InlineData("/gettask/2")]
        public async Task GetTaskByAlreadyExistingId(string url)
        {
            var testClient = _factory.CreateClient();
            var actualRes = await testClient.GetAsync(url);
            Assert.NotNull(actualRes);
            Assert.Equal("application/json; charset=utf-8", actualRes.Content.Headers.ContentType.ToString());
        }


        [Theory]
        [Trait("Category", "GetTasks")]
        [InlineData("/gettask")]
        public async Task GetAllTasks(string url)
        {
            var testClient = _factory.CreateClient();
            var actualRes = await testClient.GetAsync(url);
            Assert.Equal("application/json; charset=utf-8", actualRes.Content.Headers.ContentType.ToString());
        }


        [Theory]
        [Trait("Category", "CreateTask")]
        [InlineData("/createtask")]
        public async Task CreateAFreshTasks(string url)
        {
            var testClient = _factory.CreateClient();
            Tasks tasks = new Tasks { ID = 2222, ProjectID = 1211, Status = 2, AssiignedToUserID = 1, CreatedOn = new System.DateTime(2021, 4, 4, 4, 4, 4) };
            var actualRes = await testClient.PostAsync(url, (HttpContent)tasks);
            Assert.Equal(HttpStatusCode.OK, actualRes.StatusCode);
            Assert.Equal("text/plain; charset=utf-8", actualRes.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [Trait("Category", "CreateTask")]
        [InlineData("/createtask")]
        public async Task CreateATaskwithAlreadyExistingId(string url)
        {
            var testClient = _factory.CreateClient();
            Tasks tasks = new Tasks { ID = 1, ProjectID = 1211, Status = 2, AssiignedToUserID = 1, CreatedOn = new System.DateTime(2021, 4, 4, 4, 4, 4) };
            var actualRes = await testClient.PostAsync(url, (HttpContent)tasks);
            Assert.Equal(HttpStatusCode.BadRequest, actualRes.StatusCode);
        }

        [Theory]
        [Trait("Category", "UpdateTask")]
        [InlineData("/updatetask")]
        public async Task UpdateATaskNotExisting(string url)
        {
            var testClient = _factory.CreateClient();
            Tasks tasks = new Tasks { ID = 900, ProjectID = 1211, Status = 2, AssiignedToUserID = 1, CreatedOn = new System.DateTime(2021, 4, 4, 4, 4, 4) };
            var actualRes = await testClient.PutAsync(url, (HttpContent)tasks);
            Assert.Equal(HttpStatusCode.BadRequest, actualRes.StatusCode);
        }

        [Theory]
        [Trait("Category", "UpdateTask")]
        [InlineData("/updatetask")]
        public async Task UpdateATaskExisting(string url)
        {
            var testClient = _factory.CreateClient();
            Tasks tasks = new Tasks { ID = 1, ProjectID = 1211, Status = 2, AssiignedToUserID = 1, CreatedOn = new System.DateTime(2021, 4, 4, 4, 4, 4) };
            var actualRes = await testClient.PutAsync(url, (HttpContent)tasks);
            Assert.Equal(HttpStatusCode.OK, actualRes.StatusCode);
        }
    }
}
