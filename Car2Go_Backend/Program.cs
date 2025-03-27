
using Car2Go.Data;
using Car2Go.Models;
using Car2Go.Repository;
using Car2Go.Service;
using Car2Go.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using log4net.Config;
using log4net;

namespace Car2Go
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddLog4net();

            builder.Services.AddDbContext<Car2GoDBContext>();

            builder.Services.AddScoped<IUserService,UserRepository>();

            builder.Services.AddScoped<ICarService, CarRepository>();
            builder.Services.AddScoped<ICarFilterService, CarFilterRepository>();
            builder.Services.AddScoped<ICarSearchService, CarSearchRepository>();
            builder.Services.AddScoped<ICarFilterByLocationService, CarFilterByLocationRepository>();

            builder.Services.AddScoped<ILocationService, LocationRepository>();

            builder.Services.AddScoped<IReservationService, ReservationRepository>();

            builder.Services.AddScoped<IReviewService, ReviewRepository>();

            builder.Services.AddScoped<IPaymentService, PaymentRepository>();


            builder.Services.AddScoped<IAgentService, AgentRepository>();

            builder.Services.AddScoped<ILoginService, LoginRepository>();

            builder.Services.AddScoped<TokenService>();

            // Add authentication with JWT
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
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

            // Add authorization
            builder.Services.AddAuthorization();

            // Configure Swagger to include authentication
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });

                // Add JWT Bearer authentication definition
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter your token in the format: Bearer {token}",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                // Add global requirement for JWT Bearer authentication
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowLocalhost");


            // Ensure authentication middleware is used before authorization
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }
    }
}
