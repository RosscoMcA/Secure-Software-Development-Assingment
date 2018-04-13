using SecureSoftwareApplication.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SecureSoftwareApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        void Application_Error(object sender, EventArgs e)
        {
            if (typeof(System.Web.HttpRequestValidationException) == Server.GetLastError().GetType() || 
                typeof(System.Web.HttpException) == Server.GetLastError().GetType())
            {

                Response.Status = "403 Action forbidden";
                Response.StatusCode = 403;
                Response.End();

                

            }

        }
    }
}
