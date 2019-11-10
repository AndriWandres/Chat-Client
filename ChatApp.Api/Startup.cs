using AutoMapper;
using ChatApp.Api.Configuration;
using ChatApp.Api.Database;
using ChatApp.Api.Services;
using ChatApp.Api.SignalR.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Api
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
            // Add database context
            services.AddDbContext<ChatContext>(builder =>
            {
                builder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            var jwtSection = Configuration.GetSection("Jwt");
            var jwtSettings = jwtSection.Get<JwtSettings>();
            var jwtSecret = Encoding.ASCII.GetBytes(jwtSettings.Secret);

            // Add JWT Configuration
            services.Configure<JwtSettings>(jwtSection);

            // Add JWT Bearer Authentication
            services.AddAuthentication(builder =>
            {
                builder.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                builder.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(builder => 
            {
                builder.RequireHttpsMetadata = false;
                builder.SaveToken = true;
                builder.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(jwtSecret),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };

                // Attach SignalR access token
                builder.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        PathString path = context.HttpContext.Request.Path;
                        string token = context.Request.Query["access_token"];

                        if (!string.IsNullOrEmpty(token) && path.StartsWithSegments("/hubs/chat"))
                        {
                            context.Token = token;
                        }

                        return Task.CompletedTask;
                    }
                };
            });

            // Add SignalR Websocket
            services.AddSignalR();
            
            // Add AutoMapper
            services.AddAutoMapper(typeof(Startup));

            // Add Swagger
            services.AddSwaggerGen(options =>
            {
                // Swagger document
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Chat API",
                    Description = "Web Api for Chat App"
                });

                // Add Bearer security definition
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                // Add Bearer security requirement
                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new string[] { }
                    }
                });
            });

            // Add Custom Services
            services.AddScoped<UserService>();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                 {
                     options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                 });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Use JWT Bearer Authentication
            app.UseAuthentication();
            
            // Use Swaggger UI
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Chat API");
                options.DocExpansion(DocExpansion.None);
            });

            // Use SignalR Websocket
            app.UseSignalR(builder =>
            {
                builder.MapHub<ChatHub>("/chat");
            });

            // Use Cross-Origin-Resource-Sharing
            app.UseCors(builder =>
            {
                builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .AllowCredentials();
            });

            app.UseMvc();
        }
    }
}
