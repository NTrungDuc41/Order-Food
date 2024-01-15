using TTCM.Models;

namespace TTCM._2Repository
{
    public class NhaHang2Repository : INhaHang2Repository
    {
        private readonly QldoAn4Context _context;
        
        public NhaHang2Repository(QldoAn4Context context)
        {
            _context = context;

        }

        public IEnumerable<TNhaHang> GetAllNhaHang()
        {
            return _context.TNhaHangs;
        }

        public TNhaHang GetNhaHang(string maNh)
        {
            return _context.TNhaHangs.Find(maNh);
        }
    }
}
