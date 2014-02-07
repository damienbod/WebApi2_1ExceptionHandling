using System.Web;
using System.Web.Mvc;

namespace WebApi2_1ExceptionHandling
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
