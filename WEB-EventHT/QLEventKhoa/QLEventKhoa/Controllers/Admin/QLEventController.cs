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
using Rotativa;
using System.Net.Mail;

namespace QLEventKhoa.Controllers.Admin
{
    [AuthenticationFillter]
    public class QLEventController : Controller
    {
        dbEvent db = new dbEvent();
        // GET: QLEvent
        public ActionResult DanhSachEvent(int? ntcid ,int? idkhoa,int? idloaisk )
        {
            List<DSEventViewModels> dsEvent = new List<DSEventViewModels>();
            List<tbSuKien> dsev = null;
            if (ntcid == null && idkhoa ==null&&idloaisk==null)
            {
               dsev = db.tbSuKiens.Where(p=>p.SKDX==false).ToList();
            }
             if (ntcid == null && idkhoa != null && idloaisk == null)
            {
                dsev = db.tbSuKiens.Where(p => p.idKhoa==idkhoa&& p.SKDX == false).ToList();
            }
            if (ntcid == null && idkhoa == null && idloaisk != null)
            {
                dsev = db.tbSuKiens.Where(p => p.idLoaiEvent == idloaisk && p.SKDX ==false).ToList();
            }
            if(ntcid == null && idkhoa != null && idloaisk != null)
            {
                dsev = db.tbSuKiens.Where(p=>p.idKhoa==idkhoa&&p.idLoaiEvent==idloaisk && p.SKDX ==false).ToList();
            }   
            if(ntcid != null && idkhoa == null && idloaisk == null)
            {
                dsev = db.tbSuKiens.Where(p => p.idNhaTC==ntcid && p.SKDX ==false).ToList();
            }
            foreach (var item1 in dsev)
                {
                    DSEventViewModels md = new DSEventViewModels();
                    md.idEvent = item1.idEvent;
                    md.maThamGia = item1.maThamGia;
                    md.tenEvent = item1.tenEvent;
                    md.soLuongThamGia = item1.soLuongThamGia;
                    md.ngayBatDau = item1.ngayBatDau;
                    md.imageEvent = item1.imageEvent;
                    md.tenTrangThai = item1.tbTrangThaiEvent.tenTrangThaiEvent;
                    md.idStatus = item1.tbTrangThaiEvent.statusEvent;
                    md.conlai = db.tbThamGiaEvents.Where(p => p.idEvent == item1.idEvent).Count();
                    dsEvent.Add(md);
                }
            List<tbKhoa> listkhoa = db.tbKhoas.Where(a => a.statusKhoa == true).ToList();
            ViewBag.LoaiKhoa = new SelectList(listkhoa, "idKhoa", "tenKhoa");
            List<tbLoaiEvent> listloai = db.tbLoaiEvents.Where(a => a.statusLoaiEvent == true).ToList();
            ViewBag.LoaiEvent = new SelectList(listloai, "idLoaiEvent", "tenLoaiEvent");
            return View(dsEvent.OrderByDescending(a=>a.idEvent));
        }
        [HttpPost]
        public ActionResult locevent(int? selectLoaiKhoa, int? selectLoaiEvent)
        {
            if (selectLoaiKhoa == null&& selectLoaiEvent == null)
            {
                return RedirectToAction("Danhsachevent");
            }
            else if (selectLoaiKhoa != null && selectLoaiEvent == null)
            {
                return RedirectToAction("Danhsachevent",new {idkhoa = selectLoaiKhoa });
            }
            else if (selectLoaiKhoa == null && selectLoaiEvent != null)
            {
                return RedirectToAction("Danhsachevent", new { idloaisk = selectLoaiEvent });
            }
            else
            {
                return RedirectToAction("Danhsachevent", new { idkhoa = selectLoaiKhoa, idloaisk = selectLoaiEvent });
            }
        }
        //tạo event mới
        public ActionResult addEvent()
        {
            tbSuKien sk = new tbSuKien();
            sk.listLoaiEvent = db.tbLoaiEvents.Where(a => a.statusLoaiEvent == true).ToList();
            sk.listKhoa = db.tbKhoas.Where(a => a.statusKhoa == true).ToList();
            sk.listStatus = db.tbTrangThaiEvents.Where(a => a.statusTTE == true).ToList();
            sk.listTag = db.tbTagEvents.Where(a => a.statusTagEvent == true).ToList();
            sk.listNTC = db.tbNhaToChucs.Where(a => a.statusNTC == true).ToList();
            sk.ngayBatDau = DateTime.Now;
            return View(sk);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult addEvent(tbSuKien sk, HttpPostedFileBase fileupload)
        {
            if (ModelState.IsValid)
            {
                string y = fileupload.FileName;
                var fileName = Path.GetFileName(fileupload.FileName);
                var path = Path.Combine(Server.MapPath("~/images/uploads"), fileName);
                if (System.IO.File.Exists(path))
                {
                    ViewData["Alert"] = "Hình ảnh đã tồn tại";
                }
                else
                {
                    fileupload.SaveAs(path);
                }
                sk.imageEvent = fileName;
                db.tbSuKiens.Add(sk);
                db.SaveChanges();
                return RedirectToAction("addEvent");
            }
            return RedirectToAction("addEvent");
        }
        public ActionResult editEvent(int id)
        {
            tbSuKien sk = new tbSuKien();

            sk = db.tbSuKiens.FirstOrDefault(a => a.idEvent == id);

            sk.listLoaiEvent = db.tbLoaiEvents.Where(a => a.statusLoaiEvent == true).ToList();
            sk.listKhoa = db.tbKhoas.Where(a => a.statusKhoa == true).ToList();
            sk.listStatus = db.tbTrangThaiEvents.Where(a => a.statusTTE == true&&a.statusEvent!=6).ToList();
            sk.listTag = db.tbTagEvents.Where(a => a.statusTagEvent == true).ToList();
            sk.listNTC = db.tbNhaToChucs.Where(a => a.statusNTC == true).ToList();
            return View(sk);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult editEvent(tbSuKien sk, HttpPostedFileBase fileupload)
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
                return RedirectToAction("DanhSachEvent");
            }
            else
            {
                return View();
            }
        }

