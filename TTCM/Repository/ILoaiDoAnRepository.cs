using TTCM.Models;
namespace TTCM.Repository
{
    public interface ILoaiDoAnRepository
    {
        TDanhMuc GetDanhMuc(String madanhmuc);
        IEnumerable<TDanhMuc> GetAllDanhMucDA();
    }
}
