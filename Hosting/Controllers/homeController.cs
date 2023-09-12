using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using DataAccessLayer.Klase;

namespace Hosting.Controllers
{
    public class homeController : Controller
    {
        public mssql m = new mssql();
        // GET: home
        public ActionResult Index()
        {
            List<HostingPaketi> hp = m.HostingPaketi.Where(x=>x.Aktivan==true).ToList();

            ViewData["hp"] = hp;

            return View();
        }

        public ActionResult osobnipodaci()
        {
            return View("osobnipodaci");
        }
        public ActionResult uvjeti()
        {
            return View("uvjeti");
        }
    }
}