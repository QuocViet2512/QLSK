using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Collections.Concurrent;
using QLEventKhoa.Models;
using QLEventKhoa.ViewModels;

namespace QLEventKhoa.Controllers.Admin
{
    [AuthenticationFillter]
    public class HomeADController : Controller
    {
        dbEvent db = new dbEvent();
        // GET: HomeAD

     
        public ActionResult IndexAD()
        {

            return View();
        }

        public ActionResult Unauthorized()
        {
            ViewBag.message = "Không có quyền truy cập";
            return View();
        }

    }
}