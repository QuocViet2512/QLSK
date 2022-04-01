using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Concurrent;
using QLEventKhoa.Models;
using System.Data.Entity;
using QLEventKhoa.ViewModels;

namespace QLEventKhoa.Controllers.Admin
{
    [AuthenticationFillter]
    [AuthorizeRole(Roles = "Admin Tổng")]
    public class QLAdminController : Controller
    {
        // GET: QLLoaiAD
        dbEvent db = new dbEvent();
        EncryptMD5 MD5 = new EncryptMD5();
    
        public ActionResult IndexLoaiAD()
        {
            return View(db.tbLoaiAdmins.ToList());
        }
        [HttpGet]
        public ActionResult addLoaiAD()
        {
            return PartialView();
        }


        [HttpPost]
        public ActionResult addLoaiAD(tbLoaiAdmin tad)
        {
            var check = db.tbLoaiAdmins.FirstOrDefault(a => a.tenLoaiAD.Equals(tad.tenLoaiAD));
            if (ModelState.IsValid)
            {
                if (check == null)
                {
                    tbLoaiAdmin tbe = new tbLoaiAdmin();
                    tbe.tenLoaiAD = tad.tenLoaiAD;
                    tbe.statusLAD = true;
                    db.tbLoaiAdmins.Add(tbe);
                    db.SaveChanges();
                    return RedirectToAction("IndexLoaiAD");
                }
                else
                {
                    TempData["Temp"] = "Loại Admin đã tồn tại";
                    return RedirectToAction("IndexLoaiAD");
                }
            }
            return RedirectToAction("IndexLoaiAD");
        }

    
        public ActionResult statusLoaiAD(int id)
        {
            tbLoaiAdmin tbe = db.tbLoaiAdmins.SingleOrDefault(a => a.idLoaiAD == id);
            if (tbe.statusLAD == true)
            {
                tbe.statusLAD = false;
            }
            else
            {
                tbe.statusLAD = true;
            }
            db.Entry(tbe).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("IndexLoaiAD");
        }

        [HttpGet]
        public ActionResult editLoaiAD(int id)
        {
            tbLoaiAdmin tla = db.tbLoaiAdmins.Find(id);
            return PartialView(tla);
        }
        [HttpPost]
        public ActionResult editLoaiAD(tbLoaiAdmin tla)
        {
            var check = db.tbLoaiAdmins.FirstOrDefault(a => a.tenLoaiAD.Equals(tla.tenLoaiAD));
            if (ModelState.IsValid)
            {
                if (check == null)
                {
                    tla.statusLAD = true;
                    db.Entry(tla).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("IndexLoaiAD");
                }
                else
                {
                    TempData["Temp"] = "Không có cập nhập gì mới";
                    return RedirectToAction("IndexLoaiAD");
                }
            }
            return RedirectToAction("IndexLoaiAD");
        }


        public ActionResult DSAdmin()
        {
            DSAdminViewModels ad = new DSAdminViewModels();
            ad.listAdmin = db.tbAdmins.ToList();
            ad.listLoaiAD = db.tbLoaiAdmins.ToList();
            return View(ad);
        }

        public JsonResult EditAD(int id,string e_adusername,string e_admail,int e_adtype,bool e_adstt)
        {
            var edad = db.tbAdmins.FirstOrDefault(p => p.idAdmin == id);
            if (edad != null )
            {
                if (!edad.userAdmin.Equals(e_adusername) || !edad.emailAD.Equals(e_admail) || edad.idloaiql != e_adtype || edad.statusAd != e_adstt)
                {
                    edad.userAdmin =e_adusername ;
                    edad.emailAD =e_admail ;
                    edad.idloaiql = e_adtype;
                    edad.statusAd = e_adstt;
                    db.SaveChanges();
                    return Json("true", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("false", JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json( JsonRequestBehavior.DenyGet);
            }
        }

        public JsonResult ResetPassAD(int id)
        {
            int a = id;
            var find = db.tbAdmins.FirstOrDefault(p => p.idAdmin == id);
            if (find != null)
            {
                find.passAdmin = MD5.Encrypt(find.userAdmin);
                db.SaveChanges();
                return Json("Đặt lại mật khẩu thành công !", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Xử lý không thành công !", JsonRequestBehavior.DenyGet);
            }
        }

    }
}