using Helpers.Configration;
using Helpers.Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace Helpers
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var host = CreateHostDefaultBuilder(args).Build();
            host.RunAsync();
            RunTask(host.Services);
        }



        public static IHostBuilder CreateHostDefaultBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(app => { app.AddJsonFile("appsettings.json"); })
            .ConfigureServices((_, services) =>
            {
                services.AddConnection(_.Configuration)
                        .ConfigureMapper()
                        .ConfigureRepository()
                        .ConfigureManager()
                        .ConfigureHelper();
                
                        
            });
        }



        public static void RunTask(IServiceProvider services)
        {
            var library = (ILibraryHelper)services.GetService(typeof(ILibraryHelper));
            Task.Run(() => library?.Execute()).Wait();
        }
    }
}
