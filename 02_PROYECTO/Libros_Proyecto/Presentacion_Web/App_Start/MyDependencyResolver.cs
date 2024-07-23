using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Presentacion_Web.Service;
using Unity;
using Unity.Exceptions;

namespace Presentacion_Web.App_Start
{
    public class MyDependencyResolver : IDependencyResolver
    {
        private readonly IUnityContainer _container;

        public MyDependencyResolver()
        {
            _container = new UnityContainer();
            RegisterDependencies();
        }

        private void RegisterDependencies()
        {
            // Registrar AuthorService
            _container.RegisterType<AuthorService>();
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return _container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }
    }
}
