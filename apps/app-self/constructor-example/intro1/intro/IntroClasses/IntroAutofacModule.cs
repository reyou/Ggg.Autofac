using System;
using Autofac;
using intro.IntroClasses;

namespace intro
{
    public class IntroAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.Register(q => new CarRepository(DateTime.Now)).As<ICarRepository>();
        }
    }
}