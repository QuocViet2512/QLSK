using QLEventKhoa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLEventKhoa.ViewModels;
using System.Data.Entity;

namespace QLEventKhoa.Controllers.Admin
{
    [AuthenticationFillter]
    [AuthorizeRole(Roles = "Admin Tổng")]
    public class QLNTCController : Controller
    {
        dbEvent db = new dbEvent();
        
     
        public ActionResult NTC()
        {         
            return View(db.tbNhaToChucs.ToList());
        }

        // ADD NTC

        public JsonResult AddNTC(string a_ntccode,string a_ntcname,string a_ntcmail,string a_ntcphone)
        {
            var check = db.tbNhaToChucs.FirstOrDefault(p => p.maNTC.Equals(a_ntccode));
            try
            {
                if (check == null)
                {
                    var ntcnew = new tbNhaToChuc();
                    ntcnew.maNTC = a_ntccode;
                    ntcnew.tenNTC = a_ntcname;
                    ntcnew.emailNTC = a_ntcmail;
                    ntcnew.phoneNTC = a_ntcphone;
                    ntcnew.statusNTC = true;
                    db.tbNhaToChucs.Add(ntcnew);                
                    db.SaveChanges();
                    return Json(new { code = 200, idNTC = ntcnew.idNhaTC }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { code = 500, msg = "Mã nhà tổ chức này đã tồn tại !!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json("", JsonRequestBehavior.DenyGet);
            }

        }

        // hàm chỉnh sửa nhà tổ chức 
        public JsonResult EditNTC(int id, string e_ntcname, string e_ntcmail, string e_ntcphone, bool e_ntcstt)
        {
            var edntc = db.tbNhaToChucs.FirstOrDefault(p => p.idNhaTC == id);
            if (edntc != null)
            {
                if (!edntc.tenNTC.Equals(e_ntcname) || !edntc.emailNTC.Equals(e_ntcmail) || !edntc.phoneNTC.Equals(e_ntcphone)||edntc.statusNTC!=e_ntcstt)
                {
                    edntc.tenNTC = e_ntcname;
                    edntc.emailNTC = e_ntcmail;
                    edntc.phoneNTC = e_ntcphone;
                    edntc.statusNTC = e_ntcstt;
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