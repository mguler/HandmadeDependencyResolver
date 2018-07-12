using System;

namespace Handmade.DependencyResolver.Resolver
{
    public interface IDependencyResolver
    {
        void Register<TImpl>();
        void Register<TService, TImpl>();
        object[] ResolveAll(Type serviceType);
        TService[] ResolveAll<TService>();
        object Resolve(Type serviceType);
        TService Resolve<TService>();
    }
}
