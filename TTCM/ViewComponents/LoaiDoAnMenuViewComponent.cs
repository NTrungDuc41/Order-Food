using TTCM.Models;
using TTCM.Repository;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace TTCM.ViewComponents
{
    public class LoaiDoAnMenuViewComponent:ViewComponent
    {
        private readonly ILoaiDoAnRepository _loaiDa;
        public LoaiDoAnMenuViewComponent(ILoaiDoAnRepository loaiDoAnRepository)
        {
            _loaiDa = loaiDoAnRepository;
        }
        public IViewComponentResult Invoke()
        {
            var loaiDa = _loaiDa.GetAllDanhMucDA().OrderBy(X => X.MaDanhMuc);
            return View(loaiDa);
        }

    }
}
