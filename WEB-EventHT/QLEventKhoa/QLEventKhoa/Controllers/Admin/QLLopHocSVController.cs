using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLEventKhoa.ViewModels;
using QLEventKhoa.Models;
using System.Data.Entity;

namespace QLEventKhoa.Controllers.Admin
{
    [AuthenticationFillter]
    [AuthorizeRole(Roles = "Admin Tổng")]
    public class QLLopHocSVController : Controller
    {
        dbEvent db = new dbEvent();
        // GET: QLLopHocSV
        public ActionResult IndexLopHocSV(int? falid)
        {
            listclass dsLH = new listclass();
            if (falid == null)
            {
                dsLH.dsLop = db.tbLopHocs.OrderBy(p => p.idLopHoc).ToList();
            }
            else
            {
                dsLH.dsLop = db.tbLopHocs.Where(p => p.idKhoa == falid).OrderByDescending(p => p.idLopHoc).ToList();
            }                 
            dsLH.dsKhoa = db.tbKhoas.Where(p=>p.statusKhoa==true).ToList();
            return View(dsLH);
        }

        public JsonResult AddCLASS(string a_classname,int a_classfal,int a_classin,int a_classout )
        {
            var check = db.tbLopHocs.FirstOrDefault(p => p.tenLopHoc.Equals(a_classname));
            try
            {
                if (check == null)
                {
                    var classnew = new tbLopHoc();
                    classnew.tenLopHoc = a_classname;
                    classnew.idKhoa = a_classfal;
                    classnew.namBatDau = a_classin;
                    classnew.namKetThuc = a_classout;
                    classnew.statusLopHoc = true;
                    db.tbLopHocs.Add(classnew);
                    db.SaveChanges();
                    var lkhoa = db.tbKhoas.Select(p => new { idkhoa = p.idKhoa, tenkhoa = p.tenKhoa,stt=p.statusKhoa }).Where(p=>p.stt==true).ToList();
                    return Json(new { code = 200, idclass = classnew.idLopHoc, lstfal = lkhoa  }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { code = 500, msg = "Mã khoa này đã tồn tại !!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json("", JsonRequestBehavior.DenyGet);
            }
        }

        public JsonResult EditLH(int id, string e_classname,int e_classfal, int e_classin,int e_classout,bool e_classstt )
        {
            var edlh = db.tbLopHocs.FirstOrDefault(p => p.idLopHoc == id);
            if (edlh != null)
            {
                if ( edlh.tbSinhViens.Where(p => p.statusSV == true).ToList().Count != 0 && e_classstt == false)
                {
                    return Json("cantremove", JsonRequestBehavior.AllowGet);
                }
                else if (!edlh.tenLopHoc.Equals(e_classname) || edlh.namBatDau!=e_classin || edlh.namKetThuc != e_classout || edlh.idKhoa != e_classfal||edlh.statusLopHoc!=e_classstt)
                {
                    edlh.tenLopHoc = e_classname;
                    edlh.idKhoa = e_classfal;
                    edlh.namBatDau = e_classin;
                    edlh.namKetThuc = e_classout;
                    edlh.statusLopHoc = e_classstt;
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
