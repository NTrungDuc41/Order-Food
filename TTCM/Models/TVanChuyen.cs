using System;
using System.Collections.Generic;

namespace TTCM.Models;

public partial class TVanChuyen
{
    public string MaDvvc { get; set; } = null!;

    public string? TenDonViVanChuyen { get; set; }

    public string? SoDienThoai { get; set; }

    public string? DiaChi { get; set; }

    public decimal? GhiChu { get; set; }

    public virtual ICollection<THoaDonBan> THoaDonBans { get; set; } = new List<THoaDonBan>();

    public virtual ICollection<THoaDonNhap> THoaDonNhaps { get; set; } = new List<THoaDonNhap>();
}
