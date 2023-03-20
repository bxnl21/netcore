using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ShopGame.Models
{
    public partial class DbQuanLyShopGameContext : DbContext
    {
        public DbQuanLyShopGameContext()
        {
        }

        public DbQuanLyShopGameContext(DbContextOptions<DbQuanLyShopGameContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<LoaiSanPham> LoaiSanPhams { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public object Session { get; internal set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-5LOKFKO\\SQLEXPRESS;Database=DbQuanLyShopGame;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ChiTietHoaDon>(entity =>
            {
                entity.HasKey(e => e.MaChiTietHoaDon)
                    .HasName("PK__ChiTietH__CFF2C4261E948A87");

                entity.ToTable("ChiTietHoaDon");

                entity.Property(e => e.DonGia).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.MaHoaDonNavigation)
                    .WithMany(p => p.ChiTietHoaDons)
                    .HasForeignKey(d => d.MaHoaDon)
                    .HasConstraintName("FK__ChiTietHo__MaHoa__173876EA");

                entity.HasOne(d => d.MaSanPhamNavigation)
                    .WithMany(p => p.ChiTietHoaDons)
                    .HasForeignKey(d => d.MaSanPham)
                    .HasConstraintName("FK__ChiTietHo__MaSan__182C9B23");
            });

            modelBuilder.Entity<HoaDon>(entity =>
            {
                entity.HasKey(e => e.MaHoaDon)
                    .HasName("PK__HoaDon__835ED13B4F2A5D8B");

                entity.ToTable("HoaDon");

                entity.Property(e => e.DiaChi).HasMaxLength(200);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SoDienThoai)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.TenKhachHang).HasMaxLength(100);

                entity.Property(e => e.TongTien).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.HasKey(e => e.MaKhachHang)
                    .HasName("PK__KhachHan__88D2F0E552599B7A");

                entity.ToTable("KhachHang");

                entity.Property(e => e.HoTenKhachHang).HasMaxLength(100);

                entity.Property(e => e.Id)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PassWordd)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LoaiSanPham>(entity =>
            {
                entity.HasKey(e => e.MaLoai)
                    .HasName("PK__LoaiSanP__730A57592BCE3C56");

                entity.ToTable("LoaiSanPham");

                entity.Property(e => e.TenLoai).HasMaxLength(50);
            });

            modelBuilder.Entity<NhanVien>(entity =>
            {
                entity.HasKey(e => e.MaNhanVien)
                    .HasName("PK__NhanVien__77B2CA4763503538");

                entity.ToTable("NhanVien");

                entity.Property(e => e.HoTenNhanVien).HasMaxLength(100);

                entity.Property(e => e.Id)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PassWordd)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SanPham>(entity =>
            {
                entity.HasKey(e => e.MaSanPham)
                    .HasName("PK__SanPham__FAC7442DA31CC73B");

                entity.ToTable("SanPham");

                entity.Property(e => e.Anh)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.GiaBan).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TenSanPham).HasMaxLength(100);

                entity.HasOne(d => d.MaLoaiNavigation)
                    .WithMany(p => p.SanPhams)
                    .HasForeignKey(d => d.MaLoai)
                    .HasConstraintName("FK__SanPham__MaLoai__1273C1CD");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
