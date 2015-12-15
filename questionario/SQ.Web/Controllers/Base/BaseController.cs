using SQ.Core.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ.Web.Controllers.Base
{
    [HandleError]
    public class BaseController : Controller
    {
        protected MUsuario Usuario { get { return GetUsuarioSession(); } }
        protected void SetUsuarioSession(MUsuario u)
        {
            Session["Usuario"] = u;
        }
        protected MUsuario GetUsuarioSession()
        {
            var session = HttpContext.Session.Contents["Usuario"] as MUsuario;
            return session;
        }
        protected void FinalizaSessao()
        {
            HttpContext.Session.Abandon();
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            bool xmlRequest = false;
            if (base.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                xmlRequest = true;
            }
            else
            {
                ViewBag.MessageError = filterContext.Exception.Message;
                ViewBag.StackTrace = filterContext.Exception.StackTrace;
                filterContext.ExceptionHandled = true;
                //if (xmlRequest)
                //    filterContext.Result = PartialView("Error");
                //else
                filterContext.Result = View("Error");

            }


        }
    }

}
