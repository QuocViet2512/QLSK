using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLEventKhoa.Models;
using PagedList;
using PagedList.Mvc;
using QLEventKhoa.ViewModels;
using System.Net.Mail;
namespace QLEventKhoa.Controllers.SinhVien
{
    [SVSessionTimeOut]
    public class HomeSVController : Controller
    {
        // GET: HomeSV
        dbEvent db = new dbEvent();

        public ActionResult IndexSV()
        {
            return View(db.tbSuKiens.Where(p=>p.SKDX==false).OrderByDescending(a => a.ngayBatDau).ToList());
        }


        public ActionResult DetailEvent(int? id)
        {
            var findd = db.tbSuKiens.Find(id);
            if (findd == null)
            {
                return RedirectToAction("notfound404","Exception");
            }
            else
            {
                tbSinhVien sv = (tbSinhVien)Session["SV"];
                tbSuKien so = db.tbSuKiens.Where(a => a.idEvent == id).FirstOrDefault();
                int sum = 0;
                if (Session["LogIn"] != null)
                {
                    tbThamGiaEvent find = db.tbThamGiaEvents.FirstOrDefault(a => a.idSinhVien == sv.idSinhVien && a.idEvent == so.idEvent);
                    if (find == null)
                    {
                        so.isThmaGia = true;
                    }
                }
                var count = db.tbThamGiaEvents.Where(a => a.idEvent == id).Count();
                if (count > 0)
                {
                    sum += count;
                }
                ViewBag.SL = sum;
                return View(so);
            }

        }

        public ActionResult _PartialDSKhoaNavBar()
        {
            var listK = (from a in db.tbKhoas join b in db.tbSuKiens on a.idKhoa equals b.idKhoa where a.statusKhoa == true select a).Distinct();

            return PartialView(listK);
        }

        public ActionResult getEventbyKhoa(int? id, int? page)
        {
            var find = db.tbKhoas.Find(id);
            if (find == null)
            {
                return RedirectToAction("IndexSV");
            }
            else
            {
                int pageNum = (page ?? 1);
                int pageSize = 12;
                var eventKhoa = db.tbSuKiens.ToList().Where(a => a.idKhoa == id).OrderByDescending(a => a.ngayBatDau).ToPagedList(pageNum, pageSize);
                List<tbLoaiEvent> loaiList = db.tbLoaiEvents.Where(a => a.statusLoaiEvent == true).ToList();
                ViewBag.LoaiEvent = new SelectList(loaiList, "idLoaiEvent", "tenLoaiEvent");
                ViewBag.tenKhoa = eventKhoa.FirstOrDefault().tbKhoa.tenKhoa;
                return View(eventKhoa);
            }
        }

        [HttpPost]
        public ActionResult getEventbyKhoa(string txtsearch, int? page, int id, int? selectLoaiEvent)
        {
            int pageNum = (page ?? 1);
            int pageSize = 12;
            var search = db.tbSuKiens.Where(a => a.idKhoa == id);

            ViewBag.tenKhoa = search.FirstOrDefault().tbKhoa.tenKhoa;

            List<tbLoaiEvent> loaiList = db.tbLoaiEvents.Where(a => a.statusLoaiEvent == true).ToList();
            ViewBag.LoaiEvent = new SelectList(loaiList, "idLoaiEvent", "tenLoaiEvent");

            if (!string.IsNullOrEmpty(txtsearch))
            {
                search = search.Where(a => a.tenEvent.Contains(txtsearch));
                ViewBag.search = txtsearch;
            }

            if (selectLoaiEvent != null)
            {
                search = search.Where(a => a.idLoaiEvent == selectLoaiEvent);
            }

            if (!string.IsNullOrEmpty(txtsearch) && selectLoaiEvent != null)
            {
                search = search.Where(a => a.tenEvent.Contains(txtsearch) && a.idLoaiEvent == selectLoaiEvent);
            }
            return View(search.OrderByDescending(a => a.ngayBatDau).ToList().ToPagedList(pageNum, pageSize));
        }

