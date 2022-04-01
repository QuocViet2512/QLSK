using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BC = BCrypt.Net.BCrypt;
using QLEventKhoa.Models;
using QLEventKhoa.ViewModels;

namespace QLEventKhoa.Controllers.Admin
{
    [AuthenticationFillter]
    [AuthorizeRole(Roles = "Admin Tổng")]
    public class QLTaiKhoanSVController : Controller
    {
        // GET: QLTaiKhoanSV
        dbEvent db = new dbEvent();
        public ActionResult QLTKSV()
        {
            var accounts = db.tbTaiKhoanSVs.ToList();
            return View(accounts);
        }

        public ActionResult Changestt(int id)
        {
            var find = db.tbTaiKhoanSVs.FirstOrDefault(p => p.idAccSV == id);
            if (find != null)
            {
                find.statusAccSV = (find.statusAccSV == true ? false : true);
                db.SaveChanges();
            }
            else
            {
                TempData["error"] = "Khong tim thay sinh vien nay";
            }
            return RedirectToAction("QLTKSV");
        }

        public JsonResult resetpass(int id)
        {
            int a = id;
            var find = db.tbTaiKhoanSVs.FirstOrDefault(p => p.idAccSV == id);
            if (find != null)
            {
                find.matKhauSV = BC.HashPassword(find.maSV);
                db.SaveChanges();
                return Json("Đặt lại mật khẩu thành công !", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Xử lý không thành công !", JsonRequestBehavior.DenyGet);
            }
        }


        //---------------------------------------------Quan lis sinh vieen -------------------------------------//
        public ActionResult QLSV(int? classid)
        {
            var sts = new listsv();
            if (classid == null )
            {
                sts.lstst = db.tbSinhViens.ToList();
            }
            else
            {
                sts.lstst = db.tbSinhViens.Where(p=>p.idLopHoc==classid).ToList();
            }
            sts.lstfal = db.tbKhoas.Where(p=>p.statusKhoa==true).ToList();
            return View(sts);
        }
        public JsonResult editST(int id,string e_stcode,string e_stname,DateTime e_stbd,string e_stmail,string e_stphone,string e_stclass,int e_stfal, bool e_ststt )
        {
            var edst = db.tbSinhViens.SingleOrDefault(p => p.idSinhVien == id);
            var classid = db.tbLopHocs.SingleOrDefault(p => p.tenLopHoc.Equals(e_stclass)&&p.idKhoa==e_stfal);           
            try
            {
                if (edst != null)
                {
                    if (classid == null)
                    {
                        return Json("dontexist", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        if (!edst.maSV.Equals(e_stcode) || !edst.tenSV.Equals(e_stname) || !edst.ngaySinhSV.Equals(e_stbd) || !edst.emailSV.Equals(e_stmail) || !edst.phoneSV.Equals(e_stphone) || edst.idLopHoc != classid.idLopHoc || edst.idKhoa != e_stfal || edst.statusSV != e_ststt)
                        {
                            edst.maSV = e_stcode;
                            edst.tenSV = e_stname;
                            edst.ngaySinhSV = e_stbd;
                            edst.emailSV = e_stmail;
                            edst.phoneSV = e_stphone;
                            edst.idLopHoc = classid.idLopHoc;
                            edst.idKhoa = e_stfal;
                            edst.statusSV = e_ststt;
                            var svtg = db.tbThamGiaEvents.Where(p => p.idSinhVien == id).ToList();
                            foreach (var item in svtg)
                            {
                                item.tenSV = e_stname;
                                item.emailSV = e_stmail;
                            }
                           
                            db.SaveChanges();
                            return Json("true", JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json("false", JsonRequestBehavior.AllowGet);
                        }
                    }                   
                }
                else
                {
                    return Json("Không có gì được thay đổi cả !", JsonRequestBehavior.DenyGet);
                }
            }
            catch
            {
                return Json("Không có gì được thay đổi cả !", JsonRequestBehavior.DenyGet);
            }
           
        }
       
        public JsonResult getlstclass(int getfalid)
        {
            //int bg = getfalid;
            var lstclass = db.tbLopHocs.Where(p=>p.idKhoa==getfalid&&p.statusLopHoc==true).Select(p=>p.tenLopHoc).ToList();
            if (lstclass.Count != 0)
            {
                return Json(new {code=200, data = lstclass ,message="Thanh cong"}, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new {code=500, message= "Hiện tại khoa này chưa có lớp nào !!" }, JsonRequestBehavior.AllowGet);
           
            }
        }

        public ActionResult update_cv(int id)
        {
            var find = db.tbSinhViens.FirstOrDefault(p => p.idSinhVien == id);
            if (find != null)
            {
                find.isLopTruong = (find.isLopTruong == true ? false : true);
                db.SaveChanges();
                return RedirectToAction("QLSV",new {classid = find.idLopHoc });
            }
            else
            {
                return HttpNotFound();
            }         
        }

    }
}