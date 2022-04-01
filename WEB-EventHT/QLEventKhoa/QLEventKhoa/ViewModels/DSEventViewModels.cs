using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLEventKhoa.Models;

namespace QLEventKhoa.ViewModels
{
    public class DSEventViewModels
    {
        public int idEvent { get; set; }
        public string maThamGia { get; set; }
        public string tenEvent { get; set; }
        public int? soLuongThamGia { get; set; }
        public string tenTrangThai { get; set; }
        public DateTime ngayBatDau { get; set; }
        public string imageEvent { get; set; }

        public List<tbSuKien> TbSuKiens { get; set; }
        public List<tbTrangThaiEvent> TbTrangThaiEvents { get; set; }
        public int idStatus { get;  set; }
        public int conlai;
    }
}