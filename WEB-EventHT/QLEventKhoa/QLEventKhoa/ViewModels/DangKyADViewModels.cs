using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QLEventKhoa.Models;

namespace QLEventKhoa.ViewModels
{
    public class DangKyADViewModels
    {

        [StringLength(10)]
        [Required(ErrorMessage = "Nhập tên đăng nhập")]
        public string userAdmin { get; set; }

        [StringLength(200)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Nhập mật khẩu")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,}$", ErrorMessage = "Mật khẩu cần tối thiểu 6 ký tự 1 hoa 1 thường 1 số")]
        public string passAdmin { get; set; }


        [NotMapped]
        [StringLength(100)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Nhập lại mật khẩu")]
        [System.ComponentModel.DataAnnotations.Compare("passAdmin", ErrorMessage = "Mật khẩu nhập lại không trùng nhau")]
        public string rePassword { get; set; }



        [StringLength(25)]
        [Required(ErrorMessage = "Nhập Email")]
        [EmailAddress(ErrorMessage = "Sai định dạng email")]
        public string emailAD { get; set; }

        [Required(ErrorMessage = "Chọn loại quản lý")]
        public int idloaiql { get; set; }


        public List<tbLoaiAdmin> listLoaiAD = new List<tbLoaiAdmin>();

     
    }
}