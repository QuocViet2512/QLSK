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
    public class QLTagEventController : Controller
    {
        // GET: QLTagEvent

        dbEvent db = new dbEvent();
        public ActionResult IndexTagEvent()
        {
            return View(db.tbTagEvents.ToList());
        }

        //Thêm TAG
        [HttpGet]
        public ActionResult AddTag()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult AddTag(tbTagEvent tag )
        {
            var check = db.tbTagEvents.SingleOrDefault(a => a.tenTagEvent.Equals(tag.tenTagEvent));
            if (ModelState.IsValid)
            {
                if (check == null)
                {
                    tbTagEvent tbe = new tbTagEvent();
                    tbe.tenTagEvent = tag.tenTagEvent;
                    tbe.statusTagEvent = true;
                    db.tbTagEvents.Add(tbe);
                    db.SaveChanges();
                    return RedirectToAction("IndexTagEvent", "QLTagEvent");
                }
                else
                {
                    TempData["Temp"] = "Tag sự kiện đã tồn tại";
                    return RedirectToAction("IndexTagEvent");
                }
            }
            return View("IndexTagEvent");
        }

  
        public ActionResult TrangThaiTAG(int id)
        {
            tbTagEvent tbt = db.tbTagEvents.SingleOrDefault(a => a.idTagEvent == id);
            if (tbt.statusTagEvent == true)
            {
                tbt.statusTagEvent = false;
            }
            else
            {
                tbt.statusTagEvent = true;
            }
            db.Entry(tbt).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("IndexTagEvent");
        }

        [HttpGet]
        public ActionResult EditTAG(int id)
        {
            tbTagEvent tbt = db.tbTagEvents.Find(id);
            return PartialView(tbt);
        }
        [HttpPost]
        public ActionResult EditTAG(tbTagEvent tbt)
        {
            var check = db.tbTagEvents.FirstOrDefault(a => a.tenTagEvent.Equals(tbt.tenTagEvent));
            if (ModelState.IsValid)
            {
                if (check == null)
                {
                    tbt.statusTagEvent = true;
                    db.Entry(tbt).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("IndexTagEvent");
                }
                else
                {
                    TempData["Temp"] = "Không có cập nhập gì mới";
                    return RedirectToAction("IndexTagEvent");
                }
            }
            return View("IndexTagEvent");
        }
    }
}