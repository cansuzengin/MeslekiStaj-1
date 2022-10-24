using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcProject.Models.Entity;
namespace MvcProject.Controllers
{
    public class MessageController : Controller
    {
        // GET: Message
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        public ActionResult Index()
        {
            var user = (string)Session["Mail"];
            var degerler = db.TBLMESAJLAR.Where(x => x.ALICI == user.ToString() && x.DURUM == true).ToList();
            return View(degerler);
        }

        public ActionResult Send()
        {
            var user = (string)Session["Mail"];
            var degerler = db.TBLMESAJLAR.Where(x => x.GONDEREN == user.ToString() && x.DURUM == true).ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewMessage(TBLMESAJLAR t)
        {
            var user = (string)Session["Mail"];
            t.DURUM = true;
            t.GONDEREN = user.ToString();
            t.TARIH = DateTime.Parse(DateTime.Now.ToShortDateString());
            db.TBLMESAJLAR.Add(t);
            db.SaveChanges();
            return RedirectToAction("Send", "Message");
        }
        public ActionResult DeleteMessage(int id)
        {
            var mesaj = db.TBLMESAJLAR.Find(id);
            if (mesaj.DURUM == true)
            {
                mesaj.DURUM = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                db.TBLMESAJLAR.Remove(mesaj);
                db.SaveChanges();
                return RedirectToAction("Trash");
            }

        }
        public ActionResult Trash()
        {
            var user = (string)Session["Mail"];
            var degerler = db.TBLMESAJLAR.Where(x => x.ALICI == user.ToString() && x.DURUM == false).ToList();
            return View(degerler);
        }
        public PartialViewResult Partial1()
        {
            var user = (string)Session["Mail"];
            var gelenMesajlar = db.TBLMESAJLAR.Where(x => x.ALICI == user && x.DURUM==true).Count();
            ViewBag.dgr1 = gelenMesajlar;
            var gidenMesajlar = db.TBLMESAJLAR.Where(x => x.GONDEREN == user && x.DURUM==true).Count();
            ViewBag.dgr2 = gidenMesajlar;
            var trash = db.TBLMESAJLAR.Where(x => x.ALICI == user && x.DURUM==false).Count();
            ViewBag.dgr3 = trash;

            return PartialView();
        }
    }
}