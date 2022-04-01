namespace QLEventKhoa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbTaiKhoanSV")]
    public partial class tbTaiKhoanSV
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idAccSV { get; set; }

        [Required(ErrorMessage ="Nhập mã số sinh viên")]
        [StringLength(10)]
        public string maSV { get; set; }

        [Required(ErrorMessage ="Nhập mật khẩu")]
        [DataType(DataType.Password)]
        [StringLength(100)]
        public string matKhauSV { get; set; }

        public bool? statusAccSV { get; set; }

        public virtual tbSinhVien tbSinhVien { get; set; }
    }
}