        public ActionResult AboutUs()
        {
            return View();
        }


        public ActionResult DSDangKy()
        {
            tbSinhVien sv = (tbSinhVien)Session["SV"];
            if (sv != null)
            {
                var listdk = db.tbThamGiaEvents.Where(a => a.idSinhVien == sv.idSinhVien && a.tbSuKien.tbTrangThaiEvent.statusEvent == 1 || a.tbSuKien.tbTrangThaiEvent.statusEvent == 3).ToList();
                return View(listdk);
            }

            return View();
        }

        public ActionResult DSDangKyOld()
        {
            tbSinhVien sv = (tbSinhVien)Session["SV"];
            if (sv != null)
            {
                var listdkc = db.tbThamGiaEvents.Where(a => a.idSinhVien == sv.idSinhVien && a.tbSuKien.tbTrangThaiEvent.statusEvent == 2).ToList();
                return View(listdkc);
            }
            return View();
        }

        /// <summary>
        /// /////////
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        [HttpGet]
        public ActionResult ThamGiaSK(int id)
        {
            tbSuKien find = db.tbSuKiens.Find(id);
            return PartialView(find);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThamGiaSK(tbThamGiaEvent tg,tbSuKien sk,tbSinhVien svv )
        {        
            if (Session["LogIn"] == null)
            {
                return View("LoginSV", "AccountSV");
            }         
            tbSinhVien sv = (tbSinhVien)Session["SV"];         
            tg.idSinhVien = sv.idSinhVien;
            tg.tenSV = sv.tenSV;
            tg.maSV = sv.maSV;
            tg.phoneSV = svv.phoneSV;
            tg.emailSV = svv.emailSV;
            tg.idLopHoc = sv.idLopHoc;
            tg.tenLopHoc = sv.tbLopHoc.tenLopHoc;
            tg.statusThamGia = false;

            db.tbThamGiaEvents.Add(tg);
            db.SaveChanges();
            return RedirectToAction("DetailEvent", "HomeSV", new { id = sk.idEvent });
        }

        [HttpGet]
        public ActionResult HuyThamGiafromDetailSK(int? id)
        {
            tbSuKien find = db.tbSuKiens.Find(id);
            return PartialView(find);
        }

        [HttpPost]
        public ActionResult HuyThamGiafromDetailSK(int id)
        {
            tbSinhVien sv = (tbSinhVien)Session["SV"];
            tbThamGiaEvent tg = db.tbThamGiaEvents.FirstOrDefault(a => a.idEvent == id && a.idSinhVien == sv.idSinhVien);
            var message = new MailMessage();

            if (tg != null)
            {
                db.tbThamGiaEvents.Remove(tg);
                db.SaveChanges();
                message.To.Add(new MailAddress(tg.emailSV));
                message.Subject = "Thông báo đã hủy tham gia \t" + tg.tenEvent;
                message.Body = "Sinh viên: " + tg.tenSV + " đã hủy tham gia sự kiện " + tg.tenEvent + " vào ngày: " + tg.ngayBatDau.ToString("dd/MM/yyyy HH:mm");
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    smtp.Send(message);
                }
            }
            return RedirectToAction("DetailEvent", "HomeSV", new { id });
        }

        [HttpGet]
        public ActionResult HuyThamGiaSK(int? id)
        {
            tbThamGiaEvent find = db.tbThamGiaEvents.Find(id);
            return PartialView(find);
        }

        [HttpPost]
        public ActionResult HuyThamGiaSK(int id)
        {
            var tg = db.tbThamGiaEvents.Find(id);
            var message = new MailMessage();
            if (tg != null)
            {
                db.tbThamGiaEvents.Remove(tg);
                db.SaveChanges();

                message.To.Add(new MailAddress(tg.emailSV));
                message.Subject = "Thông báo  hủy tham gia \t" + tg.tenEvent;
                message.Body = "Sinh viên: " + tg.tenSV + "\t đã hủy tham gia sự kiện " + tg.tenEvent + "\tvào ngày: " + tg.ngayBatDau;
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    smtp.Send(message);
                }
            }
            return RedirectToAction("DSDangKy", "HomeSV");
        }

