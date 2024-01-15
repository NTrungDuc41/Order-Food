using Microsoft.AspNetCore.Mvc;
using TTCM.Models;
using TTCM.Models.Authentication;

namespace TTCM.Controllers
{
    [Authentication]
    public class DanhGiaController : Controller
    {
        QldoAn4Context db = new QldoAn4Context();
        [HttpGet]
        public IActionResult DanhGiaSanPham(string MaDa)
        {
            MaDa = MaDa.Split(')')[0];
            var doAn = db.TDoAns.SingleOrDefault(n => n.MaDa == MaDa);
            doAn.Vote++;
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult DanhGiaSanPham2(string MaDa)
        {
            MaDa = MaDa.Split(')')[0];
            var doAn = db.TDoAns.SingleOrDefault(n => n.MaDa == MaDa);
            doAn.Vote--;
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
