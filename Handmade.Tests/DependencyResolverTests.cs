using Handmade.Classes;
using Handmade.DependencyResolver.Resolver;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Handmade.Tests
{
    [TestClass]
    public class DependencyResolverTests
    {

        [TestMethod]
        public void TestForResolvingMultipleInstances()
        {
            var dependencyResolver = new HandMadeDependencyResolver();

            #region Register types for services(interfaces)
            dependencyResolver.Register<A, B>();
            dependencyResolver.Register<C, D>();
            dependencyResolver.Register<E, G>();
            dependencyResolver.Register<F, H>();
            dependencyResolver.Register<F, H1>();
            dependencyResolver.Register<F, H2>();
            dependencyResolver.Register<F, H3>();
            #endregion Register types for services(interfaces)

            //resolve types and get instances for a service(interface) that you need
            var instances = dependencyResolver.ResolveAll<F>();
            //you must get four instances that you registered for the service F before
            Assert.IsTrue(instances?.Length == 4);

        }

        [TestMethod]
        public void TestForResolvingSingleInstance()
        {
            var dependencyResolver = new HandMadeDependencyResolver();

            #region Register types for services(interfaces)
            dependencyResolver.Register<A, B>();
            dependencyResolver.Register<C, D>();
            dependencyResolver.Register<E, G>();
            dependencyResolver.Register<F, H>();
            dependencyResolver.Register<F, H1>();
            dependencyResolver.Register<F, H2>();
            dependencyResolver.Register<F, H3>();
            #endregion Register types for services(interfaces)

            //Resolve type and get an instance for a service(interface) that you need
            var instance = dependencyResolver.Resolve<A>();
            //you must get one instance that you registered for the service A before
            Assert.IsTrue(instance != null);
        }

        [TestMethod]
        public void TestForTheClassWhichHasAConstructorWithParameters()
        {
            var dependencyResolver = new HandMadeDependencyResolver();

            #region Register types for services(interfaces)
            dependencyResolver.Register<IAnInterfaceForTheClassWhichHasConstructorWithParameters, TheClassWhichHasConstructorWithParameters>();
            dependencyResolver.Register<A, B>();
            dependencyResolver.Register<C, D>();
            dependencyResolver.Register<E, G>();
            dependencyResolver.Register<F, H>();
            dependencyResolver.Register<F, H1>();
            dependencyResolver.Register<F, H2>();
            dependencyResolver.Register<F, H3>();
            #endregion Register types for services(interfaces)

            //Resolve type and get an instance for a service(interface) that you need
            var instance = dependencyResolver.Resolve<IAnInterfaceForTheClassWhichHasConstructorWithParameters>();
            //you must get one instance that you registered for the service IAnInterfaceForTheClassWhichHasConstructorWithParameters
            //before and you must also get instances for constructor parameters
            Assert.IsTrue(instance != null && instance.A != null && instance.C != null);
        }



    }
}
