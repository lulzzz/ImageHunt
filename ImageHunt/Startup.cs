using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ImageHunt.Controllers;
using ImageHunt.Data;
using ImageHunt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ImageHunt
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
            services.AddMvc();
          services.AddAuthorization();
          services.AddTransient<IAuthorizationHandler, TokenAuthorizationHandler>();
            services.AddDbContext<HuntContext>(options => 
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));
          services.AddSingleton(Configuration);
          services.AddTransient<AuthControllerParameters>(s =>
          {
            var sb = services.BuildServiceProvider();
            return new AuthControllerParameters(Configuration,
              new HttpClient() {BaseAddress = new Uri(Configuration["GoogleApi:AccessTokenUrl"])},
              new HttpClient() {BaseAddress = new Uri(Configuration["GoogleApi:UserInfoUrl"])},
              sb.GetService<IAuthService>()
            );
          });
            services.AddTransient<ITeamService, TeamService>();
            services.AddTransient<IAdminService, AdminService>();
            services.AddTransient<IGameService, GameService>();
            services.AddTransient<IAuthService, AuthService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.Use(async (context, next) => {
                await next();
                if (context.Response.StatusCode == 404 &&
                    !Path.HasExtension(context.Request.Path.Value) &&
                    !context.Request.Path.Value.StartsWith("/api/"))
                {
                    context.Request.Path = "/index.html";
                    await next();
                }
            });

            app.UseMvcWithDefaultRoute();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
