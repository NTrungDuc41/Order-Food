using TTCM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TTCM.Controllers
{
    public class AccessController : Controller
    {
        private readonly QldoAn4Context _context;

        public AccessController(QldoAn4Context context)
        {
            _context = context;
        }


        QldoAn4Context db = new QldoAn4Context();
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                
                return View();
            }
            else {
                
                return RedirectToAction("Index","Home");
            }
        }

        [HttpPost]
        public IActionResult Login(TAccount user)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                var u = db.TAccounts.Where(X => X.Username.Equals(user.Username)&& X.Password.Equals(user.Password)).FirstOrDefault();
                if (u != null)
                {
                    HttpContext.Session.SetString("UserName", u.Username.ToString());
                    if (u.Role.ToString() == "1")
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAccount(TAccount model)
        {
            string strUserName = HttpContext.Request.Form["Username"].ToString();
            var checkUserName = db.TAccounts.FirstOrDefault(x => x.Username == strUserName);
            int id = db.TAccounts.ToList().Count() + 10;
            if (ModelState.IsValid)
            {
                if (checkUserName == null)
                {
                    model.MaKhachHang = id;
                    model.Role = null;
                    _context.TAccounts.Add(model);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult EditAccount(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            TAccount account = _context.TAccounts.Find(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditAccount(TAccount account)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(account).State = EntityState.Modified;
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Home");

        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Login", "Access");
        }
    }
}
