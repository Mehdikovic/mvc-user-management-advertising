using System.Web.Mvc;

using AppPortfolio.App_Start.Filters;

namespace AppPortfolio {
    public class FilterConfig {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new CustomErrorHandler());
        }
    }
}