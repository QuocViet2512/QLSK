using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLEventKhoa.Models;
using QLEventKhoa.ViewModels;
using BC = BCrypt.Net.BCrypt;
using System.Data.Entity;
using System.Web.Security;

namespace QLEventKhoa.Controllers.SinhVien
{
    public class AccountSVController : Controller
    {
        dbEvent db = new dbEvent();
        // GET: AccountSV
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoginSV()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginSV(tbTaiKhoanSV tk)
        {
            if (ModelState.IsValid)
            {
                var acc = db.tbTaiKhoanSVs.SingleOrDefault(a => a.maSV.Equals(tk.maSV) && a.statusAccSV == true);

                if (acc != null)
                {
                    bool isvalidPass = BC.Verify(tk.matKhauSV, acc.matKhauSV);
                    if (isvalidPass)
                    {
                        Session["LogIn"] = acc;
                        Session["SV"] = acc.tbSinhVien;
                        Session["nameSV"] = acc.tbSinhVien.tenSV;
                        return RedirectToAction("IndexSV", "HomeSV");
                    }
                    ViewBag.ThongBao = "Mã số sinh viên hoặc mật khẩu không đúng";
                    return View();
                }
            }
            ViewBag.ThongBao = "Mã số sinh viên hoặc mật khẩu không đúng";
            return View();
        }

        /// <summary>
        /// ////////////////////////////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        /// 

        [HttpGet]
        public ActionResult RegisterSV()
        {
            DangKySVViewModels dk = new DangKySVViewModels();
            dk.listKhoa = db.tbKhoas.Where(a => a.statusKhoa == true).ToList();
            //dk.listLop = db.tbLopHocs.Where(a=>a.statusLopHoc==true).ToList();
            List<tbLopHoc> LopHoc = db.tbLopHocs.Where(a => a.statusLopHoc == true).ToList();
            dk.LopHoc = new SelectList(LopHoc, "idLopHoc", "tenLopHoc");
            return View(dk);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterSV(DangKySVViewModels dk,FormCollection form)
        {
            var lophoc = form["idLopHoc"];
            if (ModelState.IsValid)
            {
                var check = db.tbTaiKhoanSVs.FirstOrDefault(a => a.maSV.Equals(dk.maSV));
                var find = db.tbLopHocs.FirstOrDefault(a => a.tenLopHoc.Equals(lophoc));
                if (check == null && find !=null)
                {
                    tbSinhVien sv = new tbSinhVien();
                    sv.maSV = dk.maSV;
                    sv.tenSV = dk.tenSV;
                    sv.emailSV = dk.emailSV;
                    sv.phoneSV = dk.phoneSV;
                    sv.ngaySinhSV = dk.ngaySinhSV;
                    sv.idKhoa = dk.idKhoa;
                    sv.idLopHoc = find.idLopHoc;
                    sv.statusSV = true;
                    sv.isLopTruong = false;
                    db.tbSinhViens.Add(sv);
                    db.SaveChanges();

                    tbTaiKhoanSV tk = new tbTaiKhoanSV();
                    tk.idAccSV = sv.idSinhVien;
                    tk.maSV = sv.maSV;
                    tk.matKhauSV = BC.HashPassword(dk.matKhauSV);
                    tk.statusAccSV = true;
                    db.tbTaiKhoanSVs.Add(tk);
                    db.SaveChanges();

                    return RedirectToAction("indexSV", "HomeSV");
                }
                else
                {                  
                    TempData["Temp"] = "Mã sinh viên đã tồn tại";
                    return RedirectToAction("RegisterSV");
                }
            }        
            return RedirectToAction("RegisterSV");
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("IndexSV", "HomeSV");
        }


        [HttpGet]
        [SVSessionTimeOut]
        public ActionResult ProfileSV()
        {
            tbSinhVien sv = (tbSinhVien)Session["SV"];
            return View(sv);
        }

        [HttpPost]
        public ActionResult ProfileSV(tbSinhVien svv)
        {
            tbSinhVien sv = (tbSinhVien)Session["SV"];
            var edit = db.tbSinhViens.FirstOrDefault(a => a.idSinhVien == sv.idSinhVien);

            if (ModelState.IsValid)
            {
                edit.statusSV = true;
                edit.emailSV = svv.emailSV;
                edit.phoneSV = svv.phoneSV;
                db.Entry(edit).State = EntityState.Modified;
                db.SaveChanges();
                Session["SV"] = edit;
            }
            return RedirectToAction("ProfileSV", "AccountSV");
        }

        [HttpGet]
        [SVSessionTimeOut]
        public ActionResult ChangePass()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePass(ChangePassSVViewModels cp)
        {
            tbSinhVien sv = (tbSinhVien)Session["SV"];

            if (ModelState.IsValid)
            {
                if (cp.matKhauSV == cp.newPassword)
                {                                 
                    ModelState.AddModelError("", "Mật khẩu mới trùng với mật khẩu hiện tại"); ;
                    return View();
                }
                else
                {
                    var acc = db.tbTaiKhoanSVs.FirstOrDefault(a => a.idAccSV == sv.idSinhVien);
                    if (acc != null)
                    {
                        acc.matKhauSV = BC.HashPassword(cp.newPassword);
                        db.Entry(acc).State = EntityState.Modified;
                        db.SaveChanges();
                        Session.Clear();
                        return RedirectToAction("LoginSV", "AccountSV");
                    }
                }
            }
            return View();
        }

    }
}