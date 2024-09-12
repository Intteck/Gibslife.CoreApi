using System;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Gibs.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var section = builder.Configuration.GetSection("AppSettings");
            var settings = section.Get<Settings>();
            var services = builder.Services;

            if (settings == null)
                throw new InvalidOperationException("Configuration Settings node is missing");

            services.Configure<Settings>(section);

            services.AddDbContext<Infrastructure.GibsContext>(options =>
            {
                options.UseSqlServer(settings.ConnStrings.SqlDb);
                options.EnableSensitiveDataLogging(true);
            });

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = actionContext =>
                        new Contracts.V1.ApiErrorResult(actionContext.ModelState);
                });

            services.AddSwaggerGen(options =>
            {
                var companyHost = "product.gibsonline.com";
                var companyUrl = $"https://{companyHost}";

                options.SupportNonNullableReferenceTypes();

                options.CustomOperationIds(e =>
                {
                    var controllerAction = (ControllerActionDescriptor)e.ActionDescriptor;
                    return controllerAction.ActionName;
                });

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Description = "Enterprise Insurance API for Nigeria",
                    Title = "Gibs Enterprise Api",
                    Version = "v5",
                    Contact = new OpenApiContact()
                    {
                        Name = "Gibs Insurance",
                        Email = $"info@{companyHost}",
                        Url = new Uri(companyUrl)
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "Privacy Policy",
                        Url = new Uri($"{companyUrl}/Legal/Privacy.txt")
                    },
                    TermsOfService = new Uri($"{companyUrl}/Legal/Agreement.txt")
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n" +
                    " Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
                    "Example: \"Bearer 1safsfsdfdfd\"",
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
                });
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    var key = Encoding.ASCII.GetBytes(settings.JWT.Secret);

                    options.SaveToken = false;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                });

            services.AddScoped<Services.EmailService>();
            services.AddScoped<Controllers.ControllerServices>();

            services.AddSingleton(settings);
            services.AddHttpContextAccessor();
            services.AddCors();

            //---------------------------------------------------------------------------

            var app = builder.Build();

            app.UseCors(options =>
            {
                options
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders(Settings.Headers.PAGE_SIZE)
                .WithExposedHeaders(Settings.Headers.PAGE_NUMBER)
                .WithExposedHeaders(Settings.Headers.TOTAL_PAGES)
                .WithExposedHeaders(Settings.Headers.TOTAL_ITEMS);
            });
            app.UseSwagger(options =>
            {
                options.RouteTemplate = "api-docs/{documentName}/openapi.json";
            });
            app.UseSwaggerUI(options =>
            {
                options.DisplayOperationId();
                options.DocumentTitle = "Gibs Enterprise Api";
                options.SwaggerEndpoint("/api-docs/v1/openapi.json", "Gibs Api V7.8");
                options.RoutePrefix = string.Empty;
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            await RunStartupTasks(app);

            app.Run();
        }

        private static async Task RunStartupTasks(WebApplication app)
        {
            // fetch ProductFields to cache
            using var scope = app.Services.GetService<IServiceScopeFactory>()!.CreateScope();
            using var db = scope.ServiceProvider.GetRequiredService<Infrastructure.GibsContext>();

            var fields = await db.ProductFields.AsNoTracking().ToListAsync();
            Domain.Entities.Product.SetFieldsCache(fields);

            var rates = await db.PartyRates.AsNoTracking().ToListAsync();
            Domain.Entities.Product.SetRatesCache(rates);
        }
    }
}
