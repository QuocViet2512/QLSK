using QLEventKhoa.Models;
using QLEventKhoa.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLEventKhoa.Controllers.SinhVien
{
    public class SuggestEventController : Controller
    {
        dbEvent db = new dbEvent();
        // GET: SuggestEvent
        public ActionResult SuggestEventView()
        {
            tbSinhVien sv = (tbSinhVien)Session["SV"];
            if (sv != null)
            {
                GetListFaculyAndCategoryEvent GetMixList = new GetListFaculyAndCategoryEvent();
                GetMixList.GetListFaculty = db.tbKhoas.Where(p => p.statusKhoa == true).ToList();
                GetMixList.GetCategoryEvent = db.tbLoaiEvents.Where(p => p.statusLoaiEvent == true).ToList();
                GetMixList.GetListEvent = db.tbSuKiens.ToList();
                return View(GetMixList);
            }
            return RedirectToAction("LoginSV","AccountSV");
        }
        [HttpPost]
        public ActionResult SGevent (FormCollection form) {
            tbSinhVien sv = (tbSinhVien)Session["SV"];
            if (sv != null)
            {
                return View();
            }
            return RedirectToAction("LoginSV");
        }
        public JsonResult getcodeallevent()
        {
            var getlist = db.tbSuKiens.Select(p => p.maThamGia).ToList();
            if (getlist.Count > 0)
            {
                return Json(new {code=200,data = getlist },JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { code = 500 }, JsonRequestBehavior.AllowGet);
            }           
        }
     
        [HttpPost,ValidateInput(false)]  
        public JsonResult SendSuggest()
        {

            string EVName = Request.Form["EVName"];
            string EVPlace = Request.Form["EVPlace"];
            int EVLimmit = int.Parse(Request.Form["EVLimit"]);
            int EVFaculty = int.Parse(Request.Form["EVFaculty"]);
            int EVCate = int.Parse(Request.Form["EVCate"]);
            int? EVPrice = int.Parse(Request.Form["EVPrice"]);
            DateTime EVBegin = DateTime.Parse(Request.Form["EVBegin"]);
            string EVCode = Request.Form["EVCode"];
            string EVContent= Request.Form["EVContent"];
            var EVBanner = Request.Files["EVBanner"];
            var EVTarget = Request.Files["EVTarget"];
            tbSinhVien sv = Session["SV"] as tbSinhVien;
            if (sv != null) {
                try
                {
                    EVBanner.SaveAs(Path.Combine(Server.MapPath("~/images/uploads"), EVBanner.FileName));
                    EVTarget.SaveAs(Path.Combine(Server.MapPath("~/DocumentUploads"), EVTarget.FileName));
                    tbSuKien CreateSuggest = new tbSuKien();
                    CreateSuggest.tenEvent = EVName;
                    CreateSuggest.maThamGia = EVCode;
                    CreateSuggest.noiDungEvent = EVContent;
                    CreateSuggest.noiToChuc = EVPlace;
                    CreateSuggest.chiPhiEvent = (EVPrice == null ? 0 : EVPrice);
                    CreateSuggest.soLuongThamGia = EVLimmit;
                    CreateSuggest.ngayBatDau = EVBegin;
                    CreateSuggest.idKhoa = EVFaculty;
                    CreateSuggest.idLoaiEvent = EVCate;
                    CreateSuggest.SKDX = true;
                    CreateSuggest.statusEvent = 6;
                    CreateSuggest.KeHoach = EVTarget.FileName;
                    CreateSuggest.videoEvent = "";
                    CreateSuggest.imageEvent = EVBanner.FileName;
                    CreateSuggest.idNhaTC = null;
                    CreateSuggest.idTagEvent = null;
                    CreateSuggest.EmailDX = sv.emailSV;
                    db.tbSuKiens.Add(CreateSuggest);

                    db.SaveChanges();
                    return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e) { return Json(new { code = 500, msg = e }, JsonRequestBehavior.AllowGet); }
            }
            else { return Json(new { code = 404, msg="Bạn chưa đăng nhập hoặc tài khoản đã hết hạn đăng nhập. Vui lòng đăng nhập lại!" }, JsonRequestBehavior.AllowGet); }



        }
public ActionResult ConfirmEvent(int id)
        {
            var skcf = db.tbSuKiens.FirstOrDefault(p => p.idEvent == id);
            if (skcf == null)
            {
                return RedirectToAction("notfound404", "Exception");
            }
            return View(skcf);
        }

    }
}