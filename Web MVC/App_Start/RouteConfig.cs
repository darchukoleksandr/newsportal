using System.Web.Mvc;
using System.Web.Routing;

namespace Web_MVC
{
    public class RouteConfig
    {
        public MvcApplication MvcApplication
        {
            get => default(MvcApplication);
            set
            {
            }
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

//            routes.MapRoute(
//                name: "User", 
//                url: "User/{id}",
//                defaults: new { controller = "User", action = "Index"}
////                defaults: new { controller = "User", action = "Details", id = UrlParameter.Optional }
//            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
