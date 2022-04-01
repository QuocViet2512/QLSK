using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLEventKhoa.Models;
using System.Data.Entity;
using QLEventKhoa.ViewModels;
using System.Net.Mail;
using System.Net;


namespace QLEventKhoa.Controllers.Admin
{
    [AuthenticationFillter]
    public class QLLienHeController : Controller
    {
        // GET: QLLoaiLH
        dbEvent db = new dbEvent();


        [AuthorizeRole(Roles = "Admin Tổng")]
        public ActionResult IndexLoaiLH()
        {
            return View(db.tbLoaiLHs.ToList());
        }

        public ActionResult addLoaiLH()
        {
            return PartialView();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [AuthorizeRole(Roles = "Admin Tổng")]
        public ActionResult addLoaiLH(tbLoaiLH tad )
        {
            var check = db.tbLoaiLHs.FirstOrDefault(a => a.tenLoaiLH.Equals(tad.tenLoaiLH));
            if (ModelState.IsValid)
            {
                if (check == null)
                {
                    tbLoaiLH tbe = new tbLoaiLH();
                    tbe.tenLoaiLH = tad.tenLoaiLH;
                    tbe.statusLoaiLH = true;
                    db.tbLoaiLHs.Add(tbe);
                    db.SaveChanges();
                    return RedirectToAction("IndexLoaiLH");
                }
                else
                {
                    TempData["Temp"] = "Loại liên hệ đã tồn tại";
                    return RedirectToAction("IndexLoaiLH");
                }
            }
            return View("IndexLoaiLH");
        }


        [AuthorizeRole(Roles = "Admin Tổng")]
        public ActionResult TrangThaiLoaiLH(int id)
        {
            tbLoaiLH tbe = db.tbLoaiLHs.FirstOrDefault(a => a.idLoaiLH == id);
            if (tbe.statusLoaiLH == true)
            {
                tbe.statusLoaiLH = false;
            }
            else
            {
                tbe.statusLoaiLH = true;
            }
            db.Entry(tbe).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("IndexLoaiLH");
        }

        [HttpGet]
        [AuthorizeRole(Roles = "Admin Tổng")]
        public ActionResult editLoaiLH(int id)
        {
            tbLoaiLH tbe = db.tbLoaiLHs.Find(id);
            return PartialView(tbe);
        }
        [HttpPost]
        [AuthorizeRole(Roles = "Admin Tổng")]
        public ActionResult editLoaiLH(tbLoaiLH tla)
        {
            var check = db.tbLoaiLHs.FirstOrDefault(a => a.tenLoaiLH.Equals(tla.tenLoaiLH));
            if (ModelState.IsValid)
            {
                if (check == null)
                {
                    tla.statusLoaiLH = true;
                    db.Entry(tla).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("IndexLoaiLH");
                }
                else
                {
                    TempData["Temp"] = "Không có cập nhập gì mới";
                    return RedirectToAction("IndexLoaiLH");
                }
            }
            return View("IndexLoaiLH");
        }


        /// <summary>
        /// //////////
        /// </summary>
        /// <returns></returns>
        public ActionResult DSPhanHoi()
        {
            var listPH = db.tbLienHes.ToList();
            return View(listPH);
        }

        public ActionResult PhanHoiLH(int id)
        {
            var ph = db.tbLienHes.FirstOrDefault(a => a.idLienHe == id);

            return View(ph);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult PhanHoiLH(tbLienHe lh)
        {
            tbAdmin ad = (tbAdmin)Session["LogAD"];
            lh.idAdmin = ad.idAdmin;
            lh.userAdmin = ad.userAdmin;
            lh.emailAD = ad.emailAD;
            lh.statusLH = true;
            if (ModelState.IsValid)
            {
                db.Entry(lh).State = EntityState.Modified;
                db.SaveChanges();

                var message = new MailMessage();
                message.To.Add(new MailAddress(lh.emailSV));
                message.Subject = lh.tieuDeLH;
                message.Body = lh.noiDungPH;
                message.IsBodyHtml = true;
                using (var smtp = new SmtpClient())
                {
                    smtp.Send(message);
                }
                return RedirectToAction("DSPhanHoi");
            }
            return View();
        }
    }
}