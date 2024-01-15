using TTCM.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Http;
using TTCM.Utility;
using Microsoft.CodeAnalysis.Elfie.Model.Tree;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Newtonsoft.Json;
using System.Globalization;
using TTCM.Models.Authentication;
using Microsoft.CodeAnalysis.Elfie.Extensions;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TTCM.Controllers
{

    [Authentication]
    public class CartController : Controller
    {

        QldoAn4Context db = new QldoAn4Context();
        public const string CARTKEY = "GioHang";
        private readonly QldoAn4Context _context;
        public CartController (QldoAn4Context context ){
            _context    = context;
        }
        List<CartItem> GetCartItems()
        {

            var session = HttpContext.Session;
            string jsoncart = session.GetString(CARTKEY);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartItem>> (jsoncart);
            }
            return new List<CartItem> ();
        }
        public List<CartItem> Carts
        {
            get
            {
                var data = HttpContext.Session.Get<List<CartItem>>("GioHang");
                if (data == null)
                {
                    data = new List<CartItem>();
                }
                return data;
            }
        }
        void SaveCartSession(List<CartItem> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString(CARTKEY, jsoncart);
        }
        public IActionResult RemoveCart(string MaDa)
        {
            List<CartItem> lstgiohang = HttpContext.Session.Get<List<CartItem>>("GioHang");
            CartItem cartitem = lstgiohang.SingleOrDefault(n => n.MaDa == MaDa);
            if (cartitem != null)
            {
                lstgiohang.RemoveAll(n => n.MaDa == MaDa);
                SaveCartSession(lstgiohang);
                
            }
            if (lstgiohang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index");
        }
        public IActionResult AddToCart(string maDa,int SoLuong)
        {
            var myCart = Carts;
            var item = myCart.SingleOrDefault(x => x.MaDa == maDa);
            if (item == null)//chưa có
            {
                var hangHoa = _context.TDoAns.SingleOrDefault(p => p.MaDa == maDa);
                item = new CartItem
                {
                    MaDa = maDa,
                    TenDa = hangHoa.TenDa,
                    DonGia = hangHoa.GiaNhoNhat.Value,
                    SoLuong = SoLuong,
                    Anh = hangHoa.Anh
                };
                myCart.Add(item);
            }
            else
            {
                if (SoLuong ==null)
                {
                    item.SoLuong = 1;
                }
                else
                {
                    item.SoLuong += SoLuong;
                }
            }
            HttpContext.Session.Set("GioHang",myCart);
            return RedirectToAction("Index");
        }

        public IActionResult CapNhapSoLuong(string MaDa, IFormCollection f)
        {
            List<CartItem> listGioHang = HttpContext.Session.Get<List<CartItem>>("GioHang");
            CartItem sp = listGioHang.SingleOrDefault(n => n.MaDa == MaDa);
            if (sp != null)
            {
                sp.SoLuong = int.Parse(f["Soluong"].ToString());
                if (sp.SoLuong <= 0)
                {
                    listGioHang.RemoveAll(n => n.MaDa == MaDa);
                    SaveCartSession(listGioHang);
                }
                SaveCartSession(listGioHang);
            }
            return RedirectToAction("Index");
        }
        [Authentication]
        public IActionResult Index()
        {
            double tongTien = TongTien();
            ViewBag.TongTien = tongTien;
            ViewBag.TongSoLuong = TongSoLuong();
            return View(Carts);
        }
        public double TongTien()
        {
            double t = 0;
            List<CartItem> listGioHang = HttpContext.Session.Get<List<CartItem>>("GioHang");
            t = (double)listGioHang.Sum(n => n.ThanhTien);
            return t;
        }
        public int TongSoLuong()
        {
            int t = 0;
            List<CartItem> listGioHang = HttpContext.Session.Get<List<CartItem>>("GioHang");
            t = listGioHang.Count;
            return t;
        }
        public void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove(CARTKEY);
        }
        public IActionResult ThanhToanThanhCong()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ThanhToan()
        {
            if (HttpContext.Session.GetString("GioHang") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<CartItem> gioHangs = GetCartItems();

            ViewBag.TongTien = TongTien();
            ViewBag.SoLuong = TongSoLuong();
            return View(gioHangs);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThanhToan(IFormCollection f)
        {
            List<CartItem> gioHangs = GetCartItems();

           
            //lưu thông tin vào hóa đơn bán
            THoaDonBan hd = new THoaDonBan();
            string name = f["Name"].ToString();
            string phone = f["Phone"].ToString();
            string address = f["Address"].ToString();
            string email = f["Email"].ToString();
            string dvvc = f["DVVC"].ToString();
            string ghichu = f["GhiChu"].ToString();
            string pttt = f["PTTT"].ToString();
            DateTime ngayhdb = DateTime.ParseExact(f["Ngay"], "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime gio = DateTime.ParseExact(f["Gio"], "HH:mm", CultureInfo.InvariantCulture);

            int check = db.TAccounts.Where(n => n.TenKhachHang == name && n.Email == email).Count();
            TAccount khachHang = new TAccount();
            if (check == 0)
            {
                int Sl = db.TAccounts.ToList().Count();
                int MaKH = (Sl + 2);
                khachHang.MaKhachHang = MaKH;
                khachHang.TenKhachHang = name;
                khachHang.Email = email;
                khachHang.SoDienThoai = phone;
                

                db.TAccounts.Add(khachHang);
            }
            else
            {
                khachHang = db.TAccounts.SingleOrDefault(n => n.TenKhachHang == name && n.Email == email);
            }
            //them vanchuyen
            TVanChuyen vanchuyen = new TVanChuyen();
            int check2 = db.TVanChuyens.Where(n => n.TenDonViVanChuyen == dvvc).Count();
            if (check2 != 0)
            {
                vanchuyen = db.TVanChuyens.SingleOrDefault(n => n.TenDonViVanChuyen == dvvc);
            }
            hd.MaKhachHang = khachHang.MaKhachHang;
            hd.DiaChiGh = address;
            hd.NgayHdb = DateTime.Now;
            hd.ThoiGianGh = ngayhdb.ToString();
            hd.GioGh = gio;
            hd.MaDvvc = vanchuyen.MaDvvc;
            hd.GhiChu = ghichu;
            hd.PhuongThucThanhToan = pttt;
            int sl1 = (db.THoaDonBans.ToList().Count() + 2);

            if (address == vanchuyen.DiaChi)
            {
                hd.PhiVanChuyen = 0;
            }
            else {
                hd.PhiVanChuyen = vanchuyen.GhiChu;
            }
            hd.TongTienHdb = 0;
            hd.MaHdb = "HDB_" + sl1.ToString();
            foreach (var item in gioHangs)
            {
                TChiTietHdb cthd = new TChiTietHdb();
                cthd.MaHdb = hd.MaHdb;
                cthd.MaDa = item.MaDa;
                cthd.SoLuongBan = item.SoLuong;
                cthd.ThanhTien = (decimal?)item.ThanhTien;
                hd.TongTienHdb += cthd.ThanhTien;
                db.TChiTietHdbs.Add(cthd);
            }
            hd.TongTienHdb = hd.TongTienHdb + vanchuyen.GhiChu;
            hd.Status = 1;
            db.THoaDonBans.Add(hd);
            db.SaveChanges();
            HttpContext.Session.Remove("GioHang");
            db.SaveChanges();
            return RedirectToAction("ThanhToanThanhCong", "Cart");

        }

    }
}
