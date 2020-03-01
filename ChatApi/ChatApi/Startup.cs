using ChatApi.Domain.Entities;
using ChatApi.Domain.Services;
using ChatApi.Domain.Services.Impl;
using ChatApi.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChatApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IValidationService, ValidationService>();
            services.AddSingleton<IAsyncRepository<User>, UserRepository>();
            services.AddSingleton<IAsyncRepository<Message>, MessageRepository>();

            services.AddSignalR();
            services.AddCors(opt => opt.AddPolicy("ChatPolicy", builder => builder.AllowAnyHeader()
                                                                                  .AllowAnyMethod()
                                                                                  .AllowCredentials()
                                                                                  .WithOrigins("http://localhost:4200")));

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("ChatPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/chat");
                endpoints.MapControllers();
            });
        }
    }
}
