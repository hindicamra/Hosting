using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using DataAccessLayer.Klase;

namespace Hosting.Controllers
{
    public class dedicatedController : Controller
    {
        // GET: dedicated
        public ActionResult Index()
        {
            mssql m = new mssql();
            List<DedicatedPaketi> dp = m.DedicatedPaketi.Where(x=>x.Deaktivirano==false).ToList();
            ViewData["dp"] = dp;
            return View();
        }
        public ActionResult DedicatedPaketiDodaj()
        {
            mssql s = new mssql();
            ViewData["s"] = s.Serveri.ToList();
            return View("DedicatedPaketiDodaj");
        }
        public ActionResult DedicatedPaketiSnimi(string Paket, string CPU, string RAM, string HDD, string SHDD, string OS, string Promet, string IPV4, string IPV6, string Nadogradnja, double Cijena, int Server)
        {
            mssql s = new mssql();
            DedicatedPaketi d = new DedicatedPaketi();
            d.Naziv = Paket;
            d.CPU = CPU;
            d.Ram = RAM;
            d.HDDPrimar = HDD;
            d.HDDSekundar = SHDD;
            d.OS = OS;
            d.Promet = Promet;
            d.IPv4 = IPV4;
            d.IPv6 = IPV6;
            if (Nadogradnja == "on")
                d.Nadogradnja = true;
            else
                d.Nadogradnja = false;
            d.ServeriId = Server;
            d.Cijena = Cijena;

            s.DedicatedPaketi.Add(d);
            s.SaveChanges();

            return RedirectToAction("index", "Admin");
        }
        public ActionResult pregled()
        {
            mssql s = new mssql();
            ViewData["d"] = s.DedicatedPaketi.Where(x=>x.Deaktivirano==false).ToList();
            return View("pregled");
        }
        public ActionResult deaktivacija(int id)
        {
            mssql s = new mssql();
            DedicatedPaketi d = s.DedicatedPaketi.SingleOrDefault(x => x.Id == id);
            d.Deaktivirano = true;
            s.SaveChanges();
            return RedirectToAction("pregled");
        }
    }
}