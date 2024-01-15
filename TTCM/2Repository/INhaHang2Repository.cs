using TTCM.Models;
namespace TTCM._2Repository
{
    public interface INhaHang2Repository
    {
        TNhaHang GetNhaHang(String maNh);
        IEnumerable<TNhaHang> GetAllNhaHang();

    }
}
