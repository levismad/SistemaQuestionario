using SQ.Web.Controllers.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace SQ.Web.Filter
{
    public class LoginFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = filterContext.HttpContext.Session.Contents["Usuario"];
            if (session == null && !(filterContext.Controller is LoginController))
            {
                bool xmlRequest = filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
                if (!xmlRequest)
                {
                    filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary{
                        {"controller" ,"Login"},
                        {"action", "SessaoError"}
                    }
                );
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary{
                        {"controller" ,"Login"},
                        {"action", "SessaoErrorAjax"}
                    }
                );
                }


            }
        }
    }
}
