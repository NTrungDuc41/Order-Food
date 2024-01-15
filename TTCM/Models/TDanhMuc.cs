using System;
using System.Collections.Generic;

namespace TTCM.Models;

public partial class TDanhMuc
{
    public string MaDanhMuc { get; set; } = null!;

    public string? TenDanhMuc { get; set; }

    public virtual ICollection<TDoAn> TDoAns { get; set; } = new List<TDoAn>();
}
