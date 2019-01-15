using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;

namespace intro.IntroClasses
{
    public class RepositoryHandlerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PostRepository>().As<IPostRepository>();
        }
    }
}
