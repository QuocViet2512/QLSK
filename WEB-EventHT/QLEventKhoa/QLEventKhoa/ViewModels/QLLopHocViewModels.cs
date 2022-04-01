using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using QLEventKhoa.Models;
namespace QLEventKhoa.ViewModels
{
    public class QLLopHocViewModels 
    {
        public int idLopHoc { get; set; }

        [StringLength(10)]
        [MinLength(7, ErrorMessage = "Mã lớp cần 7 ký tự")]
        [MaxLength(7, ErrorMessage = "Mã lớp tối đa 7 ký tự")]
        [Required(ErrorMessage = "Nhập mã lớp học")]
        public string tenLopHoc { get; set; }

        public bool? statusLopHoc { get; set; }

        [Required(ErrorMessage = "Chọn khoa cho lớp học")]
        public int? idKhoa { get; set; }

        [StringLength(40)]
        public string tenKhoa { get; set; }

        [Required(ErrorMessage = "Chọn năm bắt đầu của lớp")]
        public int? namBatDau { get; set; }

        [Required(ErrorMessage = "Chọn năm kết thúc của lớp")]
        public int? namKetThuc { get; set; }

        public List<tbKhoa> listKhoa { get; set; }
    }
}