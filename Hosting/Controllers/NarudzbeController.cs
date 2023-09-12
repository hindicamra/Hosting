using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using DataAccessLayer.Klase;

namespace Hosting.Controllers
{
    public class NarudzbeController : Controller
    {
        // GET: Narudzbe
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult domenkupi(int id)
        {
            mssql s = new mssql();
            DomenePaketi d = s.DomenePaketi.Include(x=>x.Domena).Single(x => x.Id == id);
            ViewData["d"] = d;
            return View("domenkupi");
        }
        public ActionResult DomenaSnimi(string Domena, int DomenePaketiId)
        {
            return RedirectToAction("Lice", "Klijent",
                new {Domena = Domena, DomenePaketiId = DomenePaketiId, upozorenje = "hidden"});
        }

        public ActionResult sslkupi(int sslid)
        {
            ViewData["sslid"] = sslid;
            return View("sslkupi");
        }

        public ActionResult vpskupi(int vpsid)
        {
            return RedirectToAction("Lice", "Klijent", new { vpsid =vpsid});
        }
        public ActionResult dedicatedkupi(int did)
        {
            return RedirectToAction("Lice", "Klijent", new { did = did });
        }
        public ActionResult hostingkupi(int hid)
        {
            ViewData["hid"] = hid;
            return View("hostingkupi");
        }
        public ActionResult HostingSnimi(string Domena, int hid)
        {
            return RedirectToAction("Lice", "Klijent",
                new { Domena = Domena, hid = hid });
        }
    }
}