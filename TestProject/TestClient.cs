using Ujjwal_Srivastava_SprintIII;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace TestProject
{
    public class TestClient : WebApplicationFactory<Startup>
    {
        protected override IWebHostBuilder CreateWebHostBuilder() => WebHost.CreateDefaultBuilder().UseStartup<Startup>();
    }
}
