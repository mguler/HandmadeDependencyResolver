using System;
using System.Collections.Generic;
using System.Linq;

namespace Handmade.DependencyResolver.Resolver
{
    public class HandMadeDependencyResolver:IDependencyResolver
    {
        private List<Type> registered = new List<Type>();
        public void Register<TImpl>()
        {
            if (!typeof(TImpl).IsClass || typeof(TImpl).IsAbstract)
            {
                throw new Exception($"{nameof(TImpl)} must be a non-abstract class");
            }
            registered.Add(typeof(TImpl));
        }
        public void Register<TService, TImpl>()
        {
            if (!typeof(TService).IsInterface)
            {
                throw new Exception($"{nameof(TService)} must be an interface");
            }

            var isInterfaceImplemented = typeof(TImpl).GetInterfaces().Any(_interface => _interface == typeof(TService));

            if (!isInterfaceImplemented)
            {
                throw new Exception($"{nameof(TImpl)} does not implement the interface {nameof(TService)}");
            }

            registered.Add(typeof(TImpl));

        }
        public object[] ResolveAll(Type serviceType)
        {
            var instances = registered.Where(type => type.GetInterfaces().Any(_interface => _interface == serviceType)).Select(typeToConstruct => Resolve(typeToConstruct)).ToArray();
            var dependencies = Array.CreateInstance(serviceType, instances == null ? 0 : instances.Length);
            instances.CopyTo(dependencies, 0);
            return (object[])dependencies;
        }
        public TService[] ResolveAll<TService>()
        {
            var serviceType = typeof(TService);
            var instances = ResolveAll(serviceType)?.Cast<TService>().ToArray();
            return instances;
        }
        public object Resolve(Type serviceType)
        {
            var type = registered.FirstOrDefault(typeToConstruct => typeToConstruct == serviceType || serviceType.IsInterface && typeToConstruct.GetInterfaces().Any(_interface => _interface == serviceType));
            var constructor = type.GetConstructors().FirstOrDefault();

            if (constructor == null)
            {
                throw new Exception($"the type {nameof(type)} does not have any public constructor(s)");
            }

            var parameters = constructor.GetParameters().Select(parameter =>
            {

                var parameterType = parameter.ParameterType.HasElementType ? parameter.ParameterType.GetElementType() : parameter.ParameterType;
                if (parameter.ParameterType.HasElementType)
                {
                    return ResolveAll(parameterType);
                }
                else
                {
                    return Resolve(parameterType);
                }
            }).ToArray();

            var instance = constructor.Invoke(parameters);
            return instance;

        }
        public TService Resolve<TService>()
        {
            return (TService)Resolve(typeof(TService));
        }
    }
}
