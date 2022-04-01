namespace QLEventKhoa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("tbLopHoc")]
    public partial class tbLopHoc 
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbLopHoc()
        {
            tbSinhViens = new HashSet<tbSinhVien>();
            tbThamGiaEvents = new HashSet<tbThamGiaEvent>();
        }

        [Key]
        public int idLopHoc { get; set; }

        [StringLength(10)]
        public string tenLopHoc { get; set; }

        public bool? statusLopHoc { get; set; }

        public int? idKhoa { get; set; }

        public int? namBatDau { get; set; }


        public int? namKetThuc { get; set; }

        public virtual tbKhoa tbKhoa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbSinhVien> tbSinhViens { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbThamGiaEvent> tbThamGiaEvents { get; set; }

        public List<tbKhoa> listKhoa = new List<tbKhoa>();


    }
}
