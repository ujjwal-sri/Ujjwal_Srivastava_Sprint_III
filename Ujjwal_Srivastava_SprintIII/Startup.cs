using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore.InMemory;
using ProjectRepository.Repository;
using ProjectRepository.DBAccess;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using ProjectRepository.Models;

namespace Ujjwal_Srivastava_SprintIII
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DBContext>(context => { context.UseInMemoryDatabase("ProjectApplication");});
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository.Repository.ProjectRepository>();
            services.AddScoped<ITasksRepository, TasksRepository>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Project Api",
                    Version = "v1",
                    Description = "Contains Project info",
                    Contact = new OpenApiContact
                    {
                    },
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Project Api");
                c.RoutePrefix = string.Empty;
            });
            app.UseDeveloperExceptionPage();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<DBContext>();
            SeedData(context);
        }

        public void SeedData(DBContext context)
        {
            context.Users.Add(new User { ID = 1, FirstName = "John", SecondName = "Doe", Email = "john.doe@test.com", Password = "Password1" });
            context.Users.Add(new User { ID = 2, FirstName = "John", SecondName = "Skeet", Email = "john.skeet@test.com", Password = "Password1" });
            context.Users.Add(new User { ID = 3, FirstName = "Mark", SecondName = "Seeman", Email = "mark.seeman@test.com", Password = "Password1" });
            context.Users.Add(new User { ID = 4, FirstName = "Bob", SecondName = "Martin", Email = "bob.martin@test.com", Password = "Password1" });

            context.Projects.Add(new Project { ID = 1, Name = "TestProject1", Detail = "This is a test project", CreatedOn = new DateTime(2021, 05, 12, 0, 0, 0) });
            context.Projects.Add(new Project { ID = 2, Name = "TestProject2", Detail = "This is a test project", CreatedOn = new DateTime(2021, 06, 12, 0, 0, 0) });
            context.Projects.Add(new Project { ID = 3, Name = "TestProject3", Detail = "This is a test project", CreatedOn = new DateTime(2021, 07, 12, 0, 0, 0) });
            context.Projects.Add(new Project { ID = 4, Name = "TestProject4", Detail = "This is a test project", CreatedOn = new DateTime(2021, 08, 12, 0, 0, 0) });

            context.Tasks.Add(new Tasks { ID = 1, ProjectID = 1, Status = 2, AssiignedToUserID = 1,Detail = "This is a test task", CreatedOn = new DateTime(2021, 05, 12, 0, 0, 0) });
            context.Tasks.Add(new Tasks { ID = 2, ProjectID = 1, Status = 3, AssiignedToUserID = 2, Detail = "This is a test task", CreatedOn = new DateTime(2021, 06, 12, 0, 0, 0) });
            context.Tasks.Add(new Tasks { ID = 3, ProjectID = 2, Status = 4, AssiignedToUserID = 2, Detail = "This is a test task", CreatedOn = new DateTime(2021, 07, 12, 0, 0, 0) });
            context.SaveChanges();
        }
    }
}
