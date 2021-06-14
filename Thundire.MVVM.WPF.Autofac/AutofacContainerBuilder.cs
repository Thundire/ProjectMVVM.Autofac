using System;
using Autofac;
using Thundire.MVVM.WPF.Services.Interfaces;
using Thundire.MVVM.WPF.Services.ViewService.Models;

namespace Thundire.MVVM.WPF.Autofac
{
    public class AutofacContainerBuilder : IDIContainerBuilder
    {
        private readonly ContainerBuilder _builder;
        
        public AutofacContainerBuilder(ContainerBuilder builder)
        {
            _builder = builder;
        }
        
        public void RegisterType(Type type, LifeTimeMode mode)
        {
            _builder.RegisterType(type).SetLifeTimeMode(mode);
        }

        public void RegisterTypeAs(Type type, Type asType, LifeTimeMode mode)
        {
            _builder.RegisterType(type).As(asType).SetLifeTimeMode(mode);
        }
    }
}