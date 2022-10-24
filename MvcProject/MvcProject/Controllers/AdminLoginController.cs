using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcProject.Models.Entity;

namespace MvcProject.Controllers
{
    [AllowAnonymous]
    public class AdminLoginController : Controller
    {
        // GET: AdminLogin
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        public ActionResult Login(string Admin)
        {
            ViewBag.deger = Admin;
            return View();
        }
        [HttpPost]
        public ActionResult Login(TBLYONETICI y)
        {
            var bilgiler = db.TBLYONETICI.FirstOrDefault(x => x.MAIL == y.MAIL && x.SIFRE == y.SIFRE);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.MAIL, false);
                Session["Email"] = bilgiler.MAIL.ToString();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login", "AdminLogin", new { Admin = "1" });
            }
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "AdminLogin");
        }
        public ActionResult SifreKontrol()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Kontrol(TBLUYELER p)
        {
            var bilgiler = db.TBLYONETICI.Where(x => x.MAIL == p.MAIL && x.KURTARMA == p.KURTARMA).FirstOrDefault();
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.MAIL, false);
                bilgiler.SIFRE = p.SIFRE;
                db.SaveChanges();
                return RedirectToAction("Login", "AdminLogin");
            }
            else
            {

                return View("Kontrol");
            }
        }
    }
}