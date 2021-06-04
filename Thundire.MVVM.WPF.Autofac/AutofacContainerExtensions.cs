using System;
using Autofac;
using Autofac.Builder;
using Thundire.MVVM.WPF.Services.ViewService;
using Thundire.MVVM.WPF.Services.ViewService.Interfaces;
using Thundire.MVVM.WPF.Services.ViewService.Models;

namespace Thundire.MVVM.WPF.Autofac
{
    public static class AutofacContainerExtensions{
        public static void AddViewHandlerCache<TContainerHandler>(
            this ContainerBuilder builder,
            IDIContainerBuilder containerBuilder,
            Action<IViewRegister> registration) 
            where TContainerHandler: IDIContainer
        {
            builder.RegisterType<TContainerHandler>().As<IDIContainer>().SingleInstance();
            var viewRegister = new ViewRegister(containerBuilder);
            registration?.Invoke(viewRegister);
            viewRegister.Build();
            builder.Register(context => viewRegister).As<IViewRegisterCache>().SingleInstance();
            builder.RegisterType<ViewHandlerService>().As<IViewHandlerService>();
        }

        public static void SetLifeTimeMode<TImplementer>(
            this IRegistrationBuilder<TImplementer, ConcreteReflectionActivatorData, SingleRegistrationStyle> typeBuilder,
            LifeTimeMode mode)
        {
            switch (mode)
            {
                case LifeTimeMode.Singleton:
                    typeBuilder.SingleInstance();
                    break;
                case LifeTimeMode.Transient:
                    typeBuilder.InstancePerDependency();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }
    }
}