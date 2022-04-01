using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QLEventKhoa.Models;

namespace QLEventKhoa.ViewModels
{
    public class Thongketop10
    {
        public dbEvent db = new dbEvent();
        public List<topevthamgia> evthamgia()
        {
            List<topevthamgia> listtop = new List<topevthamgia>();
            var ev = db.tbSuKiens.ToList();
            foreach (var item in ev)
            {
                topevthamgia top = new topevthamgia();
                top.evid = item.idEvent;
                top.evname = item.tenEvent;
                top.thamgia = item.tbThamGiaEvents.Count();
                top.tong = (int)item.soLuongThamGia;
                top.mathamgia = item.maThamGia;
                top.trangthai = item.tbTrangThaiEvent.tenTrangThaiEvent;
                if (top.tong != 0)
                {
                    listtop.Add(top);
                }
                
            }
            return listtop;
        }

        public List<topclassjoin> classjoin()
        {
            List<topclassjoin> listclass = new List<topclassjoin>();
            var thamgia = db.tbLopHocs.ToList();
            foreach(var item in thamgia)
            {
                topclassjoin join = new topclassjoin();
                join.classid = (int)item.idLopHoc;
                join.classname = item.tenLopHoc;
                join.evjoincount= item.tbThamGiaEvents.Select(p=>p.idEvent).Distinct().Count();
                join.schoolyear = (item.namBatDau + " - " + item.namKetThuc).ToString();
                join.status = (item.statusLopHoc == true ? "Chưa tốt nghiệp" : "Đã tốt nghiệp");
                join.faculty = item.tbKhoa.tenKhoa;
                if (join.evjoincount != 0)
                {
                    listclass.Add(join);
                }
               
            }
            return listclass;
        }
        public List<topstudent> topstjoin()
        {
            List<topstudent> lststjoin = new List<topstudent>();
            var sv = db.tbSinhViens.ToList();
            foreach(var item in sv)
            {
                topstudent topst = new topstudent();
                topst.sname = item.tenSV;
                topst.scode = item.maSV;
                topst.sclass = item.tbLopHoc.tenLopHoc;
                topst.sjoin = item.tbThamGiaEvents.Count();
                topst.status = (item.statusSV == true ? "Chưa tốt nghiệp" : "Đã tốt nghiệp");
                if (topst.sjoin != 0)
                {
                    lststjoin.Add(topst);
                }
                
            }
            return lststjoin;
        }
        public List<topfaculty> topfaculties()
        {
            List<topfaculty> lstfal = new List<topfaculty>();
            var c = db.tbKhoas.ToList();
            foreach(var item in c)
            {
                topfaculty fal = new topfaculty();
                fal.fcode = item.maKHoa;
                fal.fname = item.tenKhoa;
                fal.fevcount = item.tbSuKiens.Count();
                fal.status = (item.statusKhoa == true ? "Đang hoạt động" : "Đã vô hiệu hóa");
                if (fal.fevcount != 0)
                {
                    lstfal.Add(fal);
                }
            }
            return lstfal;
        }
       
    }
}