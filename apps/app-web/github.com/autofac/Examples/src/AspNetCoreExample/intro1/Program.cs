using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Autofac.Extensions.DependencyInjection;

namespace AspNetCoreExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // The ConfigureServices call here allows for
            // ConfigureContainer to be supported in Startup with
            // a strongly-typed ContainerBuilder. If you don't
            // have the call to AddAutofac here, you won't get
            // ConfigureContainer support.
            IWebHost host = new WebHostBuilder()
                // Specify Kestrel as the server to be used by the web host.
                .UseKestrel()
                .ConfigureServices(services => services.AddAutofac())
                .UseContentRoot(Directory.GetCurrentDirectory())
                // Configures the port and base path the server should listen on when running behind AspNetCoreModule.
                // The app will also be configured to capture startup errors.
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
