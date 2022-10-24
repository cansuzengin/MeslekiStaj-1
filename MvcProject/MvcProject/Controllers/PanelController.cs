using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcProject.Models.Entity;
namespace MvcProject.Controllers
{
    [Authorize]
    public class PanelController : Controller
    {
        // GET: Panel
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        [HttpGet]
        public ActionResult Index()
        {
            var userMail = (string)Session["Mail"];
            var degerler = db.TBLUYELER.Where(z => z.MAIL == userMail).FirstOrDefault();
            return View(degerler);
        }
        [HttpPost]
        public ActionResult Index2(TBLUYELER p)
        {
            var user = (string)Session["Mail"];
            var uye = db.TBLUYELER.FirstOrDefault(x => x.MAIL == user);
            uye.SIFRE = p.SIFRE;
            uye.AD = p.AD;
            uye.SOYAD = p.SOYAD;
            uye.KULLANICIADI = p.KULLANICIADI;
            uye.OKUL = p.OKUL;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Account()
        {
            var userMail = (string)Session["Mail"];
            var degerler = db.TBLDUYURU.ToList();

            var deger1 = db.TBLUYELER.Where(x => x.MAIL == userMail).Select(y => y.AD).FirstOrDefault();
            ViewBag.dgr1 = deger1;
            var deger2 = db.TBLUYELER.Where(x => x.MAIL == userMail).Select(y => y.SOYAD).FirstOrDefault();
            ViewBag.dgr2 = deger2;
            var deger3 = db.TBLUYELER.Where(x => x.MAIL == userMail).Select(y => y.FOTOGRAF).FirstOrDefault();
            ViewBag.dgr3 = deger3;
            var deger4 = db.TBLUYELER.Where(x => x.MAIL == userMail).Select(y => y.KULLANICIADI).FirstOrDefault();
            ViewBag.dgr4 = deger4;
            var deger5 = db.TBLUYELER.Where(x => x.MAIL == userMail).Select(y => y.TELEFON).FirstOrDefault();
            ViewBag.dgr5 = deger5;
            var deger6 = db.TBLUYELER.Where(x => x.MAIL == userMail).Select(y => y.OKUL).FirstOrDefault();
            ViewBag.dgr6 = deger6;
            var deger7 = db.TBLUYELER.Where(x => x.MAIL == userMail).Select(y => y.MAIL).FirstOrDefault();
            ViewBag.dgr7 = deger7;

            var UyeId = db.TBLUYELER.Where(x => x.MAIL == userMail).Select(y => y.ID).FirstOrDefault();
            var deger8 = db.TBLHAREKET.Where(x => x.UYE == UyeId).Count();
            ViewBag.dgr8 = deger8;

            var deger9 = db.TBLMESAJLAR.Where(x => x.ALICI == userMail).Count();
            ViewBag.dgr9 = deger9;

            var deger10 = db.TBLDUYURU.Count();
            ViewBag.dgr10 = deger10;

            return View(degerler);
        }
        public ActionResult Books()
        {
            var userMail = (string)Session["Mail"];
            var userId = db.TBLUYELER.Where(x => x.MAIL == userMail.ToString()).Select(z=>z.ID).FirstOrDefault();
            var degerler = db.TBLHAREKET.Where(x => x.UYE == userId && x.ISLEMDURUM==true).ToList();
            return View(degerler);
        }
        public ActionResult Duyurular()
        {
            var duyurular = db.TBLDUYURU.ToList();
            return View(duyurular);
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("GirisYap", "Login");
        }
        public ActionResult NewBooks()
        {
            var userMail = (string)Session["Mail"];
            var userId = db.TBLUYELER.Where(x => x.MAIL == userMail.ToString()).Select(z => z.ID).FirstOrDefault();
            var degerler = db.TBLHAREKET.Where(x => x.UYE == userId && x.ISLEMDURUM == false).ToList();
            return View(degerler);
        }
        public ActionResult Iade(TBLHAREKET p)
        {
            var odn = db.TBLHAREKET.Find(p.ID);
            DateTime d1 = DateTime.Parse(odn.IADETARIH.ToString());
            DateTime d2 = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            TimeSpan d3 = d2 - d1;
            ViewBag.dgr = d3.TotalDays;
            return View("iade", odn);
        }
        public ActionResult Guncelle(TBLHAREKET p)
        {
            var hrk = db.TBLHAREKET.Find(p.ID);
            hrk.UYEGETIRTARIH = p.UYEGETIRTARIH;
            hrk.ISLEMDURUM = true;
            db.SaveChanges();
            return RedirectToAction("Books");
        }
        public PartialViewResult Partial1()
        {
            return PartialView();
        }
        public PartialViewResult Partial2()
        {
            var userMail = (string)Session["Mail"];
            var userId = db.TBLUYELER.Where(x => x.MAIL == userMail.ToString()).Select(z => z.ID).FirstOrDefault();
            var user = db.TBLUYELER.Find(userId);
            return PartialView("Partial2",user);
        }
    }
}