namespace QLEventKhoa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbTagEvent")]
    public partial class tbTagEvent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbTagEvent()
        {
            tbSuKiens = new HashSet<tbSuKien>();
        }

        [Key]
        public int idTagEvent { get; set; }

        [Required(ErrorMessage ="Nhập tag sự kiện")]
        [StringLength(15)]
        public string tenTagEvent { get; set; }

        public bool? statusTagEvent { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbSuKien> tbSuKiens { get; set; }
    }
}
