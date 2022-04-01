using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace QLEventKhoa.ViewModels
{
    public class ChangePassSVViewModels
    {


        [StringLength(100)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Nhập mật khẩu cũ")]       
        public string matKhauSV { get; set; }

        [StringLength(100)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Nhập mật khẩu mới")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,}$", ErrorMessage = "Mật khẩu cần tối thiểu 6 ký tự 1 hoa 1 thường 1 số")]   
        public string newPassword { get; set; }


        [StringLength(100)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Nhập lại mật khẩu mới")]
        [Compare("newPassword", ErrorMessage = "Mật khẩu mới trùng mật khẩu nhập lại")]
        public string RenewPassword { get; set; }

    }
}