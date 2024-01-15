using System;
using System.Collections.Generic;

namespace TTCM.Models;

public partial class THoaDonBan
{
    public string MaHdb { get; set; } = null!;

    public DateTime? NgayHdb { get; set; }

    public DateTime? GioGh { get; set; }

    public string? ThoiGianGh { get; set; }

    public int MaKhachHang { get; set; }

    public string MaDvvc { get; set; } = null!;

    public decimal? TongTienHdb { get; set; }

    public string? DiaChiGh { get; set; }

    public decimal? PhiVanChuyen { get; set; }

    public string? PhuongThucThanhToan { get; set; }

    public string? GhiChu { get; set; }

    public int? Status { get; set; }

    public virtual TVanChuyen MaDvvcNavigation { get; set; } = null!;

    public virtual TAccount MaKhachHangNavigation { get; set; } = null!;

    public virtual ICollection<TChiTietHdb> TChiTietHdbs { get; set; } = new List<TChiTietHdb>();
}
