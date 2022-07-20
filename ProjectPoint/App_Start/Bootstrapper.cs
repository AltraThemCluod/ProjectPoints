using Autofac;
using Autofac.Integration.Mvc;
using ServicesLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectPoint.App_Start
{
    public sealed class Bootstrapper
    {
        public static void Resolve(ContainerBuilder builder)
        {
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModule<ServiceModule>();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}