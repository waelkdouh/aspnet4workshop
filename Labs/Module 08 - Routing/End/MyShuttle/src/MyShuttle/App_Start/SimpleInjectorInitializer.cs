[assembly: WebActivator.PostApplicationStartMethod(typeof(MyShuttle.Web.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace MyShuttle.Web.App_Start
{
    using System.Reflection;
    using System.Web.Mvc;

    using SimpleInjector;
    using SimpleInjector.Extensions;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
    
    public static class SimpleInjectorInitializer
    {
        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            
            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            
            //container.Verify();
            
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
     
        private static void InitializeContainer(Container container)
        {
            container.Register<MyShuttle.Data.ICarrierRepository, MyShuttle.Data.CarrierRepository>();
            container.Register<MyShuttle.Data.MyShuttleContext>(() => new MyShuttle.Data.MyShuttleContext(), Lifestyle.Scoped);
            // For instance:
            // container.Register<IUserRepository, SqlUserRepository>(Lifestyle.Scoped);
        }
    }
}