        public ActionResult DanhSachThamGia(int id)
        {
            var tg = db.tbThamGiaEvents.Where(a => a.idEvent == id).ToList();
            ViewBag.tenSK = tg.FirstOrDefault().tenEvent;
            return View(tg);
        }

        public ActionResult printDS(int id)
        {          
            var tg = db.tbThamGiaEvents.Where(a => a.idEvent == id).ToList();
            return new ViewAsPdf("DanhSachThamGia",tg);
        }

        public ActionResult XacNhanTG(int id)
        {
            tbThamGiaEvent tbt = db.tbThamGiaEvents.FirstOrDefault(a => a.idThamGiaEvent == id);
            var message = new MailMessage();
            if (tbt.statusThamGia== false)
            {
                tbt.statusThamGia = true;             
                message.To.Add(new MailAddress(tbt.emailSV));
                message.Subject = "Đã xác nhận tham gia" + tbt.tenEvent;
                message.Body = "Chúc mừng bạn đã đăng ký thành công tham gia sự kiện " + tbt.tenEvent + "vào ngày: "+tbt.ngayBatDau;
                message.IsBodyHtml = true;
            }
            else
            {
                tbt.statusThamGia = false;
                message.To.Add(new MailAddress(tbt.emailSV));
                message.Subject = "Đã bị từ chối tham gia" + tbt.tenEvent;
                message.Body = "Bạn đã bị từ chối tham gia sự kiện " + tbt.tenEvent + "vào ngày: " + tbt.ngayBatDau;
                message.IsBodyHtml = true;
            }
            if (ModelState.IsValid)
            {
                db.Entry(tbt).State = EntityState.Modified;
                db.SaveChanges();

                using(var smtp = new SmtpClient())
                {
                    smtp.Send(message);
                }
            }
            return RedirectToAction("DanhSachThamGia", "QLEvent", new { id = tbt.idEvent });
        }
     
        public ActionResult CancelChuaxacnhan(int id)
        {          
            var result = db.tbThamGiaEvents.Where(a => id.Equals(a.idEvent) && a.statusThamGia==false);
            db.tbThamGiaEvents.RemoveRange(result);
            db.SaveChanges();
            return RedirectToAction("DanhSachEvent");
        }

    }
}