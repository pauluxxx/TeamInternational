using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeamProject.DAL;
[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(MvcUi.App_Start.PreStartAppDBCreate), "Start")]
namespace MvcUi.App_Start
{
     public class PreStartAppDBCreate
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Метод запускается один раз перед стартом приложения        
        /// </summary>
        public static void Start()
        {
            logger.Info("Application PreStart");
        }
    }
}