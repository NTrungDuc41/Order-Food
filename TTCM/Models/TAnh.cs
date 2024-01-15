using System;
using System.Collections.Generic;

namespace TTCM.Models;

public partial class TAnh
{
    public string MaDa { get; set; } = null!;

    public string TenFileAnh { get; set; } = null!;

    public virtual TDoAn MaDaNavigation { get; set; } = null!;
}
