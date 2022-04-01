using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QLEventKhoa.Models;

namespace QLEventKhoa.ViewModels
{
    public class LienHeBenSVViewModels
    {
        public int? idSinhVien { get; set; }

        [Required(ErrorMessage = "Chọn loại liên hệ")]
        public int idLoaiLH { get; set; }

        [Required(ErrorMessage = "Nhập tiêu đê liên hệ")]
        [StringLength(255)]
        public string tieuDeLH { get; set; }

        [Required]
        [StringLength(3000)]
        public string noiDungLH { get; set; }

        [Required(ErrorMessage = "Nhập số điện thoại")]
        [StringLength(18)]
        [RegularExpression(@"^[0-9]{10,18}", ErrorMessage = "Chỉ nhập số")]
        public string phoneSV { get; set; }

        [Required(ErrorMessage = "Nhập email để liên hệ")]
        [StringLength(50)]
        [EmailAddress(ErrorMessage = "Sai định dạng email")]
        public string emailSV { get; set; }

        [Required(ErrorMessage = "Nhập mã số sinh viên")]
        [StringLength(10)]
        [RegularExpression(@"^[0-9]{10}", ErrorMessage = "Nhập đúng định dạng mã số sinh viên đang học")]
        public string maSV { get; set; }

        [Required(ErrorMessage = "Nhập họ tên sinh viên")]
        [StringLength(40)]
        public string tenSV { get; set; }

        

        public List<tbLoaiLH> tbLoaiLHs = new List<tbLoaiLH>();
    }
}