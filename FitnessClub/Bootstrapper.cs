using System.Web.Http;
using System.Web.Mvc;
using BusinessServices;
//using DataModel.UnitOfWork;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using Unity.WebApi;
using Resolver;

namespace FitnessClub
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            //DependencyResolver.SetResolver(new Microsoft.Practices.Unity.Mvc.UnityDependencyResolver(container));
            System.Web.Mvc.DependencyResolver.SetResolver(new Microsoft.Practices.Unity.Mvc.UnityDependencyResolver(container));    

            // register dependency resolver for WebAPI RC
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();        
            //container.RegisterType<IUserServices, UserServices>().RegisterType<UnitOfWork>(new HierarchicalLifetimeManager());
            RegisterTypes(container);

            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {

            //Component initialization via MEF
            ComponentLoader.LoadContainer(container, ".\\bin", "FitnessClub.dll");
            ComponentLoader.LoadContainer(container, ".\\bin", "BusinessServices.dll");

        }
    }
}