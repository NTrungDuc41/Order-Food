using System;
using System.Collections.Generic;

namespace TTCM.Models;

public partial class TNhaHang
{
    public string MaNh { get; set; } = null!;

    public string? TenNhaHang { get; set; }

    public string? DiaChi { get; set; }

    public string? AnhNh { get; set; }

    public virtual ICollection<TDoAn> TDoAns { get; set; } = new List<TDoAn>();

    public virtual ICollection<THoaDonNhap> THoaDonNhaps { get; set; } = new List<THoaDonNhap>();
}
