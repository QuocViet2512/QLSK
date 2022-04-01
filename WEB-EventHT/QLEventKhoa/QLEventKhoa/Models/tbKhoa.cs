namespace QLEventKhoa.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbKhoa")]
    public partial class tbKhoa
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbKhoa()
        {
            tbSinhViens = new HashSet<tbSinhVien>();
            tbSuKiens = new HashSet<tbSuKien>();
            tbLopHocs = new HashSet<tbLopHoc>();
        }

        [Key]
        public int idKhoa { get; set; }

        [Required(ErrorMessage ="Nhập mã khoa")]
        [StringLength(5)]
        public string maKHoa { get; set; }

        [Required(ErrorMessage ="Nhập tên khoa")]
        [StringLength(40)]
        public string tenKhoa { get; set; }

        public bool? statusKhoa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbSinhVien> tbSinhViens { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbSuKien> tbSuKiens { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbLopHoc> tbLopHocs { get; set; }


    }


}
