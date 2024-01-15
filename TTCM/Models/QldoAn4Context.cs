using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TTCM.Models;

public partial class QldoAn4Context : DbContext
{
    public QldoAn4Context()
    {
    }

    public QldoAn4Context(DbContextOptions<QldoAn4Context> options)
        : base(options)
    {
    }

    public virtual DbSet<TAccount> TAccounts { get; set; }

    public virtual DbSet<TAnh> TAnhs { get; set; }

    public virtual DbSet<TChiTietHdb> TChiTietHdbs { get; set; }

    public virtual DbSet<TDanhMuc> TDanhMucs { get; set; }

    public virtual DbSet<TDoAn> TDoAns { get; set; }

    public virtual DbSet<TGioHang> TGioHangs { get; set; }

    public virtual DbSet<THoaDonBan> THoaDonBans { get; set; }

    public virtual DbSet<THoaDonNhap> THoaDonNhaps { get; set; }

    public virtual DbSet<TNhaHang> TNhaHangs { get; set; }

    public virtual DbSet<TVanChuyen> TVanChuyens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=ADMINMI-7NE0HCP\\SQLEXPRESS;Initial Catalog=QLDoAn4;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TAccount>(entity =>
        {
            entity.HasKey(e => e.MaKhachHang).HasName("pk_tAccount");

            entity.ToTable("tAccount");

            entity.Property(e => e.MaKhachHang).ValueGeneratedNever();
            entity.Property(e => e.DiaChi).HasMaxLength(100);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.SoDienThoai).HasMaxLength(20);
            entity.Property(e => e.TenKhachHang).HasMaxLength(50);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        modelBuilder.Entity<TAnh>(entity =>
        {
            entity.HasKey(e => e.MaDa);

            entity.ToTable("tAnh");

            entity.Property(e => e.MaDa)
                .HasMaxLength(10)
                .HasColumnName("MaDA");
            entity.Property(e => e.TenFileAnh).HasMaxLength(100);

            entity.HasOne(d => d.MaDaNavigation).WithOne(p => p.TAnh)
                .HasForeignKey<TAnh>(d => d.MaDa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tAnh_tDoAn");
        });

        modelBuilder.Entity<TChiTietHdb>(entity =>
        {
            entity.HasKey(e => new { e.MaHdb, e.MaDa }).HasName("pk_tChiTietHDB");

            entity.ToTable("tChiTietHDB");

            entity.Property(e => e.MaHdb)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaHDB");
            entity.Property(e => e.MaDa)
                .HasMaxLength(10)
                .HasColumnName("MaDA");
            entity.Property(e => e.ThanhTien).HasColumnType("money");

            entity.HasOne(d => d.MaDaNavigation).WithMany(p => p.TChiTietHdbs)
                .HasForeignKey(d => d.MaDa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tChiTietHDB_tDoAn");

            entity.HasOne(d => d.MaHdbNavigation).WithMany(p => p.TChiTietHdbs)
                .HasForeignKey(d => d.MaHdb)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietHDB_tHoaDonBan");
        });

        modelBuilder.Entity<TDanhMuc>(entity =>
        {
            entity.HasKey(e => e.MaDanhMuc).HasName("pk_tDanhMuc");

            entity.ToTable("tDanhMuc");

            entity.Property(e => e.MaDanhMuc).HasMaxLength(10);
            entity.Property(e => e.TenDanhMuc).HasMaxLength(50);
        });

        modelBuilder.Entity<TDoAn>(entity =>
        {
            entity.HasKey(e => e.MaDa);

            entity.ToTable("tDoAn");

            entity.Property(e => e.MaDa)
                .HasMaxLength(10)
                .HasColumnName("MaDA");
            entity.Property(e => e.Anh).HasMaxLength(100);
            entity.Property(e => e.GhiChu).HasMaxLength(4000);
            entity.Property(e => e.GiaLonNhat).HasColumnType("money");
            entity.Property(e => e.GiaNhoNhat).HasColumnType("money");
            entity.Property(e => e.MaDanhMuc).HasMaxLength(10);
            entity.Property(e => e.MaNh)
                .HasMaxLength(10)
                .HasColumnName("MaNH");
            entity.Property(e => e.TenDa)
                .HasMaxLength(50)
                .HasColumnName("TenDA");
            entity.Property(e => e.Vote).HasColumnName("vote");

            entity.HasOne(d => d.MaDanhMucNavigation).WithMany(p => p.TDoAns)
                .HasForeignKey(d => d.MaDanhMuc)
                .HasConstraintName("FK_tDoAn_tDanhMuc");

            entity.HasOne(d => d.MaNhNavigation).WithMany(p => p.TDoAns)
                .HasForeignKey(d => d.MaNh)
                .HasConstraintName("FK_tDoAn_tNhaHang");
        });

        modelBuilder.Entity<TGioHang>(entity =>
        {
            entity.HasKey(e => e.IdGh);

            entity.ToTable("tGioHang");

            entity.Property(e => e.IdGh)
                .ValueGeneratedNever()
                .HasColumnName("idGH");
            entity.Property(e => e.DonGia).HasColumnType("money");
            entity.Property(e => e.MaDa)
                .HasMaxLength(10)
                .HasColumnName("MaDA");
            entity.Property(e => e.NgayGhdk)
                .HasColumnType("datetime")
                .HasColumnName("NgayGHDK");
            entity.Property(e => e.ThoiGianGh)
                .HasColumnType("datetime")
                .HasColumnName("ThoiGianGH");

            entity.HasOne(d => d.MaDaNavigation).WithMany(p => p.TGioHangs)
                .HasForeignKey(d => d.MaDa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tGioHang_tDoAn");

            entity.HasOne(d => d.MaKhachHangNavigation).WithMany(p => p.TGioHangs)
                .HasForeignKey(d => d.MaKhachHang)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tGioHang_tAccount");
        });

        modelBuilder.Entity<THoaDonBan>(entity =>
        {
            entity.HasKey(e => e.MaHdb);

            entity.ToTable("tHoaDonBan");

            entity.Property(e => e.MaHdb)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaHDB");
            entity.Property(e => e.DiaChiGh)
                .HasMaxLength(225)
                .HasColumnName("DiaChiGH");
            entity.Property(e => e.GhiChu).HasMaxLength(100);
            entity.Property(e => e.GioGh)
                .HasColumnType("datetime")
                .HasColumnName("GioGH");
            entity.Property(e => e.MaDvvc)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaDVVC");
            entity.Property(e => e.NgayHdb)
                .HasColumnType("datetime")
                .HasColumnName("NgayHDB");
            entity.Property(e => e.PhiVanChuyen).HasColumnType("money");
            entity.Property(e => e.PhuongThucThanhToan).HasMaxLength(50);
            entity.Property(e => e.ThoiGianGh)
                .HasMaxLength(25)
                .HasColumnName("ThoiGianGH");
            entity.Property(e => e.TongTienHdb)
                .HasColumnType("money")
                .HasColumnName("TongTienHDB");

            entity.HasOne(d => d.MaDvvcNavigation).WithMany(p => p.THoaDonBans)
                .HasForeignKey(d => d.MaDvvc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tHoaDonBan_tVanChuyen");

            entity.HasOne(d => d.MaKhachHangNavigation).WithMany(p => p.THoaDonBans)
                .HasForeignKey(d => d.MaKhachHang)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tHoaDonBan_tAccount");
        });

        modelBuilder.Entity<THoaDonNhap>(entity =>
        {
            entity.HasKey(e => e.MaHdn);

            entity.ToTable("tHoaDonNhap");

            entity.Property(e => e.MaHdn)
                .HasMaxLength(25)
                .HasColumnName("MaHDN");
            entity.Property(e => e.GhiChu).HasMaxLength(100);
            entity.Property(e => e.MaDa)
                .HasMaxLength(10)
                .HasColumnName("MaDA");
            entity.Property(e => e.MaDvvc)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaDVVC");
            entity.Property(e => e.MaNh)
                .HasMaxLength(10)
                .HasColumnName("MaNH");
            entity.Property(e => e.NgayHdn)
                .HasColumnType("datetime")
                .HasColumnName("NgayHDN");
            entity.Property(e => e.ThoiGianHdn)
                .HasColumnType("datetime")
                .HasColumnName("ThoiGianHDN");
            entity.Property(e => e.TongTienHdn)
                .HasColumnType("money")
                .HasColumnName("TongTienHDn");

            entity.HasOne(d => d.MaDaNavigation).WithMany(p => p.THoaDonNhaps)
                .HasForeignKey(d => d.MaDa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HoaDonNhap_tDoAn");

            entity.HasOne(d => d.MaDvvcNavigation).WithMany(p => p.THoaDonNhaps)
                .HasForeignKey(d => d.MaDvvc)
                .HasConstraintName("FK_HoaDonNhap_tVanChuyen");

            entity.HasOne(d => d.MaNhNavigation).WithMany(p => p.THoaDonNhaps)
                .HasForeignKey(d => d.MaNh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HoaDonNhap_tNhaHang");
        });

        modelBuilder.Entity<TNhaHang>(entity =>
        {
            entity.HasKey(e => e.MaNh).HasName("pk_tNhaHang");

            entity.ToTable("tNhaHang");

            entity.Property(e => e.MaNh)
                .HasMaxLength(10)
                .HasColumnName("MaNH");
            entity.Property(e => e.AnhNh)
                .HasMaxLength(50)
                .HasColumnName("AnhNH");
            entity.Property(e => e.DiaChi).HasMaxLength(100);
            entity.Property(e => e.TenNhaHang).HasMaxLength(50);
        });

        modelBuilder.Entity<TVanChuyen>(entity =>
        {
            entity.HasKey(e => e.MaDvvc);

            entity.ToTable("tVanChuyen");

            entity.Property(e => e.MaDvvc)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaDVVC");
            entity.Property(e => e.DiaChi).HasMaxLength(150);
            entity.Property(e => e.GhiChu).HasMaxLength(100);
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(15)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.TenDonViVanChuyen)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
