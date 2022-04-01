using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLEventKhoa.Models;
using QLEventKhoa.ViewModels;

namespace QLEventKhoa.Controllers.Admin
{
    public class ThongkeController : Controller
    {
        dbEvent db = new dbEvent();
        public ActionResult Thongke()
        {
            Thongketop10 tktop = new Thongketop10();
            return View(tktop);
        }
       
    }
}