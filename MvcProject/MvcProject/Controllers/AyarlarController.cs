using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcProject.Models.Entity;
namespace MvcProject.Controllers
{
    public class AyarlarController : Controller
    {
        // GET: Ayarlar
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Index2()
        {
            var user = db.TBLYONETICI.ToList();
            return View(user);
        }
        [HttpGet]
        public ActionResult NewAdmin()
        {
            return RedirectToAction("Index2");
        }
        [HttpPost]
        public ActionResult NewAdmin(TBLYONETICI p)
        {
            db.TBLYONETICI.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index2");
        }
        public ActionResult AdminSil(int id)
        {
            var admin = db.TBLYONETICI.Find(id);
            db.TBLYONETICI.Remove(admin);
            db.SaveChanges();
            return RedirectToAction("Index2");
        }
        [HttpGet]
        public ActionResult AdminGuncelle(int? id)
        {
            var admin = db.TBLYONETICI.Find(id);
            return View("AdminGuncelle",admin);
        }
        [HttpPost]
        public ActionResult AdminGuncelle(TBLYONETICI p)
        {
            var admin = db.TBLYONETICI.Find(p.ID);
            admin.AD = p.AD;
            admin.SOYAD = p.SOYAD;
            admin.MAIL = p.MAIL;
            admin.SIFRE = p.SIFRE;
            admin.YETKI = p.YETKI;
            db.SaveChanges();
            return RedirectToAction("Index2");
        }
    }
}