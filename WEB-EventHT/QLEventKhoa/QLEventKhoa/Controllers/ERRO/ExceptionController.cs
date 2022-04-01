using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLEventKhoa.Controllers.ERRO
{
    public class ExceptionController : Controller
    {
        // GET: Exception
        public ActionResult notfound404()
        {
            return View();
        }
    }
}