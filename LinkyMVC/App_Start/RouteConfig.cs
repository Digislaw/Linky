using System.Web.Mvc;
using System.Web.Routing;

namespace LinkyMVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                constraints: new { controller = "Home|Account|Links|Manage" }
            );

            routes.MapRoute(
                name: "Redirect",
                url: "{*url}",
                defaults: new { controller = "Links", action = "RedirectLink" }
            );
        }
    }
}
