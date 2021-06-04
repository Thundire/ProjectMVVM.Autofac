using System;
using Autofac;
using Thundire.MVVM.WPF.Services.ViewService.Interfaces;

namespace Thundire.MVVM.WPF.Autofac
{
    public class AutofacContainer : IDIContainer
    {
        private readonly ILifetimeScope _services;
        
        public AutofacContainer(ILifetimeScope services)
        {
            _services = services;
        }
        
        public bool IsRegistered(Type type)
        {
            return _services.IsRegistered(type);
        }

        public object Resolve(Type type)
        {
            var result = _services.Resolve(type);
            return result;
        }
    }
}