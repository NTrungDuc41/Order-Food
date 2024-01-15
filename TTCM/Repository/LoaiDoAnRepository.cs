using TTCM.Models;

namespace TTCM.Repository
{
    public class LoaiDoAnRepository : ILoaiDoAnRepository
    {
        private readonly QldoAn4Context _context;

        public LoaiDoAnRepository(QldoAn4Context context)
        {
            _context = context;

        }

        public IEnumerable<TDanhMuc> GetAllDanhMucDA()
        {
            return _context.TDanhMucs;
        }

        public TDanhMuc GetDanhMuc(string madanhmuc)
        {
            return _context.TDanhMucs.Find(madanhmuc);
        }
    }
}
