
using Microsoft.EntityFrameworkCore;
using NaftalanHotelSystem.Application;
using NaftalanHotelSystem.Infrastructure.Services;
using NaftalanHotelSystem.Persistence;
using NaftalanHotelSystem.Persistence.DataAccessLayer;
using NaftalanHotelSystem.Persistence.SeedData;

namespace NaftalanHotelSystem.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(
                    opt=>opt.AddDefaultPolicy(
                        builder=>builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:3000", "http://localhost:3000", "http://localhost:3001", "https://localhost:3001")
                        )
                );

            //TODO : AppDbContext-->Persistence layer SR
           builder.Services.AddDbContext<AppDbContext>(options =>
                   options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

            //TODO : SmtpSetting Configure --> Infrastructure Layer SR
            builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));



          
            builder.Services.AddInfrastructureServices();
            builder.Services.AddApplicationServices();
            builder.Services.AddPersistenceServices(builder.Configuration);

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var seederManager = scope.ServiceProvider.GetRequiredService<SeederManager>();
                await seederManager.SeedAsync(context);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors();
            app.UseAuthorization();


            app.MapControllers();

            await app.RunAsync();
        }
    }
}
