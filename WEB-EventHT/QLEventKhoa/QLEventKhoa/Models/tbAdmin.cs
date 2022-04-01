namespace QLEventKhoa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbAdmin")]
    public partial class tbAdmin
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbAdmin()
        {
            tbLienHes = new HashSet<tbLienHe>();
        }

        [Key]
        public int idAdmin { get; set; }

        [Required]
        [StringLength(10)]
        public string userAdmin { get; set; }

        [Required]
        [StringLength(200)]
        [DataType(DataType.Password)]
        public string passAdmin { get; set; }

        [StringLength(25)]
        public string emailAD { get; set; }

        public bool? statusAd { get; set; }

        public int idloaiql { get; set; }

        public virtual tbLoaiAdmin tbLoaiAdmin { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbLienHe> tbLienHes { get; set; }
        

    }
}
