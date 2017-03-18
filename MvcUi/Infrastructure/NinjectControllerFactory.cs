using MvcUi.Controllers;
using Ninject;
using System;
using System.Web.Mvc;

namespace MvcUi.Infrastructure
{
    internal class NinjectControllerFactory : DefaultControllerFactory
        // TODO а можно этот клас использовать как dependency resolver и вообще зачем ресолвер нужен?
    {
        private IKernel kernel;
        public NinjectControllerFactory()
        {
            kernel = new StandardKernel();
            AddBindings();
        }
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return controllerType == null
                ? null
                : (IController)kernel.Get(controllerType);
        }
        private void AddBindings()
        {
           
                kernel.Bind<IWeapon>().To<Bazuka>();
         
        }
    }
}