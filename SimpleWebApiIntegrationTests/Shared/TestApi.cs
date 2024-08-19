using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using SimpleWebApi;


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
                if(services is not null)
                {
                    builder.ConfigureServices(services);
                }

            }).CreateClient();
        }
    }
}
