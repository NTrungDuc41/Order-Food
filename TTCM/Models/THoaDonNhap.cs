using System;
using System.Collections.Generic;

namespace TTCM.Models;

public partial class THoaDonNhap
{
    public string MaHdn { get; set; } = null!;

    public DateTime? NgayHdn { get; set; }

    public DateTime? ThoiGianHdn { get; set; }

    public string MaDa { get; set; } = null!;

    public string MaNh { get; set; } = null!;

    public string? MaDvvc { get; set; }

    public int? SoLuongNhap { get; set; }

    public decimal? TongTienHdn { get; set; }

    public string? GhiChu { get; set; }

    public virtual TDoAn MaDaNavigation { get; set; } = null!;

    public virtual TVanChuyen? MaDvvcNavigation { get; set; }

    public virtual TNhaHang MaNhNavigation { get; set; } = null!;
}
