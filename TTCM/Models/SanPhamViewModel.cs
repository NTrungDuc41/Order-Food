namespace TTCM.Models
{
    public class SanPhamViewModel
    {
        public TDoAn SanPham { get; set; }
        public List<NhaHangViewModel> ListNH { get; set; }

        public string TenDa { get; set; }
        public string Anh { get; set; }
        public string GiaNhoNhat { get; set; }

        public string GhiChu { get; set; }
    }
}
