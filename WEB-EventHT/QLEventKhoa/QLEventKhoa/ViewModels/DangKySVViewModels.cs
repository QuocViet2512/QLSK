using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QLEventKhoa.Models;
using System.Web.Mvc;

namespace QLEventKhoa.ViewModels
{
    public class DangKySVViewModels
    {

        [StringLength(10)]
        [Required(ErrorMessage ="Nhập mã số sinh viên")]
        [RegularExpression(@"^[0-9]{10}", ErrorMessage = "Nhập đúng định dạng mã số sinh viên đang học")]
        public string maSV { get; set; }

        [StringLength(100)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Nhập mật khẩu")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,}$", ErrorMessage ="Mật khẩu cần tối thiểu 6 ký tự 1 hoa 1 thường 1 số")]
        public string matKhauSV { get; set; }


        [NotMapped]
        [StringLength(100)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Nhập lại mật khẩu")]
        [System.ComponentModel.DataAnnotations.Compare("matKhauSV",ErrorMessage ="Mật khẩu nhập lại không trùng nhau")]
        public string rePassword { get; set; }
          
        [StringLength(50)]
        [Required(ErrorMessage = "Nhập Họ tên")]
        public string tenSV { get; set; }

        [StringLength(50,ErrorMessage ="Không quá 50 ký tự")]
        [Required(ErrorMessage = "Nhập Email")]
        [EmailAddress(ErrorMessage = "Sai định dạng email")]
        public string emailSV { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Nhập ngày sinh")]
        [Column(TypeName = "date")]
        public DateTime ngaySinhSV { get; set; }

        [StringLength(18)]
        [Required(ErrorMessage = "Nhập số điện thoại")]
        [RegularExpression(@"^[0-9]{10,18}", ErrorMessage = "Chỉ nhập số")]
        public string phoneSV { get; set; }

        [Required(ErrorMessage = "Chọn khoa đang học")]
        public int idKhoa { get; set; }

        public SelectList LopHoc { get; set; }

        public List<tbLopHoc> listLop = new List<tbLopHoc>();
        public List<tbKhoa> listKhoa = new List<tbKhoa>();
    }
}