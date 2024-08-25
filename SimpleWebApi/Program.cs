
using AutoMapper;
using CommonLibrary.Strategy;
using Microsoft.EntityFrameworkCore;
using SimpleWebApi.Helpers;
using SimpleWebApi.IRepository;
using SimpleWebApi.IServices;
using SimpleWebApi.Model;
using SimpleWebApi.Repository;
using SimpleWebApi.Services;
using SimpleWebApi.CustomExceptionMiddleware;


namespace SimpleWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            IMapper mapper = MapperConfig.InitializeAutomapper();
            builder.Services.AddSingleton(mapper);

            // Configure NLog
            builder.Services.AddLogging(logging =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            });
            builder.Services.AddTransient<ILoggerManager, LoggerManager>();

            builder.Services.AddTransient<IEncryptStrategy, Rfc2898EncryptStrategy>();
            builder.Services.AddTransient<ITransferStrategy, Base64TransferStrategy>();

            builder.Services.AddTransient<IServiceManager, ServiceManager>();

            builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            builder.Services.AddControllers();
            var connectionString = builder.Configuration.GetConnectionString("ConnStr");
            builder.Services.AddDbContext<SimpleDbContext>(x => x.UseSqlServer(connectionString));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            var logger = app.Services.GetRequiredService<ILoggerManager>();
            app.ConfigureExceptionHandler(logger);
            app.ConfigureCustomExceptionMiddleware();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
