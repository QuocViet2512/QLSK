using QLEventKhoa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLEventKhoa.ViewModels
{
    public class listclass
    {
        public List<tbLopHoc> dsLop { get; set; }
        public List<tbKhoa> dsKhoa { get; set; }
    }
    public class listsv
    {
        public List<tbKhoa> lstfal { set; get; }
        public List<tbSinhVien> lstst { set; get; }
    }
    public class DSAdminViewModels
    {
        public List<tbAdmin> listAdmin { get; set; }
        public List<tbLoaiAdmin> listLoaiAD { get; set; }
    }
}