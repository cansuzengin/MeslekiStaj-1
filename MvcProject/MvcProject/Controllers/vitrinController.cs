using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcProject.Models.Entity;
using MvcProject.Models.Classes;

namespace MvcProject.Controllers
{
    [AllowAnonymous]
    public class vitrinController : Controller
    {
        // GET: vitrin
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        [HttpGet]
        public ActionResult Index()
        {
            Class1 cs = new Class1();
            cs.Deger1 = db.TBLKITAP.ToList();
            cs.Deger2 = db.TBLHAKKIMIZDA.ToList();
            return View(cs);
        }
        [HttpPost]
        public ActionResult Index(TBLILETISIM t)
        {
            db.TBLILETISIM.Add(t);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}