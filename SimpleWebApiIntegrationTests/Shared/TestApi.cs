using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using SimpleWebApi;
using System.Diagnostics.CodeAnalysis;

namespace SimpleWebApiIntegrationTests.Shared
{
    [ExcludeFromCodeCoverage]
    public sealed class TestApi : WebApplicationFactory<Program>
    {
        public HttpClient Client { get; }

        public TestApi(Action<IServiceCollection>? services = null)
        {
            Client = WithWebHostBuilder(builder =>
            {
                if (services is not null)
                {
                    builder.ConfigureServices(services);
                }

                builder.UseEnvironment("Test");
            }).CreateClient();
        }
    }
}
