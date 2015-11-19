using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SQ.Web.Helpers
{
    public class ZipResultFile : ActionResult
    {
        public ZipFile zip { get; set; }
        public string fileName { get; set; }
        public ZipResultFile(ZipFile zip, string name)
        {
            this.zip = zip;
            this.fileName = name;
        }
        public override void ExecuteResult(ControllerContext context)
        {
            var Response = context.HttpContext.Response;
            Response.ContentType = "application/zip´";
            Response.AddHeader("content-disposition",
            "attachment; filename=" + this.fileName +
            DateTime.Now.ToString("yyyyMMdd-hhmmss") +
           ".zip");
            zip.Save(Response.OutputStream);
            Response.End();
        }
    }

}
