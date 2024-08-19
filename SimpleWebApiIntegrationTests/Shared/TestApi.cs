using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using SimpleWebApi;
using SimpleWebApi.Interface;
using SimpleWebApi.IServices;
using SimpleWebApiIntegrationTests.TestServices;


namespace SimpleWebApiIntegrationTests.Shared
{
    public sealed class TestApi : WebApplicationFactory<Program>
    {
        public HttpClient Client { get; }
        
        public TestApi(Action<IServiceCollection>? services = null) 
        { 
            Client = WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Test");

                /*
                if(services is not null)
                {
                    builder.ConfigureServices(services);
                }*/

                builder.ConfigureTestServices(services =>
                {
                    services.AddScoped<IUserService, TestUserService>();
                    services.AddScoped<IReportService, TestReportService>();
                    services.AddScoped<ICaseService, TestCaseService>();
                });

            }).CreateClient();
        }
    }
}
