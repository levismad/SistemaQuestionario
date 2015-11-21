using SQ.Services.App_Start;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace SQ.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_Begin_Request(Object sender, EventArgs e)
        {
            CultureInfo culture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            culture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            culture.DateTimeFormat.DateSeparator = "/";
            Thread.CurrentThread.CurrentCulture = culture;
            if (!FirstRequest.initialized)
            {
                FirstRequest.Initialize(((HttpApplication)sender).Context);
            }

        }

        public class FirstRequest
        {
            public static bool initialized = false;
            private static Object s_lock = new Object();

            public static void Initialize(HttpContext context) {
                if (!initialized)
                {
                    lock (s_lock)
                    {
                        if (!initialized)
                        {
                            var host = HttpContext.Current.Request.Url.Host;
                            var connectionString = ConfigurationManager.ConnectionStrings[String.Format("connection_{0}", host)].ConnectionString;
                            (new AppHost(connectionString)).Init();
                        }
                    }
                }
            }
        }
    }
}