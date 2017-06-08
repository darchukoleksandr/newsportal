using System.Web.Mvc;

namespace Web_MVC
{
    public class FilterConfig
    {
        public MvcApplication MvcApplication
        {
            get => default(MvcApplication);
            set
            {
            }
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
