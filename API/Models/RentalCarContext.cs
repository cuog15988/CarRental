using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CarRenTal.Models
{
    public partial class RentalCarContext : DbContext
    {
        public RentalCarContext()
        {
        }

        public RentalCarContext(DbContextOptions<RentalCarContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<ChiTietThanhToan> ChiTietThanhToan { get; set; }
        public virtual DbSet<DonHang> DonHang { get; set; }
        public virtual DbSet<Footer> Footer { get; set; }
        public virtual DbSet<HangXe> HangXe { get; set; }
        public virtual DbSet<Header> Header { get; set; }
        public virtual DbSet<HinhThucThanhToan> HinhThucThanhToan { get; set; }
        public virtual DbSet<HomThuLienHe> HomThuLienHe { get; set; }
        public virtual DbSet<HopThu> HopThu { get; set; }
        public virtual DbSet<Huyen> Huyen { get; set; }
        public virtual DbSet<LienHe> LienHe { get; set; }
        public virtual DbSet<LoaiXe> LoaiXe { get; set; }
        public virtual DbSet<PhuongThucThanhToan> PhuongThucThanhToan { get; set; }
        public virtual DbSet<TenXe> TenXe { get; set; }
        public virtual DbSet<Tinh> Tinh { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Xe> Xe { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=THINKPAD\\SQLEXPRESS;Database=RentalCar;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DiaChi).HasMaxLength(301);

                entity.Property(e => e.GroudId).HasColumnName("GroudID");

                entity.Property(e => e.HoTen).HasMaxLength(51);

                entity.Property(e => e.NgayNhap).HasColumnType("datetime");

                entity.Property(e => e.NgaySinh).HasColumnType("datetime");

                entity.Property(e => e.PassWord)
                    .HasMaxLength(51)
                    .IsUnicode(false);

                entity.Property(e => e.UserName).HasMaxLength(51);
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("cart");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Gia).HasColumnName("gia");

                entity.Property(e => e.Ma).HasColumnName("ma");

                entity.Property(e => e.Manguoidang).HasColumnName("manguoidang");

                entity.Property(e => e.Maxe).HasColumnName("maxe");

                entity.Property(e => e.Tenxe)
                    .HasColumnName("tenxe")
                    .HasMaxLength(300);

                entity.HasOne(d => d.MaNavigation)
                    .WithMany(p => p.Cart)
                    .HasForeignKey(d => d.Ma)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__cart__ma__4D5F7D71");

                entity.HasOne(d => d.MaxeNavigation)
                    .WithMany(p => p.Cart)
                    .HasForeignKey(d => d.Maxe)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cart_xe");
            });

            modelBuilder.Entity<ChiTietThanhToan>(entity =>
            {
                entity.HasKey(e => new { e.MaPt, e.MaUs })
                    .HasName("PK__ChiTietT__B557B67B334395BF");

                entity.Property(e => e.MaPt).HasColumnName("MaPT");

                entity.Property(e => e.MaUs).HasColumnName("MaUS");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.NgayNhap).HasColumnType("datetime");

                entity.HasOne(d => d.MaPtNavigation)
                    .WithMany(p => p.ChiTietThanhToan)
                    .HasForeignKey(d => d.MaPt)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChiTietTha__MaPT__47DBAE45");

                entity.HasOne(d => d.MaUsNavigation)
                    .WithMany(p => p.ChiTietThanhToan)
                    .HasForeignKey(d => d.MaUs)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChiTietTha__MaUS__46E78A0C");
            });

            modelBuilder.Entity<DonHang>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DenNgay).HasColumnType("datetime");

                entity.Property(e => e.Giamgia).HasColumnName("giamgia");

                entity.Property(e => e.Huy).HasColumnName("huy");

                entity.Property(e => e.MaUs).HasColumnName("MaUS");

                entity.Property(e => e.NgayLap).HasColumnType("datetime");

                entity.Property(e => e.Songay).HasColumnName("songay");

                entity.Property(e => e.TuNgay).HasColumnType("datetime");

                entity.Property(e => e.Xacnhan).HasColumnName("xacnhan");

                entity.HasOne(d => d.MaLoaiThanhToanNavigation)
                    .WithMany(p => p.DonHang)
                    .HasForeignKey(d => d.MaLoaiThanhToan)
                    .HasConstraintName("FK_DH_HTTT");

                entity.HasOne(d => d.MaUsNavigation)
                    .WithMany(p => p.DonHang)
                    .HasForeignKey(d => d.MaUs)
                    .HasConstraintName("FK_DH_User");

                entity.HasOne(d => d.MaXeNavigation)
                    .WithMany(p => p.DonHang)
                    .HasForeignKey(d => d.MaXe)
                    .HasConstraintName("FK_Xe_DonHang");
            });

            modelBuilder.Entity<Footer>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Images)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Noidung).HasColumnName("noidung");

                entity.Property(e => e.TieuDe).HasMaxLength(300);
            });

            modelBuilder.Entity<HangXe>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.TenHang).HasMaxLength(300);

                entity.HasOne(d => d.MaLoaiXeNavigation)
                    .WithMany(p => p.HangXe)
                    .HasForeignKey(d => d.MaLoaiXe)
                    .HasConstraintName("FK_1");
            });

            modelBuilder.Entity<Header>(entity =>
            {
                entity.ToTable("header");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Images)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Noidung).HasColumnName("noidung");

                entity.Property(e => e.TieuDe).HasMaxLength(300);
            });

            modelBuilder.Entity<HinhThucThanhToan>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.TenLoai).HasMaxLength(300);
            });

            modelBuilder.Entity<HomThuLienHe>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.NgayGui).HasColumnType("datetime");

                entity.Property(e => e.NguoiGui).HasMaxLength(51);
            });

            modelBuilder.Entity<HopThu>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.MaUs).HasColumnName("MaUS");

                entity.Property(e => e.NgayGui).HasColumnType("datetime");

                entity.Property(e => e.TieuDe).HasMaxLength(1000);

                entity.HasOne(d => d.MaUsNavigation)
                    .WithMany(p => p.HopThu)
                    .HasForeignKey(d => d.MaUs)
                    .HasConstraintName("FK_Mail_Users");
            });

            modelBuilder.Entity<Huyen>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.TenHuyen).HasMaxLength(100);

                entity.HasOne(d => d.MaTinhNavigation)
                    .WithMany(p => p.Huyen)
                    .HasForeignKey(d => d.MaTinh)
                    .HasConstraintName("Tinh_Huyen");
            });

            modelBuilder.Entity<LienHe>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Link)
                    .HasMaxLength(1000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LoaiXe>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.TenLoai).HasMaxLength(300);
            });

            modelBuilder.Entity<PhuongThucThanhToan>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.TenPt)
                    .HasColumnName("TenPT")
                    .HasMaxLength(1);
            });

            modelBuilder.Entity<TenXe>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.TenXe1)
                    .HasColumnName("TenXe")
                    .HasMaxLength(300);

                entity.HasOne(d => d.MaHangXeNavigation)
                    .WithMany(p => p.TenXe)
                    .HasForeignKey(d => d.MaHangXe)
                    .HasConstraintName("FK_TenXe_hangXe");
            });

            modelBuilder.Entity<Tinh>(entity =>
            {
                entity.HasKey(e => e.Ma)
                    .HasName("PK__Tinh__3213C8B711E58E41");

                entity.Property(e => e.Ma)
                    .HasColumnName("ma")
                    .ValueGeneratedNever();

                entity.Property(e => e.TenTinh).HasMaxLength(100);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DiaChi).HasMaxLength(301);

                entity.Property(e => e.Email)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Gioitinh).HasColumnName("gioitinh");

                entity.Property(e => e.GroudId).HasColumnName("GroudID");

                entity.Property(e => e.HoTen).HasMaxLength(51);

                entity.Property(e => e.NgayNhap).HasColumnType("datetime");

                entity.Property(e => e.NgaySinh).HasColumnType("datetime");

                entity.Property(e => e.PassWord)
                    .HasMaxLength(51)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.UserName).HasMaxLength(51);
            });

            modelBuilder.Entity<Xe>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Doi)
                    .HasColumnName("doi")
                    .HasMaxLength(100);

                entity.Property(e => e.Gia).HasColumnName("gia");

                entity.Property(e => e.Hinh)
                    .HasColumnName("hinh")
                    .IsUnicode(false);

                entity.Property(e => e.Huyen).HasMaxLength(100);

                entity.Property(e => e.LoaiXe).HasMaxLength(300);

                entity.Property(e => e.Mota).HasColumnName("mota");

                entity.Property(e => e.NgayNhap).HasColumnType("datetime");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TenHang).HasMaxLength(300);

                entity.Property(e => e.TenLoai).HasMaxLength(300);

                entity.Property(e => e.TenNguoiDang).HasMaxLength(100);

                entity.Property(e => e.Tenxe)
                    .HasColumnName("tenxe")
                    .HasMaxLength(300);

                entity.Property(e => e.Tinh).HasMaxLength(100);

                entity.HasOne(d => d.MaHangXeNavigation)
                    .WithMany(p => p.Xe)
                    .HasForeignKey(d => d.MaHangXe)
                    .HasConstraintName("FK__Xe__MaHangXe__74794A92");

                entity.HasOne(d => d.MaHuyenNavigation)
                    .WithMany(p => p.Xe)
                    .HasForeignKey(d => d.MaHuyen)
                    .HasConstraintName("FK__Xe__MaHuyen__73852659");

                entity.HasOne(d => d.MaNguoiDangNavigation)
                    .WithMany(p => p.Xe)
                    .HasForeignKey(d => d.MaNguoiDang)
                    .HasConstraintName("FK_Xe_Users");

                entity.HasOne(d => d.MaTenXeNavigation)
                    .WithMany(p => p.Xe)
                    .HasForeignKey(d => d.MaTenXe)
                    .HasConstraintName("FK__Xe__MaTenXe__756D6ECB");
            });
        }
    }
}
