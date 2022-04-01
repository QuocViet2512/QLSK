namespace QLEventKhoa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbTrangThaiEvent")]
    public partial class tbTrangThaiEvent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbTrangThaiEvent()
        {
            tbSuKiens = new HashSet<tbSuKien>();
        }

        [Key]
        public int statusEvent { get; set; }

        [Required(ErrorMessage ="Nhập tên trạng thái")]
        [StringLength(20)]
        public string tenTrangThaiEvent { get; set; }

        public bool? statusTTE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbSuKien> tbSuKiens { get; set; }
    }
}
