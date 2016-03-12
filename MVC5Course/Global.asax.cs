using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MVC5Course
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            /*
                預設會執行 WebFormViewEngine、RazorViewEngine
                清除掉 Engines後再加入 RazorViewEngine 即會址執行 RazorViewEngine            
            */
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

        }
    }
}
