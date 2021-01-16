using FluentValidation.Mvc;
using LinkyMVC.Validation;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace LinkyMVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            FluentValidationModelValidatorProvider.Configure(cfg => 
            {
                cfg.ValidatorFactory = new ValidatorFactory();
            });
        }
    }
}
