using System;
using System.Collections.Generic;

namespace TTCM.Models;

public partial class TDoAn
{
    public string MaDa { get; set; } = null!;

    public string? Anh { get; set; }

    public string TenDa { get; set; } = null!;

    public string? MaDanhMuc { get; set; }

    public string? MaNh { get; set; }

    public decimal? GiaLonNhat { get; set; }

    public decimal? GiaNhoNhat { get; set; }

    public string? GhiChu { get; set; }

    public int? Vote { get; set; }

    public virtual TDanhMuc? MaDanhMucNavigation { get; set; }

    public virtual TNhaHang? MaNhNavigation { get; set; }

    public virtual TAnh? TAnh { get; set; }

    public virtual ICollection<TChiTietHdb> TChiTietHdbs { get; set; } = new List<TChiTietHdb>();

    public virtual ICollection<TGioHang> TGioHangs { get; set; } = new List<TGioHang>();

    public virtual ICollection<THoaDonNhap> THoaDonNhaps { get; set; } = new List<THoaDonNhap>();
}
