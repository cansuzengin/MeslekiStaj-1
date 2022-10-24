using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcProject.Models.Entity;
using System.Web.Security;

namespace MvcProject.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        // GET: Login
        [HttpGet]
        public ActionResult GirisYap(string Uyari)
        {
            ViewBag.intValue = Uyari;
            return View();
        }
        [HttpPost]
        public ActionResult GirisYap(TBLUYELER p)
        {
            var bilgiler = db.TBLUYELER.Where(x => x.KULLANICIADI == p.KULLANICIADI && x.SIFRE == p.SIFRE).FirstOrDefault();
            if(bilgiler!=null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.MAIL, false);
                Session["Mail"] = bilgiler.MAIL.ToString();
                Session["Fotoğraf"] = bilgiler.FOTOGRAF;
                
                return RedirectToAction("Index", "Panel");
            }
            else
            {               
                return RedirectToAction("GirisYap", "Login", new{ Uyari = "1"});
            }
        }
        [HttpGet]
        public ActionResult SifreKontrol()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SifreKontrol(TBLUYELER p)
        {
            var bilgiler = db.TBLUYELER.Where(x => x.MAIL == p.MAIL && x.KURTARMA == p.KURTARMA).FirstOrDefault();
            if(bilgiler!=null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.MAIL, false);
                bilgiler.SIFRE = p.SIFRE;
                db.SaveChanges();
                return RedirectToAction("GirisYap", "Login");
            }
            else
            {

                return View("SifreKontrol");
            }
        }
    }
}