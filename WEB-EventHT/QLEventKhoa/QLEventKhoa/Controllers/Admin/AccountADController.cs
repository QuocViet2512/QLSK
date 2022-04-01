using System.Linq;
using System.Web.Mvc;
using QLEventKhoa.Models;
using System.Web.Security;
using QLEventKhoa.ViewModels;

namespace QLEventKhoa.Controllers.Admin
{

    public class AccountADController : Controller
    {
        // GET: AccountAD
        dbEvent db = new dbEvent();
        EncryptMD5 MD5 = new EncryptMD5();
      
      

        [HttpGet]
        [AuthenticationFillter]
        [AuthorizeRole(Roles = "Admin Tổng")]
        public ActionResult RegisterAD()
        {
            DangKyADViewModels dk = new DangKyADViewModels();
            dk.listLoaiAD = db.tbLoaiAdmins.ToList();
            return View(dk);
            
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [AuthenticationFillter]
        [AuthorizeRole(Roles = "Admin Tổng")]
        public ActionResult RegisterAD(DangKyADViewModels dk)
        {
            if (ModelState.IsValid)
            {
                var check = db.tbAdmins.SingleOrDefault(a => a.userAdmin.Equals(dk.userAdmin) && a.emailAD.Equals(dk.emailAD));
                if (check == null)
                {
                    tbAdmin ad = new tbAdmin();
                    ad.userAdmin = dk.userAdmin;
                    ad.passAdmin = MD5.Encrypt(dk.passAdmin);
                    ad.emailAD = dk.emailAD;
                    ad.statusAd = true;
                    ad.idloaiql = dk.idloaiql;
                    db.tbAdmins.Add(ad);
                    db.SaveChanges();
                    return RedirectToAction("RegisterAD", "AccountAD");
                }
                else
                {
                    ViewBag.error = "Tên đăng nhập đã tồn tại";
                    return RedirectToAction("RegisterAD", "AccountAD"); ;
                }
            }
            return RedirectToAction("RegisterAD", "AccountAD");
        }

        [HttpGet]
        public ActionResult LoginAD()
        {       
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginAD(tbAdmin ad)
        {
            if (ModelState.IsValid)
            {         
                string hashpass = MD5.Encrypt(ad.passAdmin);
                var admin = db.tbAdmins.FirstOrDefault(a => a.userAdmin.Equals(ad.userAdmin) && a.passAdmin.Equals(hashpass));

                if (admin != null)
                {                  
                    Session["LogAD"] = admin;
                    Session["UserAD"] = admin.userAdmin;
                    Session["UserID"] = admin.idAdmin;
                    Session["EmailAD"] = admin.emailAD;
                    FormsAuthentication.SetAuthCookie(ad.userAdmin, false);
                    return RedirectToAction("IndexAD", "HomeAD");
                }
                else
                {
                    ViewBag.error = "Tài khoản hoặc mật khẩu sai";
                    return View();
                }
            }
            return this.View();
        }
        public ActionResult LogOut()
        {
            // Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("LoginAD", "AccountAD");
        }


    }
}
