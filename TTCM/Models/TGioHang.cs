using System;
using System.Collections.Generic;

namespace TTCM.Models;

public partial class TGioHang
{
    public int IdGh { get; set; }

    public int MaKhachHang { get; set; }

    public string MaDa { get; set; } = null!;

    public DateTime? NgayGhdk { get; set; }

    public DateTime? ThoiGianGh { get; set; }

    public int? SoLuong { get; set; }

    public decimal? DonGia { get; set; }

    public double? ThanhTien { get; set; }

    public virtual TDoAn MaDaNavigation { get; set; } = null!;

    public virtual TAccount MaKhachHangNavigation { get; set; } = null!;
}
