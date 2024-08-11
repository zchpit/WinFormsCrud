
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleWebApi.Model;
using SimpleWebApi.Interface;
using SimpleWebApi.IRepository;
using SimpleWebApi.Repository;
using SimpleWebApi.Services;
using SimpleWebApi.Strategy;
using SimpleWebApi.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace SimpleWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {


            var builder = WebApplication.CreateBuilder(args);

            IMapper mapper = MapperConfig.InitializeAutomapper();
            builder.Services.AddSingleton(mapper);

            // Add services to the container.
            builder.Services.AddScoped<IEncryptStrategy, Rfc2898EncryptStrategy>();
            builder.Services.AddScoped<ICaseRepository, CaseRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ICaseService, CaseService>();

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            builder.Services.AddControllers();
            var connectionString = builder.Configuration.GetConnectionString("ConnStr");
            builder.Services.AddDbContext<SimpleDbContext>(x => x.UseSqlServer(connectionString));


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

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
