namespace QLEventKhoa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbLoaiLH")]
    public partial class tbLoaiLH
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbLoaiLH()
        {
            tbLienHes = new HashSet<tbLienHe>();
        }

        [Key]
        public int idLoaiLH { get; set; }

        [Required(ErrorMessage ="Nhập tên loại liên hệ")]
        [StringLength(50)]
        public string tenLoaiLH { get; set; }


        public bool? statusLoaiLH { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbLienHe> tbLienHes { get; set; }
    }
}
