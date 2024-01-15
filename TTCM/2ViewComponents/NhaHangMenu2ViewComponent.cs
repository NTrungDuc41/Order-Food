using TTCM._2Repository;
using TTCM.Models;
using Microsoft.AspNetCore.Mvc;

namespace TTCM._2ViewComponents
{
    public class NhaHangMenu2ViewComponent:ViewComponent
    {
        private readonly INhaHang2Repository _nhaHang;
        public NhaHangMenu2ViewComponent(INhaHang2Repository nhaHang2Repository)
        {
            _nhaHang = nhaHang2Repository;
        }
        public IViewComponentResult Invoke()
        {
            var loaiSp = _nhaHang.GetAllNhaHang().OrderBy(X => X.MaNh);
            return View(loaiSp);
        }

    }
}
