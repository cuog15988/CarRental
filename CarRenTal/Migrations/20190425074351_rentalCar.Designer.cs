﻿// <auto-generated />
using System;
using CarRenTal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CarRenTal.Migrations
{
    [DbContext(typeof(RentalCarContext))]
    [Migration("20190425074351_rentalCar")]
    partial class rentalCar
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CarRenTal.Models.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DiaChi")
                        .HasMaxLength(301);

                    b.Property<int?>("GroudId")
                        .HasColumnName("GroudID");

                    b.Property<string>("HoTen")
                        .HasMaxLength(51);

                    b.Property<DateTime?>("NgayNhap")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("NgaySinh")
                        .HasColumnType("datetime");

                    b.Property<string>("PassWord")
                        .HasMaxLength(51)
                        .IsUnicode(false);

                    b.Property<bool?>("Status");

                    b.Property<string>("UserName")
                        .HasMaxLength(51);

                    b.HasKey("Id");

                    b.ToTable("Admin");
                });

            modelBuilder.Entity("CarRenTal.Models.ChiTietThanhToan", b =>
                {
                    b.Property<int>("MaPt")
                        .HasColumnName("MaPT");

                    b.Property<int>("MaUs")
                        .HasColumnName("MaUS");

                    b.Property<string>("Code")
                        .HasColumnName("code")
                        .HasMaxLength(300)
                        .IsUnicode(false);

                    b.Property<DateTime?>("NgayNhap")
                        .HasColumnType("datetime");

                    b.HasKey("MaPt", "MaUs")
                        .HasName("PK__ChiTietT__B557B67B334395BF");

                    b.HasIndex("MaUs");

                    b.ToTable("ChiTietThanhToan");
                });

            modelBuilder.Entity("CarRenTal.Models.DonHang", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DenNgay")
                        .HasColumnType("datetime");

                    b.Property<int?>("MaLoaiThanhToan");

                    b.Property<int?>("MaUs")
                        .HasColumnName("MaUS");

                    b.Property<int?>("MaXe");

                    b.Property<DateTime?>("NgayLap")
                        .HasColumnType("datetime");

                    b.Property<bool?>("Status");

                    b.Property<bool?>("TinhTrangThanhToan");

                    b.Property<DateTime?>("TuNgay")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("MaLoaiThanhToan");

                    b.HasIndex("MaUs");

                    b.HasIndex("MaXe");

                    b.ToTable("DonHang");
                });

            modelBuilder.Entity("CarRenTal.Models.Footer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Images")
                        .HasMaxLength(1000)
                        .IsUnicode(false);

                    b.Property<string>("Noidung")
                        .HasColumnName("noidung");

                    b.Property<string>("TieuDe")
                        .HasMaxLength(300);

                    b.HasKey("Id");

                    b.ToTable("Footer");
                });

            modelBuilder.Entity("CarRenTal.Models.HangXe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("MaLoaiXe");

                    b.Property<string>("TenHang")
                        .HasMaxLength(300);

                    b.HasKey("Id");

                    b.HasIndex("MaLoaiXe");

                    b.ToTable("HangXe");
                });

            modelBuilder.Entity("CarRenTal.Models.Header", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Images")
                        .HasMaxLength(1000)
                        .IsUnicode(false);

                    b.Property<string>("Noidung")
                        .HasColumnName("noidung");

                    b.Property<string>("TieuDe")
                        .HasMaxLength(300);

                    b.HasKey("Id");

                    b.ToTable("header");
                });

            modelBuilder.Entity("CarRenTal.Models.HinhThucThanhToan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TenLoai")
                        .HasMaxLength(300);

                    b.HasKey("Id");

                    b.ToTable("HinhThucThanhToan");
                });

            modelBuilder.Entity("CarRenTal.Models.HomThuLienHe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnName("email")
                        .HasMaxLength(300)
                        .IsUnicode(false);

                    b.Property<DateTime?>("NgayGui")
                        .HasColumnType("datetime");

                    b.Property<string>("NguoiGui")
                        .HasMaxLength(51);

                    b.Property<string>("NoiDung");

                    b.HasKey("Id");

                    b.ToTable("HomThuLienHe");
                });

            modelBuilder.Entity("CarRenTal.Models.HopThu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("MaUs")
                        .HasColumnName("MaUS");

                    b.Property<DateTime?>("NgayGui")
                        .HasColumnType("datetime");

                    b.Property<int?>("NguoiGui");

                    b.Property<string>("NoiDung");

                    b.Property<string>("TieuDe")
                        .HasMaxLength(1000);

                    b.HasKey("Id");

                    b.HasIndex("MaUs");

                    b.ToTable("HopThu");
                });

            modelBuilder.Entity("CarRenTal.Models.Huyen", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("MaTinh")
                        .HasMaxLength(3)
                        .IsUnicode(false);

                    b.Property<string>("TenHuyen")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("MaTinh");

                    b.ToTable("Huyen");
                });

            modelBuilder.Entity("CarRenTal.Models.LienHe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Link")
                        .HasMaxLength(1000)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("LienHe");
                });

            modelBuilder.Entity("CarRenTal.Models.LoaiXe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TenLoai")
                        .HasMaxLength(300);

                    b.HasKey("Id");

                    b.ToTable("LoaiXe");
                });

            modelBuilder.Entity("CarRenTal.Models.PhuongThucThanhToan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasMaxLength(300)
                        .IsUnicode(false);

                    b.Property<string>("GhiChu");

                    b.Property<string>("TenPt")
                        .HasColumnName("TenPT")
                        .HasMaxLength(1);

                    b.HasKey("Id");

                    b.ToTable("PhuongThucThanhToan");
                });

            modelBuilder.Entity("CarRenTal.Models.TenXe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Doi");

                    b.Property<int?>("MaHangXe");

                    b.Property<string>("TenXe1")
                        .HasColumnName("TenXe")
                        .HasMaxLength(300);

                    b.HasKey("Id");

                    b.HasIndex("MaHangXe");

                    b.ToTable("TenXe");
                });

            modelBuilder.Entity("CarRenTal.Models.Tinh", b =>
                {
                    b.Property<string>("Ma")
                        .HasColumnName("ma")
                        .HasMaxLength(3)
                        .IsUnicode(false);

                    b.Property<string>("TenTinh")
                        .HasMaxLength(100);

                    b.HasKey("Ma")
                        .HasName("PK__Tinh__3213C8B7BDAE7834");

                    b.ToTable("Tinh");
                });

            modelBuilder.Entity("CarRenTal.Models.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DiaChi")
                        .HasMaxLength(301);

                    b.Property<bool?>("Gioitinh")
                        .HasColumnName("gioitinh");

                    b.Property<int?>("GroudId")
                        .HasColumnName("GroudID");

                    b.Property<string>("HoTen")
                        .HasMaxLength(51);

                    b.Property<DateTime?>("NgayNhap")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("NgaySinh")
                        .HasColumnType("datetime");

                    b.Property<string>("PassWord")
                        .HasMaxLength(51)
                        .IsUnicode(false);

                    b.Property<bool?>("Status");

                    b.Property<string>("UserName")
                        .HasMaxLength(51);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CarRenTal.Models.Xe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Huyen")
                        .HasMaxLength(100);

                    b.Property<int?>("MaNguoiDang");

                    b.Property<int?>("MaTenXe");

                    b.Property<DateTime?>("NgayNhap")
                        .HasColumnType("datetime");

                    b.Property<string>("Tinh")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("MaNguoiDang");

                    b.HasIndex("MaTenXe");

                    b.ToTable("Xe");
                });

            modelBuilder.Entity("CarRenTal.Models.ChiTietThanhToan", b =>
                {
                    b.HasOne("CarRenTal.Models.Users", "MaPtNavigation")
                        .WithMany("ChiTietThanhToan")
                        .HasForeignKey("MaPt")
                        .HasConstraintName("FK__ChiTietTha__MaPT__47DBAE45");

                    b.HasOne("CarRenTal.Models.PhuongThucThanhToan", "MaUsNavigation")
                        .WithMany("ChiTietThanhToan")
                        .HasForeignKey("MaUs")
                        .HasConstraintName("FK__ChiTietTha__MaUS__46E78A0C");
                });

            modelBuilder.Entity("CarRenTal.Models.DonHang", b =>
                {
                    b.HasOne("CarRenTal.Models.HinhThucThanhToan", "MaLoaiThanhToanNavigation")
                        .WithMany("DonHang")
                        .HasForeignKey("MaLoaiThanhToan")
                        .HasConstraintName("FK_DH_HTTT");

                    b.HasOne("CarRenTal.Models.Users", "MaUsNavigation")
                        .WithMany("DonHang")
                        .HasForeignKey("MaUs")
                        .HasConstraintName("FK_DH_User");

                    b.HasOne("CarRenTal.Models.Xe", "MaXeNavigation")
                        .WithMany("DonHang")
                        .HasForeignKey("MaXe")
                        .HasConstraintName("FK_Xe_DonHang");
                });

            modelBuilder.Entity("CarRenTal.Models.HangXe", b =>
                {
                    b.HasOne("CarRenTal.Models.LoaiXe", "MaLoaiXeNavigation")
                        .WithMany("HangXe")
                        .HasForeignKey("MaLoaiXe")
                        .HasConstraintName("FK_1");
                });

            modelBuilder.Entity("CarRenTal.Models.HopThu", b =>
                {
                    b.HasOne("CarRenTal.Models.Users", "MaUsNavigation")
                        .WithMany("HopThu")
                        .HasForeignKey("MaUs")
                        .HasConstraintName("FK_Mail_Users");
                });

            modelBuilder.Entity("CarRenTal.Models.Huyen", b =>
                {
                    b.HasOne("CarRenTal.Models.Tinh", "MaTinhNavigation")
                        .WithMany("Huyen")
                        .HasForeignKey("MaTinh")
                        .HasConstraintName("Tinh_Huyen");
                });

            modelBuilder.Entity("CarRenTal.Models.TenXe", b =>
                {
                    b.HasOne("CarRenTal.Models.HangXe", "MaHangXeNavigation")
                        .WithMany("TenXe")
                        .HasForeignKey("MaHangXe")
                        .HasConstraintName("FK_TenXe_hangXe");
                });

            modelBuilder.Entity("CarRenTal.Models.Xe", b =>
                {
                    b.HasOne("CarRenTal.Models.Users", "MaNguoiDangNavigation")
                        .WithMany("Xe")
                        .HasForeignKey("MaNguoiDang")
                        .HasConstraintName("FK_Xe_Users");

                    b.HasOne("CarRenTal.Models.TenXe", "MaTenXeNavigation")
                        .WithMany("Xe")
                        .HasForeignKey("MaTenXe")
                        .HasConstraintName("FK_Xe_hangXe");
                });
#pragma warning restore 612, 618
        }
    }
}
