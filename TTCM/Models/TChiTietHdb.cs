using System;
using System.Collections.Generic;

namespace TTCM.Models;

public partial class TChiTietHdb
{
    public string MaHdb { get; set; } = null!;

    public string MaDa { get; set; } = null!;

    public int? SoLuongBan { get; set; }

    public decimal? ThanhTien { get; set; }

    public virtual TDoAn MaDaNavigation { get; set; } = null!;

    public virtual THoaDonBan MaHdbNavigation { get; set; } = null!;
}
