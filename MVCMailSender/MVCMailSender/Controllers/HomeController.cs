using MVCMailSender.Models.Context;
using MVCMailSender.Models.Entity;
using MVCMailSender.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace MVCMailSender.Controllers
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
            Guid uniqueKey = Guid.NewGuid();
            user.ActivationKey = uniqueKey;
            //kullanıcı veritabanına kayıt olacak.
            db.Users.Add(user);
            db.SaveChanges();
            //kullanıcıya mail gönderileecek.
            string message = $"üyelik işleminizi onaylamak için lütfen linki tıklayın https://localhost:44348/Home/Activation/" + uniqueKey;
            MailSender.SendEmail(user.Email, "Üyelik Aktivayon", message);
            //kullanıcı bekleme sayfasına yönlendirilecek.

            return RedirectToAction("ActivationPending", user);
        }

        //Activation Pending
        public ActionResult ActivationPending(AppUser user)
        {
            return View(user);
        }

        //Activation
        public ActionResult Activation(Guid id)
        {
            bool result = db.Users.Any(x => x.ActivationKey == id);
            if (result)
            {
                AppUser user = db.Users.Where(x => x.ActivationKey == id).FirstOrDefault();
                user.IsActive = true;
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Complete",user);
            }
            else
            {
                return RedirectToAction("Index");
            }
            
        }

        public ActionResult Complete(AppUser user)
        {
            return View(user);
        }

    }
}