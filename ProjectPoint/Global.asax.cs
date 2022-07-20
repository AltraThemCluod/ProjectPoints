using Autofac;
using ProjectPoint.App_Start;
using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ProjectPoint
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ModelBinders.Binders.DefaultBinder = new TupleModelBinder();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
            ModelMetadataProviders.Current = new CachedDataAnnotationsModelMetadataProvider();
            ContainerBuilder builder = new ContainerBuilder();

            Bootstrapper.Resolve(builder);
        }

        //Custom Model Binder
        public class TupleModelBinder : DefaultModelBinder
        {
            protected override object CreateModel(ControllerContext controllerContext,
                      ModelBindingContext bindingContext, Type modelType)
            {
                return base.CreateModel(controllerContext, bindingContext, modelType);
            }
        }
    }
}
