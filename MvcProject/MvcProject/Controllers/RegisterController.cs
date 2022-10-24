using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcProject.Models.Entity;
namespace MvcProject.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        // GET: Register
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        [HttpGet]
        public ActionResult kayit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult kayit(TBLUYELER p)
        {
            if(!ModelState.IsValid)
            {
                return View("kayit");
            }
            db.TBLUYELER.Add(p);
            db.SaveChanges();
            return View();
        }
    }
}