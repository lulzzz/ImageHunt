using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ImageHunt
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) 
        {
          return WebHost.CreateDefaultBuilder(args)
            .UseKestrel()
            .ConfigureAppConfiguration((context, builder) =>
            {
              var env = context.HostingEnvironment;
              builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
              builder.AddEnvironmentVariables();
            })
            .ConfigureLogging((context, builder) =>
              {
                builder.AddConfiguration(context.Configuration.GetSection("Logging"));
                builder.AddConsole();
                builder.AddDebug();
              })
             .UseStartup<Startup>()
           .Build();
        
        }
    }
}
