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
using QLEventKhoa.ViewModels;
using System.Data.Entity;
namespace QLEventKhoa.Controllers.Admin
{
    [AuthenticationFillter]
    [AuthorizeRole(Roles = "Admin Tổng")]
    public class QLKhoaController : Controller
    {
        dbEvent db = new dbEvent();
        // GET: QLKhoa
        public ActionResult Index()
        {
            return View();
        }
        // lấy danh sách khoa
        public ActionResult ListKhoa()
        {
            return View(db.tbKhoas.ToList());
        }
       
        public JsonResult Addkhoa( string a_falcode, string a_falname)
        {
            var check = db.tbKhoas.FirstOrDefault(p => p.maKHoa.Equals(a_falcode));
            try
            {
                if (check == null)
                {
                    var falnew = new tbKhoa();
                    falnew.maKHoa = a_falcode;
                    falnew.tenKhoa = a_falname;
                    falnew.statusKhoa = true;
                    db.tbKhoas.Add(falnew);
                    db.SaveChanges();
                    return Json(new {code = 200 , idkhoa = falnew.idKhoa }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { code = 500 , msg = "Mã khoa này đã tồn tại !!"}, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json("",JsonRequestBehavior.DenyGet);
            }
           
        }

        public JsonResult Editkhoa(int id, string e_fname, bool e_fstt)
        {
            var edk = db.tbKhoas.FirstOrDefault(p => p.idKhoa == id);
            if (edk != null)
            {
                if (edk.tbLopHocs.Where(p => p.statusLopHoc == true).ToList().Count != 0 && e_fstt == false)
                {
                    return Json("cantremove", JsonRequestBehavior.AllowGet);
                }
                else if (!edk.tenKhoa.Equals(e_fname) || edk.statusKhoa != e_fstt)
                {
                    edk.tenKhoa = e_fname;
                    edk.statusKhoa = e_fstt;
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
                return Json("Không có gì được thay đổi cả !", JsonRequestBehavior.DenyGet);
            }
        }


    }
}