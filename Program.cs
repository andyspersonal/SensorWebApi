using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace SensorWebApi
{
   public class Program
   {
      public static IConfiguration Configuration { get; set; }
      public static void Main(string[] args)
      {
         var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json");
         Configuration = builder.Build();
         Console.WriteLine("usein memory = " + Configuration["useInMemoryStore"]);
         BuildWebHost(args).Run();
         //to public run dotnet publish -r linux-arm
         // see https://github.com/dotnet/core/blob/master/samples/RaspberryPiInstructions.md
            //to copy to pi run 
            //scp -rp linux-arm pi@piserver.local:/home/pi/temperature
            //or use winscp
            //to control services ont he pi
            //systemctrl start sesrialread.service
            //systemctrl start webapi.service
            //service files are in /lib/systemd/system

      }

      public static IWebHost BuildWebHost(string[] args) =>
         
          WebHost.CreateDefaultBuilder(args)
              .UseStartup<Startup>()
              .UseUrls("http://*:80")
                .Build();
    }
}
