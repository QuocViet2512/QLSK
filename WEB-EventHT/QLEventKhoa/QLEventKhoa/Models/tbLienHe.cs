namespace QLEventKhoa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbLienHe")]
    public partial class tbLienHe
    {
        [Key]
        public int idLienHe { get; set; }

        public int? idLoaiLH { get; set; }

        public int? idSinhVien { get; set; }

        [StringLength(255)]
        public string tieuDeLH { get; set; }

        [StringLength(3000)]
        public string noiDungLH { get; set; }

        [StringLength(18)]
        public string phoneSV { get; set; }

        [StringLength(50)]
        public string emailSV { get; set; }

        [StringLength(10)]
        public string maSV { get; set; }

        [StringLength(40)]
        public string tenSV { get; set; }

        public int? idAdmin { get; set; }

        [StringLength(25)]
        public string emailAD { get; set; }

        [StringLength(10)]
        public string userAdmin { get; set; }

        [StringLength(3000)]
        public string noiDungPH { get; set; }

        public bool? statusLH { get; set; }

        public virtual tbAdmin tbAdmin { get; set; }

        public virtual tbLoaiLH tbLoaiLH { get; set; }

        public virtual tbSinhVien tbSinhVien { get; set; }
    }
}
