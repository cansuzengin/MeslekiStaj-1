using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcProject.Models.Entity;
namespace MvcProject.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        public ActionResult Index()
        {
            var degerler = db.TBLKATEGORI.Where(x => x.DURUM == true).ToList();   //Kategori tablosundaki değerleri tablo şeklinde alır.
            return View(degerler);
        }
        [HttpGet]
        public ActionResult KategoriEkle()         //Veri girişi yoksa
        {
            return View();
        }
        [HttpPost]
        public ActionResult KategoriEkle(TBLKATEGORI p)    //Veri girişi varsa
        {
            if (!ModelState.IsValid)
            {
                return View("KategoriEkle");
            }
            db.TBLKATEGORI.Add(p);                         //Gelen veriyi tabloya ekler.
            db.SaveChanges();                              //Değişiklikleri kaydeder.
            return View();
        }
        public ActionResult KategoriSil(int id)
        {
            var kategori = db.TBLKATEGORI.Find(id);         //Tablodan gelen id değerine göre veriyi alır.
            //db.TBLKATEGORI.Remove(kategori);                //Veriyi siler.
            kategori.DURUM = false;
            db.SaveChanges();
            return RedirectToAction("Index");               //Index sayfasına yönlendirir.
        }
        public ActionResult KategoriGetir(int id)
        {
            var ktg = db.TBLKATEGORI.Find(id);
            return View("KategoriGetir", ktg);              //Kategori getir sınıfına gider.
        }
        public ActionResult KategoriGuncelle(TBLKATEGORI p)
        {
            var ktg = db.TBLKATEGORI.Find(p.ID);
            ktg.AD = p.AD;                                  //Girilen ad değeri ile tablodaki ad değerini değiştirir.
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PasifKategori()
        {
            var degerler = db.TBLKATEGORI.Where(x => x.DURUM == false).ToList();   //Kategori tablosundaki değerleri tablo şeklinde alır.
            return View(degerler);
        }
        public ActionResult GeriAl(int id)
        {
            var ktg = db.TBLKATEGORI.Find(id);
            ktg.DURUM = true;                             
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}