using System.Web;
using System.Web.Mvc;
using SisATU.Util;

namespace SisATU.WebUI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new VerificarAcceso(), 1);
            filters.Add(new HandleErrorAttribute());
        }
    }
}
