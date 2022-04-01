namespace QLEventKhoa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbLoaiEvent")]
    public partial class tbLoaiEvent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbLoaiEvent()
        {
            tbSuKiens = new HashSet<tbSuKien>();
        }

        [Key]
        public int idLoaiEvent { get; set; }

        [Required(ErrorMessage ="Nhập tên loại sự kiện")]
        [StringLength(30)]
        public string tenLoaiEvent { get; set; }

        public bool? statusLoaiEvent { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbSuKien> tbSuKiens { get; set; }
    }
}
