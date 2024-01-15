using TTCM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using X.PagedList;
using TTCM.Models.Authentication;

namespace TTCM.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]
    [Authentication]
    public class HomeAdminController : Controller
    {
        QldoAn4Context db = new QldoAn4Context();
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("danhmucdoan")]
        public IActionResult DanhMucDoAn(string searchby,string search)
        {
            var lstSanPham = db.TDoAns.AsNoTracking().OrderBy(x => x.MaDa);
            var lst = db.TDoAns.AsNoTracking().OrderBy(x => x.MaDa);
            if (searchby == "tendoan")
            {
                return View(lstSanPham.Where(X => X.TenDa.StartsWith(search)).ToList());
            }
            else if (searchby == "giatien")
            {
                decimal searchDecimal;
                bool parseSuccess = decimal.TryParse(search, out searchDecimal);

                if (parseSuccess)
                {
                    return View(lstSanPham.Where(X => X.GiaNhoNhat == searchDecimal).ToList());
                }
                else
                {
                    return View(lst);
                }
            }
            else
            {
                return View(lst);
            }
        }
        [Route("ThemMonAnMoi")]
        [HttpGet]
        public IActionResult ThemMonAnMoi()
        {
            ViewBag.MaDanhMuc = new SelectList(db.TDanhMucs.ToList(), "MaDanhMuc", "TenDanhMuc");
            ViewBag.MaNh = new SelectList(db.TNhaHangs.ToList(), "MaNh", "TenNhaHang");
            return View();
        }

        [Route("ThemMonAnMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemMonAnMoi(TDoAn doAn)
        {
            if (ModelState.IsValid)
            {
                db.TDoAns.Add(doAn);
                db.SaveChanges();
                return RedirectToAction("DanhMucDoAn");
            }
            return View(doAn);
        }
        [Route("SuaMonAn")]
        [HttpGet]
        public IActionResult SuaMonAn(string maDa)
        {
            ViewBag.MaDanhMuc = new SelectList(db.TDanhMucs.ToList(), "MaDanhMuc", "TenDanhMuc");
            ViewBag.MaNh = new SelectList(db.TNhaHangs.ToList(), "MaNh", "TenNhaHang");
            var doAn = db.TDoAns.Find(maDa);
            return View(doAn);
        }

        [Route("SuaMonAn")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaMonAn(TDoAn doAn)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doAn).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhMucDoAn", "HomeAdmin");

            }
            return View(doAn);
        }
        [Route("XoaMonAn")]
        [HttpGet]
        public IActionResult XoaMonAn(string maDa)
        {
            TempData["Message"] = "";
            var chitietHDB = db.TChiTietHdbs.Where(x => x.MaDa == maDa).ToList();
            if (chitietHDB.Count() > 0)
            {
                TempData["Message"] = "Không xóa được món ăn này !";
                return RedirectToAction("DanhMucDoAn", "HomeAdmin");
            }
            var anhSanPhams = db.TAnhs.Where(x => x.MaDa == maDa).ToList();
            var gioHangs = db.TGioHangs.Where(x => x.MaDa == maDa).ToList();
            var hoaDonNhaps = db.THoaDonNhaps.Where(x => x.MaDa == maDa).ToList();
            if (anhSanPhams.Any()) db.RemoveRange(anhSanPhams);
            if (gioHangs.Any()) db.RemoveRange(gioHangs);
            if (hoaDonNhaps.Any()) db.RemoveRange(hoaDonNhaps);
            db.Remove(db.TDoAns.Find(maDa));
            db.SaveChanges();
            TempData["Message"] = "Món Ăn đã được xóa !";
            return RedirectToAction("DanhMucDoAn", "HomeAdmin");
        }


        //Account
        [Route("danhmuctaikhoan")]
        public IActionResult DanhMucTaiKhoan(int? page)
        {
            int pageSize = 12;
            //có 8 trang
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var lstSanPham = db.TAccounts.AsNoTracking().OrderBy(x => x.MaKhachHang);
            ///phân trang theo  tên sản phẩm
            PagedList<TAccount> lst = new PagedList<TAccount>(lstSanPham, pageNumber, pageSize);
            return View(lst);
        }

        [Route("ThemTaiKhoanMoi")]
        [HttpGet]
        public IActionResult ThemTaiKhoanMoi()
        {
            return View();
        }
        [Route("ThemTaiKhoanMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemTaiKhoanMoi(TAccount taikhoan)
        {
            if (ModelState.IsValid)
            {
                db.TAccounts.Add(taikhoan);
                db.SaveChanges();
                return RedirectToAction("DanhMucTaiKhoan","HomeAdmin");
            }
            return View(taikhoan);
        }

        [Route("SuaTaiKhoan")]
        [HttpGet]
        public IActionResult SuaTaiKhoan(int maKH)
        {
            var taikhoan = db.TAccounts.Find(maKH);
            return View(taikhoan);
        }
        [Route("SuaTaiKhoan")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaTaiKhoan(TAccount tk)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tk).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhMucTaiKhoan", "HomeAdmin"); ;
            }
            return View(tk);
        }

        [Route("XoaTaiKhoan")]
        [HttpGet]
        public IActionResult XoaTaiKhoan(int maKH)
        {
            TempData["Message"] = "";
            var hoaDonBans = db.THoaDonBans.Where(x => x.MaKhachHang == maKH).ToList();
            if (hoaDonBans.Count() > 0)
            {
                TempData["Message"] = "Không xóa được tài khoản này !";
                return RedirectToAction("DanhMucTaiKhoan", "HomeAdmin");
            }
            var gioHangs = db.TGioHangs.Where(x => x.MaKhachHang == maKH).ToList();
            if (gioHangs.Count() > 0)
            {
                TempData["Message"] = "Không xóa được tài khoản này !";
                return RedirectToAction("DanhMucTaiKhoan", "HomeAdmin");
            }
            db.Remove(db.TAccounts.Find(maKH));
            db.SaveChanges();
            TempData["Message"] = "Tài khoản đã được xóa !";
            return RedirectToAction("DanhMucTaiKhoan", "HomeAdmin");
        }

        //DonHang
        [Route("danhmucdonhang")]
        public IActionResult DanhMucDonHang(string searchby, string search)
        {
            
            var lstdonHang = db.THoaDonBans.AsNoTracking().OrderBy(x => x.MaHdb);
            var lst = db.THoaDonBans.AsNoTracking().OrderBy(x => x.MaHdb);
            if (searchby == "trangthai")
            {
                return View(lstdonHang.Where(X => X.Status.ToString()==search).ToList());
            }
            else if (searchby == "tongtien")
            {
                decimal searchDecimal;
                bool parseSuccess = decimal.TryParse(search, out searchDecimal);

                if (parseSuccess)
                {
                    return View(lstdonHang.Where(X => X.TongTienHdb == searchDecimal).ToList());
                }
                else
                {
                    return View(lst);
                }
            }
            else
            {
                return View(lst);
            }

        }
        [Route("SuaDonHang")]
        [HttpGet]
        public IActionResult SuaDonHang(string mahdb)
        {
           
            var taikhoan = db.THoaDonBans.Find(mahdb);
            return View(taikhoan);
        }
        [Route("SuaDonHang")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaDonHang(THoaDonBan tk)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tk).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhMucDonHang", "HomeAdmin"); ;
            }
            return View(tk);
        }

        [Route("XoaDonHang")]
        [HttpGet]
        public IActionResult XoaDonHang(string mahdb)
        {
            TempData["Message"] = "";
            TChiTietHdb cthd = new TChiTietHdb();

            var check = db.THoaDonBans.Where(x => x.Status == 0).Count();
            if (check!=0)
            {
                db.Remove(db.THoaDonBans.Find(mahdb));
                db.Remove(db.TChiTietHdbs.Find(mahdb));
            }
                db.SaveChanges();
            TempData["Message"] = "Tài khoản đã được xóa !";
            return RedirectToAction("DanhMucTaiKhoan", "HomeAdmin");
        }
    }
}
