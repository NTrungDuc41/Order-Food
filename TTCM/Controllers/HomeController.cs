using TTCM.Models;
using TTCM.Models.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using X.PagedList;
using TTCM.Utility;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;

namespace TTCM.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        QldoAn4Context db = new QldoAn4Context();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int? page)
        {
            int pageSize = 9;
            //có 8 trang
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var lstSanPham = db.TDoAns.AsNoTracking().OrderBy(x => x.TenDa);
            ///phân trang theo  tên sản phẩm
            PagedList<TDoAn> lst = new PagedList<TDoAn>(lstSanPham, pageNumber, pageSize);
            return View(lst);
        }

        public IActionResult DoAnTheoDanhMuc(String madanhmuc, int? page)
        {
            int pageSize = 9;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.TDoAns.AsNoTracking().Where(x => x.MaDanhMuc == madanhmuc).OrderBy(x => x.TenDa);
            PagedList<TDoAn> lst = new PagedList<TDoAn>(lstsanpham, pageNumber, pageSize);
            ViewBag.MaDanhMuc = madanhmuc;
            return View(lst);

        }
        public IActionResult ChiTietSanPham(string maDa)
        {
            var sanPham = db.TDoAns.SingleOrDefault(x => x.MaDa == maDa);
            var anhSanPham = db.TAnhs.Where(x => x.MaDa == maDa).ToList();
            ViewBag.anhSanPham = anhSanPham;

            var listNH = (from item in db.TDoAns
                          join nh in db.TNhaHangs
                          on item.MaNh equals nh.MaNh
                          where item.MaDa == maDa
                          select new NhaHangViewModel { MaNh = nh.MaNh, TenNhaHang = nh.TenNhaHang, DiaChi = nh.DiaChi }).Distinct().ToList();
            var viewModel = new SanPhamViewModel { SanPham = sanPham, ListNH = listNH };
            return View(viewModel);
        }
        
        public IActionResult DoAnTheoNhaHang(String maNh, int? page)
        {
            int pageSize = 9;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.TDoAns.AsNoTracking().Where(x => x.MaNh == maNh).OrderBy(x => x.TenDa);
            PagedList<TDoAn> lst = new PagedList<TDoAn>(lstsanpham, pageNumber, pageSize);
            ViewBag.MaNh = maNh;
            return View(lst);
        }
        public IActionResult Articles()
        {
            return View();
        }
        public IActionResult ArticlesDetails()
        {
            return View();
        }
        public IActionResult ChefGuide()
        {
            return View();
        }
        public IActionResult Question()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}