using MvcUi.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MvcUi
{
    //нужно ли самому настраиваить injects для NinjectHttpModule,OnePerRequestHttpModule,Bootstrapper 
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        ///NLog comit a log into system directiry it's have no need to be injected
        /// </summary>
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        protected void Application_Start()
        {
            logger.Info("Application Start");
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
        }
        public override void Init()
        {
            logger.Info("Application Init");
        }
        public override void Dispose()
        {
            logger.Info("Application Dispose");
        }
        protected void Application_Error()
        {
            logger.Info("Application Error");
        }
        protected void Application_End()
        {
            logger.Info("Application End");
        }
    }
}
