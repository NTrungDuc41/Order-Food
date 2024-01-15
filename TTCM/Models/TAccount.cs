using System;
using System.Collections.Generic;

namespace TTCM.Models;

public partial class TAccount
{
    public int MaKhachHang { get; set; }

    public string Username { get; set; } = null!;

    public string? Password { get; set; }

    public string? TenKhachHang { get; set; }

    public string? SoDienThoai { get; set; }

    public string? DiaChi { get; set; }

    public string? Email { get; set; }

    public int? Role { get; set; }

    public virtual ICollection<TGioHang> TGioHangs { get; set; } = new List<TGioHang>();

    public virtual ICollection<THoaDonBan> THoaDonBans { get; set; } = new List<THoaDonBan>();
}
