using BusinessEntityManager;
using Caching;
using Database;
using Interfaces;
using Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using UnitOfWork;
using Unity;
using Unity.Mvc5;

namespace Bootstraper
{
    public static class Bootstrapper
    {
        public static IUnityContainer Initialize()
        {
            var container = BuildUnityContainer();
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = UnityIOC.Instance;
            RegisterTypes(container);
            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterInstance(GlobalConfiguration.Configuration);
            container.RegisterType<ILogger, Log4NetLoggerManager>();
            //container.RegisterType<IEmail, Email.Email>();
            ////container.RegisterType<ISms, Sms.Sms>();
            container.RegisterType<IDbContext, SampleEntities>();
            container.RegisterType<IGenericUnitOfWork, GenericUnitOfWork>();
            //container.RegisterType<ISharedManager, SharedManager>();
            //container.RegisterType<IHttpHandler, HttpHandleManager>();
            container.RegisterType<ICacheProvider, DefaultCacheProvider>();

            //#region Managers
            container.RegisterType<IUserManager, UserManager>();
            //container.RegisterType<ILookUpManager, LookUpManager>();
            //#endregion

        }
    }
}
