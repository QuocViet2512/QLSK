using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLEventKhoa.ViewModels
{
    public class SVSessionTimeOut : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            if (HttpContext.Current.Session["LogIn"] == null)
            {
                filterContext.Result = new RedirectResult("~/AccountSV/LoginSV");
                return;
            }
            base.OnResultExecuting(filterContext);
        }
    }
}