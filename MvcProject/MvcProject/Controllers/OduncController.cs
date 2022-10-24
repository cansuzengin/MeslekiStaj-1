using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcProject.Models.Entity;

namespace MvcProject.Controllers
{
    public class OduncController : Controller
    {
        // GET: Odunc
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        [Authorize(Roles = "A")]
        public ActionResult Index()
        {
            var degerler = db.TBLHAREKET.Where(x => x.ISLEMDURUM == false).ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult OduncVer()
        {
            List<SelectListItem> deger1 = (from i in db.TBLUYELER.ToList()        //Kategori ve yazar seçimi yapabilmek için tablodaki değerler alınır.
                                           select new SelectListItem
                                           {
                                               Text = i.AD + " " + i.SOYAD,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            List<SelectListItem> deger2 = (from i in db.TBLKITAP.Where(x => x.DURUM == true).ToList()        //Kategori ve yazar seçimi yapabilmek için tablodaki değerler alınır.
                                           select new SelectListItem
                                           {
                                               Text = i.AD,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr2 = deger2;
            List<SelectListItem> deger3 = (from i in db.TBLPERSONEL.ToList()        //Kategori ve yazar seçimi yapabilmek için tablodaki değerler alınır.
                                           select new SelectListItem
                                           {
                                               Text = i.PERSONEL,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr3 = deger3;
            return View();
        }
        [HttpPost]
        public ActionResult OduncVer(TBLHAREKET p)
        {
            var value1 = db.TBLUYELER.Where(x => x.ID == p.TBLUYELER.ID).FirstOrDefault();
            var value2 = db.TBLKITAP.Where(x => x.ID == p.TBLKITAP.ID).FirstOrDefault();
            var value3 = db.TBLPERSONEL.Where(x => x.ID == p.TBLPERSONEL.ID).FirstOrDefault();
            p.TBLUYELER = value1;
            p.TBLKITAP = value2;
            p.TBLPERSONEL = value3;
            db.TBLHAREKET.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Odunciade(TBLHAREKET p)
        {
            var odn = db.TBLHAREKET.Find(p.ID);
            DateTime d1 = DateTime.Parse(odn.IADETARIH.ToString());
            DateTime d2 = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            TimeSpan d3 = d2 - d1;
            if(d3.TotalDays<0)
            {
                ViewBag.dgr = 0;
            }
            else
            {
                ViewBag.dgr = d3.TotalDays;
            }
            return View("Odunciade", odn);
        }
        public ActionResult OduncGuncelle(TBLHAREKET p)
        {
            var hrk = db.TBLHAREKET.Find(p.ID);
            hrk.UYEGETIRTARIH = p.UYEGETIRTARIH;
            hrk.ISLEMDURUM = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}