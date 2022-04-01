namespace QLEventKhoa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbThamGiaEvent")]
    public partial class tbThamGiaEvent
    {
        [Key]
        public int idThamGiaEvent { get; set; }

        public int idSinhVien { get; set; }

        public int idEvent { get; set; }

        public int? idLopHoc { get; set; }

        public DateTime ngayBatDau { get; set; }

        [Required]
        [StringLength(10)]
        public string maSV { get; set; }

        [Required]
        [StringLength(255)]
        public string tenEvent { get; set; }

        [Required]
        [StringLength(50)]
        public string tenSV { get; set; }

        [Required]
        [StringLength(50)]
        public string emailSV { get; set; }

        [Required]
        [StringLength(18)]
        public string phoneSV { get; set; }


        [StringLength(10)]
        public string tenLopHoc { get; set; }


        public bool? statusThamGia { get; set; }

        public virtual tbSinhVien tbSinhVien { get; set; }

        public virtual tbSuKien tbSuKien { get; set; }

        public virtual tbLopHoc tbLopHoc { get; set; }
    }
}
