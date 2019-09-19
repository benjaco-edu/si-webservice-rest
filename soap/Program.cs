using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MiniprojectSoapService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if(args.Length>0){
                if(args[0]=="Newdb"){
                    RelationsDb sqldb = new RelationsDb();
                    sqldb.NewDb();
                }
            }
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
