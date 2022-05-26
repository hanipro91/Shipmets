using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ShipmentTracking
{
    public class Program
    {
        public const string COOKIE_NAME = "ShpimentCustomerID";
        public const string COOKIE_NAME_ADMIN = "ShipmentsAdminID";
        
        public static string ConnectionString { get; set; }

        public static string GetSourceLogo(string src)
        {
            src = src.ToLower();
            if (src == "alibaba") return "alibaba.png";
            if (src == "aliexpress") return "AliExpress.jfif";
            if (src == "amazon") return "amazon.png";
            return "";
        }
        public static void Main(string[] args)
        {
            
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
