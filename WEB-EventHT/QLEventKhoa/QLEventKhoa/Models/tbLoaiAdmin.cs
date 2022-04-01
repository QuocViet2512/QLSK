namespace QLEventKhoa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbLoaiAdmin")]
    public partial class tbLoaiAdmin
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbLoaiAdmin()
        {
            tbAdmins = new HashSet<tbAdmin>();
        }

        [Key]
        public int idLoaiAD { get; set; }

        [Required(ErrorMessage ="Nhập tên loại admin")]
        [StringLength(50)]
        public string tenLoaiAD { get; set; }

        public bool? statusLAD { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbAdmin> tbAdmins { get; set; }

       
    }
}
