namespace QLEventKhoa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbNhaToChuc")]
    public partial class tbNhaToChuc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbNhaToChuc()
        {
            tbSuKiens = new HashSet<tbSuKien>();
        }

        [Key]
        public int idNhaTC { get; set; }

        [Required(ErrorMessage ="Nhập mã nhà tổ chức")]
        [MinLength(3,ErrorMessage ="Nhập tối thiểu 3 ký tự")]
        [StringLength(5,ErrorMessage ="Mã tối đa 5 ký tự")]
        public string maNTC { get; set; }

        [Required(ErrorMessage ="Nhập tên nhà tổ chức")]
        [StringLength(100,ErrorMessage ="Tên tối đa 50 ký tự")]
        public string tenNTC { get; set; }

        [Required(ErrorMessage ="Nhập email nhà tổ chức")]
        [StringLength(50)]
        [EmailAddress(ErrorMessage = "Sai định dạng email")]
        [MinLength(6,ErrorMessage ="Tối thiểu 6 ký tự")]
        public string emailNTC { get; set; }

        [Required(ErrorMessage ="Nhập số điện thoại nhà tổ chức")]
        [StringLength(18, ErrorMessage = "Tối đa 18 ký tự")]
        [RegularExpression(@"^[0-9]{10,18}", ErrorMessage = "Nhập sai định dạng số điện thoại")]
        public string phoneNTC { get; set; }

        public bool? statusNTC { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbSuKien> tbSuKiens { get; set; }
    }
}
