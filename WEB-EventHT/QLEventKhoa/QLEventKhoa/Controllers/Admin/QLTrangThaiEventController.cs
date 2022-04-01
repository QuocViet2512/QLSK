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
    public class QLTrangThaiEventController : Controller
    {
        // GET: QLTrangThaiEvent
        dbEvent db = new dbEvent();

        public ActionResult IndexTrangThaiEvent()
        {
            return View(db.tbTrangThaiEvents.ToList());
        }

        [HttpGet]
        public ActionResult addTrangThaiSK()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult addTrangThaiSK(tbTrangThaiEvent stas)
        {
            var check = db.tbTrangThaiEvents.FirstOrDefault(a => a.tenTrangThaiEvent.Equals(stas.tenTrangThaiEvent));
            if (ModelState.IsValid)
            {
                if (check == null)
                {
                    tbTrangThaiEvent tbe = new tbTrangThaiEvent();
                    tbe.tenTrangThaiEvent = stas.tenTrangThaiEvent;
                    tbe.statusTTE = true;
                    db.tbTrangThaiEvents.Add(tbe);
                    db.SaveChanges();
                    return RedirectToAction("IndexTrangThaiEvent");
                }
                else
                {
                    TempData["Temp"] = "Trạng thái đã tồn tại";
                    return RedirectToAction("IndexTrangThaiEvent");
                }
            }
            return View("IndexTrangThaiEvent");
        }

  
        public ActionResult TrangThaiEvent(int id)
        {
            tbTrangThaiEvent tbe = db.tbTrangThaiEvents.FirstOrDefault(a => a.statusEvent == id);
            if (tbe.statusTTE == true)
            {
                tbe.statusTTE = false;
            }
            else
            {
                tbe.statusTTE = true;
            }
            db.Entry(tbe).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("IndexTrangThaiEvent");
        }

        [HttpGet]
        public ActionResult editTrangThaiSK(int id)
        {
            tbTrangThaiEvent tbe = db.tbTrangThaiEvents.Find(id);
            return PartialView(tbe);
        }
        [HttpPost]
        public ActionResult editTrangThaiSK(tbTrangThaiEvent tbe)
        {
            var check = db.tbTrangThaiEvents.FirstOrDefault(a => a.tenTrangThaiEvent.Equals(tbe.tenTrangThaiEvent));
            if (ModelState.IsValid)
            {
                if (check == null)
                {
                    tbe.statusTTE = true;
                    db.Entry(tbe).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("IndexTrangThaiEvent");
                }
                else
                {
                    TempData["Temp"] = "Không có cập nhập gì mới";
                    return RedirectToAction("IndexTrangThaiEvent");
                }
            }
            return View("IndexTrangThaiEvent");
        }
    }
}