using MvcUi.Controllers;
using Ninject;
using System;
using System.Web.Mvc;
using TeamProject.DAL;
using TeamProject.DAL.Entities;
using TeamProject.DAL.Repositories;
using TeamProject.DAL.Repositories.Interfaces;

namespace MvcUi.Infrastructure
{
    internal class NinjectControllerFactory : DefaultControllerFactory
    // TODO а можно этот клас использовать как dependency resolver и вообще зачем ресолвер нужен?
    {
        private IKernel kernel;
        private CinemaContext _DBContextCinema;
        public NinjectControllerFactory()
        {
            kernel = new StandardKernel();
            _DBContextCinema = new CinemaContext();
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
            //  kernel.Bind<IRepository<User>>().To<UserRepository>().WithConstructorArgument(_DBContextCinema);
            UnitOfWork unit = new UnitOfWork();
            var p = unit.Users;
            
        }
    }
}