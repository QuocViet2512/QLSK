using QLEventKhoa.Models;
using QLEventKhoa.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace QLEventKhoa.Controllers.Admin
{
    [AuthenticationFillter]
    public class SuggestEventManagerController : Controller
    {

        dbEvent db = new dbEvent();
        // GET: SuggestEventManager
        public ActionResult ManagerSuggestView()
        {
            var loadsugg = db.tbSuKiens.Where(p => p.SKDX == true).ToList();
            return View(loadsugg);
        }
        public ActionResult DetailSuggest(int id)
        {
            tbSuKien sk = new tbSuKien();
            sk = db.tbSuKiens.FirstOrDefault(p => p.idEvent == id && p.SKDX == true);
            sk.listLoaiEvent = db.tbLoaiEvents.Where(a => a.statusLoaiEvent == true).ToList();
            sk.listKhoa = db.tbKhoas.Where(a => a.statusKhoa == true).ToList();
            sk.listStatus = db.tbTrangThaiEvents.Where(a => a.statusTTE == true && a.statusEvent != 6).ToList();
            sk.listTag = db.tbTagEvents.Where(a => a.statusTagEvent == true).ToList();
            sk.listNTC = db.tbNhaToChucs.Where(a => a.statusNTC == true).ToList();
            return View(sk);
        }

        public ActionResult Delete(int id)
        {
            db.tbSuKiens.Remove(db.tbSuKiens.FirstOrDefault(p => p.idEvent == id));
            db.SaveChanges();
            return RedirectToAction("ManagerSuggestView");
        }

        public FileResult Download(string filename)
        {
            string path = Server.MapPath("~/DocumentUploads/") + filename;
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ConfirmEvent(tbSuKien sk, HttpPostedFileBase fileupload)
        {
            sk.SKDX = false;
            if (fileupload != null && fileupload.ContentLength > 0)
            {
                var fileName = Path.GetFileName(fileupload.FileName);
                var path = Path.Combine(Server.MapPath("~/images/uploads"), fileupload.FileName);
                if (System.IO.File.Exists(path))
                {
                    TempData["Alert"] = "Hình ảnh đã tồn tại";
                }
                else
                {
                    fileupload.SaveAs(path);
                }
                sk.imageEvent = fileName;
            }
            if (ModelState.IsValid)
            {
                db.Entry(sk).State = EntityState.Modified;
                db.SaveChanges();
                var message = new MailMessage();
                message.To.Add(new MailAddress(sk.EmailDX));
                message.Subject = "Xác nhận đề xuất sự kiện " + sk.tenEvent + " của bạn";
                message.Body = "Chúng tôi đã xem xét và thay đổi một số thông tin phù hợp với yêu cầu của khoa và trường. Bạn vui lòng xem thông tin sự kiện đề xuất của bạn tại đây:  http://localhost:2039/SuggestEvent/ConfirmEvent/" + sk.idEvent+ "<br><br>Mọi ý kiến xin liên hệ với Quản lý qua fannpage: https://www.facebook.com/Hutech-events-community-104756452038576  hoặc liên hệ trong website sự kiện.<br><br>Xin cảm ơn sự đóng góp của bạn";
                message.IsBodyHtml = true;
                using (var smtp = new SmtpClient())
                {
                    smtp.Send(message);
                }
                return RedirectToAction("ManagerSuggestView");
            }       
            else
            {
                return View();
             }
         } 
    }
} 
        
       
    
