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
    public class serverController : Controller
    {
        // GET: server
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ServeriDodaj()
        {
            mssql s = new mssql();
            ViewData["dip"] = s.DostupneIP.ToList();
            ViewData["r"] = s.Rack.ToList();
            ViewData["srv"] = s.Serveri.Include(x => x.DostupneIP).Include(x => x.Rack).OrderBy(x => x.Rack.Id).ToList();
            return View("ServeriDodaj");
        }
        public ActionResult ServeriSnimi(string Paket, string CPU, string RAM, string HDD, string On, int IP, int Rack)
        {
            mssql s = new mssql();
            Serveri srv = new Serveri();
            srv.Naziv = Paket;
            srv.CPU = CPU;
            srv.RAM = RAM;
            srv.HDD = HDD;
            if (On == "On")
                srv.OnOf = true;
            else
                srv.OnOf = false;
            srv.DostupneIPId = IP;
            srv.RackId = Rack;

            s.Serveri.Add(srv);
            s.SaveChanges();

            return RedirectToAction("index", "Admin");
        }
    }
}