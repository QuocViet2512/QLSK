namespace QLEventKhoa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbSinhVien")]
    public partial class tbSinhVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbSinhVien()
        {
            tbLienHes = new HashSet<tbLienHe>();
            tbThamGiaEvents = new HashSet<tbThamGiaEvent>();
        }

        [Key]
        public int idSinhVien { get; set; }

        
        [StringLength(10)]
        public string maSV { get; set; }

        [StringLength(50)]
        public string tenSV { get; set; }

        
        [StringLength(50,ErrorMessage ="Tối đa 50 ký tự")]
        [Required(ErrorMessage = "Nhập Email")]
        [EmailAddress(ErrorMessage = "Sai định dạng email")]
        public string emailSV { get; set; }

        [Column(TypeName = "date")]
        public DateTime ngaySinhSV { get; set; }

     
        [StringLength(18,ErrorMessage ="Tối đa 18 ký tự")]
        [Required(ErrorMessage = "Nhập số điện thoại")]
        [RegularExpression(@"^[0-9]{10,18}", ErrorMessage = "Nhập sai định dạng số điện thoại")]
        public string phoneSV { get; set; }

        public int idKhoa { get; set; }

        public bool? statusSV { get; set; }

        public int? idLopHoc { get; set; }

        public bool? isLopTruong { get; set; }

        public virtual tbKhoa tbKhoa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbLienHe> tbLienHes { get; set; }

        public virtual tbTaiKhoanSV tbTaiKhoanSV { get; set; }

        public virtual tbLopHoc tbLopHoc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbThamGiaEvent> tbThamGiaEvents { get; set; }

      //  public List<tbKhoa> listKhoa = new List<tbKhoa>();
    }
}
