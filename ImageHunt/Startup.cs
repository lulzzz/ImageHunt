using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net.Http;
using System.Reflection;
using AutoMapper;
using ImageHunt.Computation;
using ImageHunt.Controllers;
using ImageHunt.Data;
using ImageHunt.Model;
using ImageHunt.Model.Node;
using ImageHunt.Services;
using ImageHuntWebServiceClient.Request;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;

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
      //services.AddCors();
      services.AddMvc()
        .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
      //services.AddSwaggerGen(c =>
      //{
      //  c.SwaggerDoc("v1", new Info()
      //  {
      //    Title = "ImageHuntApi SwaggerDoc",
      //    Version = "v1"
      //  });
      //  c.AddSecurityDefinition("Bearer", new ApiKeyScheme()
      //  {
      //    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
      //    Name = "Authorization",
      //    In = "Authorization",
      //    Type = "apiKey"
      //  });
      //  //Locate the XML file being generated by ASP.NET...
      //  var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
      //  var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

      //  //... and tell Swagger to use those XML comments.
      //  c.IncludeXmlComments(xmlPath);
      //});
      services.AddAuthorization();
      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
          options.RequireHttpsMetadata = false;
          options.SaveToken = true;
          //options.TokenValidationParameters = new TokenValidationParameters()
          //{

          //};
        });
      services.AddTransient<IAuthenticationHandler, TokenAuthenticationHandler>();
      services.AddTransient<IAuthorizationHandler, TokenAuthorizationHandler>();
      services.AddDbContext<HuntContext>(options =>
          options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));
      services.AddSingleton(Configuration);
      services.AddTransient(s =>
      {
        var sb = services.BuildServiceProvider();
        return new AuthControllerParameters(Configuration,
          new HttpClient() { BaseAddress = new Uri(Configuration["GoogleApi:AccessTokenUrl"]) },
          new HttpClient() { BaseAddress = new Uri(Configuration["GoogleApi:UserInfoUrl"]) },
          sb.GetService<IAuthService>()
        );
      });
      //services.AddTransient<ILocationHub, LocationHub>();
      services.AddTransient<ITeamService, TeamService>();
      services.AddTransient<IAdminService, AdminService>();
      services.AddTransient<IGameService, GameService>();
      services.AddTransient<IAuthService, AuthService>();
      services.AddTransient<IImageService, ImageService>();
      services.AddTransient<INodeService, NodeService>();
      services.AddTransient<IPlayerService, PlayerService>();
      services.AddTransient<IActionService, ActionService>();
      services.AddTransient<IImageTransformation, ImageTransformation>();
      services.AddSignalR();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, HuntContext dbContext)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      dbContext.Database.Migrate();
      //app.UseCors(builder=>builder.AllowAnyOrigin());
      app.Use(async (context, next) =>
      {
        await next();
        if (context.Response.StatusCode == 404 &&
            !Path.HasExtension(context.Request.Path.Value) &&
            !context.Request.Path.Value.StartsWith("/api/"))
        {
          context.Request.Path = "/index.html";
          await next();
        }
      });
      //app.UseSwagger();
      //app.UseSwaggerUI(c =>
      //{
      //  c.SwaggerEndpoint("/swagger/v1/swagger.json", "ImageHuntApi SwaggerDoc");
      //});
      app.UseMvcWithDefaultRoute();
      app.UseDefaultFiles();
      app.UseStaticFiles();
      app.UseMvc();
      app.UseSignalR(routes => { routes.MapHub<LocationHub>("/locationHub"); });
      ConfigureMappings();
    }

    public static void ConfigureMappings()
    {
      Mapper.Reset();
      Mapper.Initialize(config =>
      {
        config.CreateMap<AddNodeRequest, Node>()
          .ConstructUsing(r => NodeFactory.CreateNode(r.NodeType));
        config.CreateMap<GameActionRequest, GameAction>()
          .ForMember(x=>x.Picture, expression => expression.Ignore());
        config.CreateMap<Node, Node>().ForSourceMember(x => x.Id, opt => opt.Ignore());
        config.CreateMap<GameAction, GameActionToValidate>()
          .ForMember(x=>x.Node, x=>x.Ignore());
      });
    }
  }
}
