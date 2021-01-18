using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Ankor.Personal.WebService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    var url = "http://127.0.0.1:7005";
                    //var url = "http://100.83.198.202:7005";
                    Console.WriteLine($"Starting {url}");
                    webBuilder.UseStartup<Startup>().UseUrls(url);
                    //webBuilder.UseStartup<Startup>().UseUrls("http://100.83.198.202:7005");
                });
    }
}
