using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text.Json.Serialization;
using Vlpi.Data.Infrastructure;
using Vlpi.Data.Models;
using Vlpi.Domain.Configurations;
using Vlpi.Domain.Services.Implementation;
using Vlpi.Domain.Services.Interfaces;

namespace VlpiApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("Token-Expired", "InvalidRefreshToken", "InvalidCredentials")
                    .WithOrigins("http://localhost:3000")
                    .AllowCredentials()
                    .Build());
            });
            services.AddDbContext<VLPIContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("VLPI"), 
                x => x.MigrationsAssembly("Vlpi.Data")));
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<Module>, Repository<Module>>();
            services.AddScoped<IRepository<Test>, Repository<Test>>();
            services.AddScoped<IRepository<Task>, Repository<Task>>();
            services.AddScoped<IRepository<TestResult>, Repository<TestResult>>();
            services.AddScoped<IRepository<UserAnswer>, Repository<UserAnswer>>();
            services.AddScoped<IRepository<AnswerBlock>, Repository<AnswerBlock>>();
            services.AddScoped<IRepository<Emoji>, Repository<Emoji>>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IModuleService, ModuleService>();
            services.AddScoped<ITestService, TestService>();
            services.AddScoped<ITaskService, TaskService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = JwtTokenOptions.ISSUER,

                            ValidateAudience = true,
                            ValidAudience = JwtTokenOptions.AUDIENCE,
                            ValidateLifetime = true,

                            IssuerSigningKey = JwtTokenOptions.GetSymmetricSecurityKey(),
                            ValidateIssuerSigningKey = true,
                        };
                    });
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "VlpiApi", Version = "v1" }); var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter JWT Bearer token **_only_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme,
                    },
                };

                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securityScheme, Array.Empty<string>() },
                });
            });
            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "VlpiApi v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
