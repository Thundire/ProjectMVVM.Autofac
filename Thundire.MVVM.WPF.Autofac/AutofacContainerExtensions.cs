using System;
using Autofac;
using Autofac.Builder;
using Thundire.MVVM.WPF.Services;
using Thundire.MVVM.WPF.Services.Interfaces;
using Thundire.MVVM.WPF.Services.Regions;
using Thundire.MVVM.WPF.Services.ViewService;
using Thundire.MVVM.WPF.Services.ViewService.Interfaces;
using Thundire.MVVM.WPF.Services.ViewService.Models;

namespace Thundire.MVVM.WPF.Autofac
{
    public static class AutofacContainerExtensions
    {
        public static void AddViewHandlerService(this ContainerBuilder builder, Action<IViewRegister> registration) 
        {
            var viewRegister = new ViewRegister(new AutofacContainerBuilder(builder));
            registration?.Invoke(viewRegister);
            viewRegister.Build();

            builder.RegisterType<AutofacContainer>().As<IDIContainer>().SingleInstance();
            builder.RegisterInstance(viewRegister).As<IViewRegisterCache>();
            builder.RegisterType<ViewHandlerService>().As<IViewHandlerService>();
        }

        public static void AddRegionsService(this ContainerBuilder builder, Action<ITemplatesCacheBuilder> configuration)
        {
            ITemplatesRegister register = new DataTemplatesRegister();
            register.AddTemplates(configuration);

            builder.RegisterInstance(register).As<ITemplatesCache>();
            builder.RegisterType<RegionsService>();
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