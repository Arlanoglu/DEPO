using MVCLogin.Models.Context;
using MVCLogin.Models.Entity;
using MVCLogin.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCLogin.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext db = new AppDbContext();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(AppUser user)
        {
            db.Users.Add(user);
            db.SaveChanges();
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginVM loginVM)
        {
            AppUser user = db.Users.Where(x => x.Username == loginVM.Username && x.Password == loginVM.Password).FirstOrDefault();

            if (user != null)
            {
                //Cookie
                FormsAuthentication.SetAuthCookie(user.Username, true);
                return RedirectToAction("Index");
            }
            else
            {
                return View(loginVM);
            }

        }

    }
}