using TTCM.Models;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;


namespace TTCM.Controllers
{
    public class TimKiemController : Controller
    {
        QldoAn4Context db = new QldoAn4Context();
        public IActionResult KetQuaTimKiem(IFormCollection form, int? page)
        {
            int pageSize = 100;
            int pageNumber = (page ?? 1);
            string searchKey = form["txtTimKiem"].ToString();
            ViewBag.form = form;
            List<TDoAn> lstKQTK = db.TDoAns.Where(n => n.TenDa.Contains(searchKey)).ToList();
            if (lstKQTK.Count == 0)
            {
                TempData["Message"] = "Không tìm thấy sản phẩm nào!";
                return View(db.TDoAns.ToList().ToPagedList(pageNumber, pageSize));
            }
            else
            {
                TempData["Message"] = "Đã tìm thấy " + lstKQTK.Count() + " sản Phẩm " + searchKey;
                return View(lstKQTK.ToList().ToPagedList(pageNumber, pageSize));
            }
        }
    }
}