        public ActionResult SearchEvent(string searchSTR, int? page)
        {
            int pageNum = (page ?? 1);
            int pageSize = 12;
            var search = from fo in db.tbSuKiens select fo;
            if (!string.IsNullOrEmpty(searchSTR))
            {
                search = search.Where(a => a.tenEvent.Contains(searchSTR.ToLower()));
            }
            ViewBag.keyword = searchSTR;
            return View(search.ToList().ToPagedList(pageNum, pageSize));
        }
        public ActionResult LienHeSV()
        {
            LienHeBenSVViewModels lh = new LienHeBenSVViewModels();
            lh.tbLoaiLHs = db.tbLoaiLHs.Where(a => a.statusLoaiLH == true).ToList();
            return View(lh);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult LienHeSV(LienHeBenSVViewModels lh)
        {
            if (ModelState.IsValid)
            {
                tbLienHe lk = new tbLienHe();
                lk.statusLH = false;
                lk.tenSV = lh.tenSV;
                lk.tieuDeLH = lh.tieuDeLH;
                lk.maSV = lh.maSV;
                lk.phoneSV = lh.phoneSV;
                lk.emailSV = lh.emailSV;
                lk.idLoaiLH = lh.idLoaiLH;
                lk.noiDungLH = lh.noiDungLH;

                db.tbLienHes.Add(lk);
                db.SaveChanges();
                ViewBag.success = "Đã gửi liên hệ";
                return RedirectToAction("LienHeSV");
            }
            else
            {
                return View();
            }
        }

        public ActionResult DanhSachLienHe()
        {
            tbSinhVien sv = (tbSinhVien)Session["SV"];
            if (sv != null)
            {
                var listLH = db.tbLienHes.Where(a => a.maSV == sv.maSV).OrderBy(a => a.statusLH).ToList();
                return View(listLH);
            }
            return View();
        }

        public ActionResult XemChiTietLH(int id)
        {
            tbSinhVien sv = (tbSinhVien)Session["SV"];
            if (sv != null)
            {
                var listLH = db.tbLienHes.Where(a => a.maSV == sv.maSV && a.idLienHe == id).OrderBy(a => a.statusLH).ToList();
                return View(listLH);
            }
            return View("DanhSachLienHe");
        }

        public ActionResult SinhVienThamGia(int? eventid)
        {
            tbTaiKhoanSV SV = Session["LogIn"] as tbTaiKhoanSV;
            if (SV == null || SV.tbSinhVien.isLopTruong == false)
            {
                return RedirectToAction("IndexSV", "HomeSV");
            }
            var SVTG = new List<tbThamGiaEvent>();
            var TG = db.tbThamGiaEvents.Where(t => t.idEvent == eventid && t.idLopHoc == SV.tbSinhVien.idLopHoc).ToList();
            //foreach (var item in TG)
            //{
            //    var SinhViens = db.tbThamGiaEvents.SingleOrDefault(t => t.idSinhVien == item.idSinhVien);
            //    SVTG.Add(SinhViens);
            //}
            return View(TG);
        }

        public ActionResult LopTruong()
        {
            tbTaiKhoanSV SV = Session["LogIn"] as tbTaiKhoanSV;
            if (SV == null || SV.tbSinhVien.isLopTruong == false)
            {
                return RedirectToAction("IndexSV", "HomeSV");
            }
            var ListSuKien = new List<tbSuKien>();
            var ThamGia = db.tbThamGiaEvents.Where(t => t.idLopHoc == SV.tbSinhVien.idLopHoc && t.tbSuKien.tbTrangThaiEvent.statusEvent==1 || t.tbSuKien.tbTrangThaiEvent.statusEvent==3).ToList();
            foreach (var item in ThamGia)
            {
                ListSuKien.Add(db.tbSuKiens.SingleOrDefault(t => t.idEvent == item.idEvent));
            }
            return View(ListSuKien);
        }

    }
}