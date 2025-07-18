
using System.Text;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NaftalanHotelSystem.API.Services;
using NaftalanHotelSystem.Application;
using NaftalanHotelSystem.Application.Abstractions.Services;
using NaftalanHotelSystem.Domain.Entites;
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
        
            builder.Services.AddApplicationServices();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins(
                        "http://localhost:3000",
                        "https://localhost:3000",
                        "http://localhost:3001",
                        "https://localhost:3001"
                    )
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            builder.Services.AddFluentValidation(fv =>
            {

                fv.RegisterValidatorsFromAssembly(typeof(NaftalanHotelSystem.Application.ServiceRegistration).Assembly);

                fv.DisableDataAnnotationsValidation = true;

            });



            //TODO : AppDbContext-->Persistence layer SR
            builder.Services.AddDbContext<AppDbContext>(options =>
                   options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));


          
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
             
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.User.RequireUniqueEmail = true; 
                options.SignIn.RequireConfirmedEmail = false; 
            })
            .AddEntityFrameworkStores<AppDbContext>() 
            .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true, 
                    ValidateAudience = true, 
                    ValidateLifetime = true, 
                    ValidateIssuerSigningKey = true, 

                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });


            //TODO : SmtpSetting Configure --> Infrastructure Layer SR
            builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

            


            //TODO : Infrastructure layer 
            builder.Services.AddScoped<IFileService, WebFileService>();
          
            builder.Services.AddInfrastructureServices();
            builder.Services.AddApplicationServices();
            builder.Services.AddPersistenceServices(builder.Configuration);

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<AppDbContext>(); 

                
                try
                {
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    Console.WriteLine("Seeding Identity admin users and roles...");
                    await IdentitySeeder.SeedAdminUserAsync(userManager, roleManager);
                    Console.WriteLine("Identity admin seeding completed.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred during Identity seeding: {ex.Message}");
                   
                }

                
                try
                {
                    var seederManager = services.GetRequiredService<SeederManager>();
                    Console.WriteLine("Running other seeders...");
                    await seederManager.SeedAsync(context);
                    Console.WriteLine("Other seeders completed.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred during general seeding: {ex.Message}");
                   
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();

            app.UseCors("AllowFrontend");

            app.UseAuthentication(); 
            app.UseAuthorization();

         
            app.MapControllers();

            await app.RunAsync();
        }
    }
}
