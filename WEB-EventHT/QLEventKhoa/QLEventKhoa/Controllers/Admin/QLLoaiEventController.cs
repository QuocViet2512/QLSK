using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Collections.Concurrent;
using QLEventKhoa.Models;
using System.Data.Entity;
using QLEventKhoa.ViewModels;
namespace QLEventKhoa.Controllers.Admin
{
    [AuthenticationFillter]
    [AuthorizeRole(Roles = "Admin Tổng")]
    public class QLLoaiEventController : Controller
    {
        dbEvent db = new dbEvent();
        // GET: QLLoaiEvent
        public ActionResult IndexLoaiEvent()
        {
            return View(db.tbLoaiEvents.ToList());
        }

        [HttpGet]
        public ActionResult addLoaiSK()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult addLoaiSK(tbLoaiEvent tle)
        {
            var check = db.tbLoaiEvents.SingleOrDefault(a => a.tenLoaiEvent.Equals(tle.tenLoaiEvent));
            if (ModelState.IsValid)
            {
                if (check == null)
                {
                    tbLoaiEvent tbe = new tbLoaiEvent();
                    tbe.tenLoaiEvent = tle.tenLoaiEvent;
                    tbe.statusLoaiEvent = true;
                    db.tbLoaiEvents.Add(tbe);
                    db.SaveChanges();
                    return RedirectToAction("IndexLoaiEvent", "QLLoaiEvent");
                }
                else
                {
                    TempData["Temp"] = "Loại sự kiện đã tồn tại";
                    return RedirectToAction("IndexLoaiEvent");
                }
            }
            return View("IndexLoaiEvent");
        }

        public ActionResult TrangThaiLoaiSK(int id)
        {
            tbLoaiEvent tbe = db.tbLoaiEvents.FirstOrDefault(a => a.idLoaiEvent == id);
            if (tbe.statusLoaiEvent == true)
            {
                tbe.statusLoaiEvent = false;
            }
            else
            {
                tbe.statusLoaiEvent = true;
            }
            db.Entry(tbe).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("IndexLoaiEvent");
        }

        [HttpGet]
        public ActionResult editLoaiSK(int id)
        {
            tbLoaiEvent tbe = db.tbLoaiEvents.Find(id);
            return PartialView(tbe);
        }
        [HttpPost]
        public ActionResult editLoaiSK(tbLoaiEvent tbe)
        {
            var check = db.tbLoaiEvents.FirstOrDefault(a => a.tenLoaiEvent.Equals(tbe.tenLoaiEvent));
            if (ModelState.IsValid)
            {
                if (check == null)
                {
                    tbe.statusLoaiEvent = true;
                    db.Entry(tbe).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("IndexLoaiEvent");
                }
                else
                {
                    TempData["Temp"] = "Không có cập nhập gì mới";
                    return RedirectToAction("IndexLoaiEvent");
                }
            }
            return View("IndexLoaiEvent");
        }
    }
}
