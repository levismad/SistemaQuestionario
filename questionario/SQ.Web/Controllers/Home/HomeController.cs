using SQ.Web.Controllers.Base;
using SQ.Web.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ.Web.Controllers.Home
{
    [LoginFilter]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}
