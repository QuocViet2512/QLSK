namespace QLEventKhoa.Models.NewFolder1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbSuKien")]
    public partial class tbSuKien
    {
        [Key]
        public int idEvent { get; set; }

        [StringLength(255)]
        public string tenEvent { get; set; }

        [StringLength(100)]
        public string noiToChuc { get; set; }

        public DateTime? ngayBatDau { get; set; }

        [StringLength(4000)]
        public string noiDungEvent { get; set; }

        [StringLength(255)]
        public string imageEvent { get; set; }

        [StringLength(255)]
        public string videoEvent { get; set; }

        [StringLength(5)]
        public string maThamGia { get; set; }

        public int? soLuongThamGia { get; set; }

        public int? chiPhiEvent { get; set; }

        public int? idLoaiEvent { get; set; }

        public int? idTagEvent { get; set; }

        public int? idKhoa { get; set; }

        public int? idNhaTC { get; set; }

        public int? statusEvent { get; set; }

        [StringLength(128)]
        public string KeHoach { get; set; }

        public bool? SKDX { get; set; }
    }
}
