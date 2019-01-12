using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Core;

namespace intro.IntroClasses
{
    public class AutofacModule : IModule
    {

        // Configure is where you add middleware. This is called after
        // ConfigureContainer. You can use IApplicationBuilder.ApplicationServices
        // here if you need to resolve things from the container.
        public void Configure(IComponentRegistry componentRegistry)
        {
            Console.WriteLine(this.GetType().FullName + " " + "is called.");
        }
    }
}
