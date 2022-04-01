namespace QLEventKhoa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("tbSuKien")]
    public partial class tbSuKien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbSuKien()
        {
            tbThamGiaEvents = new HashSet<tbThamGiaEvent>();
        }

        [Key]
        public int idEvent { get; set; }

        [Required(ErrorMessage = "Nhập tên sự kiện")]
        [StringLength(255)]
        public string tenEvent { get; set; }

        [Required(ErrorMessage = "Nhập nơi tổ chức")]
        [StringLength(100)]
        public string noiToChuc { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime ngayBatDau { get; set; }


        [Required]
        [StringLength(4000)]
        [AllowHtml]
        public string noiDungEvent { get; set; }

        [StringLength(255)]
        public string imageEvent { get; set; }

        [StringLength(255)]
        public string videoEvent { get; set; }

        [Required(ErrorMessage = "Nhập hoặc random mã tham gia")]
        [StringLength(5)]
        public string maThamGia { get; set; }

        [Required(ErrorMessage = "Chọn số lượng tham gia")]
        [Range(50,5000,ErrorMessage ="Chọn số lượng tham gia từ 50 đến 5000")]
        public int? soLuongThamGia { get; set; }

        public int? chiPhiEvent { get; set; }

        [Required(ErrorMessage = "Chọn loại sự kiện")]
        public int idLoaiEvent { get; set; }

      //  [Required(ErrorMessage = "Chọn tag sự kiện")]
        public int? idTagEvent { get; set; }


        [Required(ErrorMessage = "Chọn khoa tổ chức")]
        public int idKhoa { get; set; }

       // [Required(ErrorMessage = "Chọn nhà tổ chức")]
        public int? idNhaTC { get; set; }

        [Required(ErrorMessage = "Chọn trạng thái sự kiện")]
        public int statusEvent { get; set; }
      
    

    public virtual tbKhoa tbKhoa { get; set; }

        public virtual tbLoaiEvent tbLoaiEvent { get; set; }

        public virtual tbNhaToChuc tbNhaToChuc { get; set; }

        public virtual tbTagEvent tbTagEvent { get; set; }

        public virtual tbTrangThaiEvent tbTrangThaiEvent { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbThamGiaEvent> tbThamGiaEvents { get; set; }


        public List<tbKhoa> listKhoa = new List<tbKhoa>();

        public List<tbLoaiEvent> listLoaiEvent = new List<tbLoaiEvent>();

        public List<tbNhaToChuc> listNTC = new List<tbNhaToChuc>();
        
        public List<tbTagEvent> listTag = new List<tbTagEvent>();
        public List<tbTrangThaiEvent> listStatus = new List<tbTrangThaiEvent>();

        public bool isThmaGia = false;
        [StringLength(128)]
        public string KeHoach { get; set; }
      
        public bool? SKDX { get; set; }
        [StringLength(100)]
        public string EmailDX { get; set; }

    }
}
