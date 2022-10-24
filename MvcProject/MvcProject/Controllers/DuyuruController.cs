using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcProject.Models.Entity;
namespace MvcProject.Controllers
{
    public class DuyuruController : Controller
    {
        // GET: Duyuru
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        public ActionResult Index()
        {
            var degerler = db.TBLDUYURU.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult DuyuruEkle()
        {
            List<SelectListItem> deger1 = (from i in db.TBLETIKET.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.ETIKET,
                                               Value = i.ETIKET
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            return View();
        }
        [HttpPost]
        public ActionResult DuyuruEkle(TBLDUYURU d)
        {
            db.TBLDUYURU.Add(d);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DuyuruSil(int id)
        {
            var duyuru = db.TBLDUYURU.Find(id);
            db.TBLDUYURU.Remove(duyuru);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DuyuruGetir(int id)
        {
            var dyr = db.TBLDUYURU.Find(id);
            return View("DuyuruGetir", dyr);
        }
        public ActionResult DuyuruGüncelle(TBLDUYURU p)
        {
            var dyr = db.TBLDUYURU.Find(p.ID);
            dyr.BASLIK = p.BASLIK;
            dyr.DUYURU = p.DUYURU;
            dyr.TARIH = p.TARIH;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DuyuruDetay(int id)
        {
            var dyr = db.TBLDUYURU.Find(id);
            return View("DuyuruDetay", dyr);
        }
    }
}