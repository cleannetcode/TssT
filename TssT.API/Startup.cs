using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TssT.Businesslogic;
using TssT.Businesslogic.Services.Test;
using TssT.DataAccess;
using TssT.DataAccess.Repositories.Test;

namespace TssT.API
{
    public class Startup
    {
        private const string DefaultPolicyName = "DefaultPolicy";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            
            
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var authOptions = new AuthOptions(Configuration).Configure();
            services.AddSingleton(authOptions);
            
            services.AddCors(o => o.AddPolicy(DefaultPolicyName,
                builder =>
                {
                    builder.WithOrigins(
                            "http://localhost:4200",
                            "https://localhost:4200",
                            "http://localhost:5000",
                            "https://localhost:5001"
                            )
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                }));

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(
                    Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddAutoMapper(conf =>
            {
                conf.AddProfile<ApiMappingProfile>();
                conf.AddProfile<DataAccessMappingProfile>();
                conf.AddProfile<BLMappingProfile>();
            });

            //In combination with UseDeveloperExceptionPage, this captures database-related exceptions that can be resolved by using Entity Framework migrations.
            //When these exceptions occur, an HTML response with details about possible actions to resolve the issue is generated.
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddIdentity<DataAccess.Entities.User, DataAccess.Entities.Role>(
                options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    // валидируется ли издатель токена
                    ValidateIssuer = true,
                    // издатель
                    ValidIssuer = authOptions.Issuer,

                    // валидируется ли потребитель токена
                    ValidateAudience = true,
                    // потребитель токена
                    ValidAudience = authOptions.Audience,

                    // валидация времени существования токена
                    ValidateLifetime = true,

                    // валидируется ли ключ безопасности
                    ValidateIssuerSigningKey = true,
                    // ключ безопасности
                    IssuerSigningKey = authOptions.GetSymmetricSecurityKey()
                };
            });

            services.AddScoped<ITestRepository, TestRepository>();
            services.AddScoped<ITestService, TestService>();

            services.AddControllers();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo {Title = "Technology stack self-confidence Test", Version = "v1"});
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme."+
                        "\r\n\r\nEnter 'Bearer' [space] and then your token in the text input below." +
                        "\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.IncludeXmlComments(Path.Combine(System.AppContext.BaseDirectory, "TssT.API.xml"));

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Technology stack self-confidence Test v1"));
            }

            app.UseCors(DefaultPolicyName);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}