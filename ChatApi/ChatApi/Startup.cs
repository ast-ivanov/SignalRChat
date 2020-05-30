using ChatApi.Domain.Entities;
using ChatApi.Domain.Services;
using ChatApi.Domain.Services.Impl;
using ChatApi.Hubs;
using ChatApi.Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MediatR;
using ChatApi.Application.Messages.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System;
using ChatApi.Application.Models;

namespace ChatApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var settingsSection = Configuration.GetSection("ApplicationSettings");

            services.Configure<ApplicationSettings>(settingsSection);

            services.AddSingleton<IValidationService, ValidationService>();
            services.AddSingleton<IAsyncRepository<User>, UserRepository>();
            services.AddSingleton<IAsyncRepository<Message>, MessageRepository>();
            services.AddSingleton<IUserManager, UserManager>();

            services.AddDbContext<ChatDbContext>(optionsBuilder =>
                                               {
                                                   optionsBuilder.UseSqlServer(
                                                       Configuration.GetConnectionString("ChatConnection"));
                                               });
            services.AddDefaultIdentity<User>().AddEntityFrameworkStores<ChatDbContext>();
            services.Configure<IdentityOptions>(options =>
                                                {
                                                    options.Password.RequireDigit = false;
                                                    options.Password.RequireNonAlphanumeric = false;
                                                    options.Password.RequireLowercase = false;
                                                    options.Password.RequireUppercase = false;
                                                    options.Password.RequiredLength = 4;
                                                });
            var appSettings = settingsSection.Get<ApplicationSettings>();
            var key = Encoding.UTF8.GetBytes(appSettings.JWT_Secret);
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.RequireHttpsMetadata = false;
                opt.SaveToken = false;
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddMediatR(typeof(GetAllMessagesQuery).Assembly);

            services.AddSignalR();
            services.AddCors(opt =>
                opt.AddPolicy("ChatPolicy",
                    builder => builder.AllowAnyHeader()
                                      .AllowAnyMethod()
                                      .AllowCredentials()
                                      .WithOrigins(appSettings.Client_URL)));
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("ChatPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
                             {
                                 endpoints.MapHub<ChatHub>("/chat");
                                 endpoints.MapControllers();
                             });
        }
    }
}