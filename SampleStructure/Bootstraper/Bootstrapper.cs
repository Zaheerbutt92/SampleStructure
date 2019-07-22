using Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
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
            //container.RegisterType<ILogger, Log4NetLoggerManager>();
            //container.RegisterType<IEmail, Email.Email>();
            ////container.RegisterType<ISms, Sms.Sms>();
            //container.RegisterType<ILCDbContext, LC_DEVEntities>();
            //container.RegisterType<IGenericUnitOfWork, GenericUnitOfWork>();
            //container.RegisterType<ISharedManager, SharedManager>();
            //container.RegisterType<IHttpHandler, HttpHandleManager>();
            container.RegisterType<ICacheProvider, DefaultCacheProvider>();

            //#region Managers
            //container.RegisterType<ILegalConsultancyManager, LegalConsultancyManager>();
            //container.RegisterType<ILookUpManager, LookUpManager>();
            //#endregion

        }
    }
}
