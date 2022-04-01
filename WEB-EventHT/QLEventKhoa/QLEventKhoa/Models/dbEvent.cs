namespace QLEventKhoa.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class dbEvent : DbContext
    {
        public dbEvent()
            : base("name=dbEvent")
        {
        }

        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<tbAdmin> tbAdmins { get; set; }
        public virtual DbSet<tbKhoa> tbKhoas { get; set; }
        public virtual DbSet<tbLienHe> tbLienHes { get; set; }
        public virtual DbSet<tbLoaiAdmin> tbLoaiAdmins { get; set; }
        public virtual DbSet<tbLoaiEvent> tbLoaiEvents { get; set; }
        public virtual DbSet<tbLoaiLH> tbLoaiLHs { get; set; }
        public virtual DbSet<tbLopHoc> tbLopHocs { get; set; }
        public virtual DbSet<tbNhaToChuc> tbNhaToChucs { get; set; }
        public virtual DbSet<tbSinhVien> tbSinhViens { get; set; }
        public virtual DbSet<tbSuKien> tbSuKiens { get; set; }
        public virtual DbSet<tbTagEvent> tbTagEvents { get; set; }
        public virtual DbSet<tbTaiKhoanSV> tbTaiKhoanSVs { get; set; }
        public virtual DbSet<tbThamGiaEvent> tbThamGiaEvents { get; set; }
        public virtual DbSet<tbTrangThaiEvent> tbTrangThaiEvents { get; set; }
      

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tbAdmin>()
                .Property(e => e.userAdmin)
                .IsUnicode(false);

            modelBuilder.Entity<tbAdmin>()
                .Property(e => e.passAdmin)
                .IsUnicode(false);

            modelBuilder.Entity<tbAdmin>()
                .HasMany(e => e.tbLienHes)
                .WithRequired(e => e.tbAdmin)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbLopHoc>()
               .Property(e => e.tenLopHoc)
               .IsUnicode(false);


            modelBuilder.Entity<tbKhoa>()
                .Property(e => e.maKHoa)
                .IsUnicode(false);

            modelBuilder.Entity<tbKhoa>()
                .HasMany(e => e.tbSinhViens)
                .WithRequired(e => e.tbKhoa)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbKhoa>()
                .HasMany(e => e.tbSuKiens)
                .WithRequired(e => e.tbKhoa)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbKhoa>()
                .HasMany(e => e.tbLopHocs)
                .WithRequired(e => e.tbKhoa)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbLienHe>()
                .Property(e => e.phoneSV)
                .IsUnicode(false);

            modelBuilder.Entity<tbLienHe>()
                .Property(e => e.maSV)
                .IsUnicode(false);

            modelBuilder.Entity<tbLienHe>()
                .Property(e => e.userAdmin)
                .IsUnicode(false);

            modelBuilder.Entity<tbLoaiAdmin>()
                .HasMany(e => e.tbAdmins)
                .WithRequired(e => e.tbLoaiAdmin)
                .HasForeignKey(e => e.idloaiql)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbLoaiEvent>()
                .HasMany(e => e.tbSuKiens)
                .WithRequired(e => e.tbLoaiEvent)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbNhaToChuc>()
                .Property(e => e.maNTC)
                .IsUnicode(false);

            modelBuilder.Entity<tbNhaToChuc>()
                .Property(e => e.phoneNTC)
                .IsUnicode(false);

            modelBuilder.Entity<tbSinhVien>()
                .Property(e => e.maSV)
                .IsUnicode(false);

            modelBuilder.Entity<tbSinhVien>()
                .Property(e => e.phoneSV)
                .IsUnicode(false);

            modelBuilder.Entity<tbSinhVien>()
                .HasMany(e => e.tbLienHes)
                .WithRequired(e => e.tbSinhVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbLopHoc>()
                .HasMany(e => e.tbSinhViens)
                .WithRequired(e => e.tbLopHoc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbSinhVien>()
                .HasOptional(e => e.tbTaiKhoanSV)
                .WithRequired(e => e.tbSinhVien)
                .WillCascadeOnDelete(true);
                

            modelBuilder.Entity<tbSinhVien>()
                .HasMany(e => e.tbThamGiaEvents)
                .WithRequired(e => e.tbSinhVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbSuKien>()
                .Property(e => e.maThamGia)
                .IsUnicode(false);

            modelBuilder.Entity<tbSuKien>()
                .HasMany(e => e.tbThamGiaEvents)
                .WithRequired(e => e.tbSuKien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbTagEvent>()
                .HasMany(e => e.tbSuKiens)
                .WithRequired(e => e.tbTagEvent)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbTaiKhoanSV>()
                .Property(e => e.maSV)
                .IsUnicode(false);

            modelBuilder.Entity<tbTaiKhoanSV>()
                .Property(e => e.matKhauSV)
                .IsUnicode(false);

            modelBuilder.Entity<tbThamGiaEvent>()
                .Property(e => e.maSV)
                .IsUnicode(false);

            modelBuilder.Entity<tbThamGiaEvent>()
                .Property(e => e.phoneSV)
                .IsUnicode(false);

      
        }
    }
}
