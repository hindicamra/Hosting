using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using DataAccessLayer.Klase;

namespace Hosting.Controllers
{
    public class vpsController : Controller
    {
        // GET: vps
        public ActionResult Index()
        {
            mssql m = new mssql();

            List<VpsPaketi> vps = m.VpsPaketi.Where(x=>x.Deaktiviran==false).ToList();

            ViewData["vps"] = vps;

            return View();
        }
        public ActionResult VpsPaketiDodaj()
        {
            mssql s = new mssql();
            ViewData["s"] = s.Serveri.ToList();
            return View("VpsPaketiDodaj");
        }
        public ActionResult VpsPaketiSnimi(string Paket, string HDD, string Promet, string CPU, string RAM, string SWAP, string OS, string IPV4, string IPV6, string Konfiguracija, string Cijena, int Server)
        {
            mssql s = new mssql();
            VpsPaketi v = new VpsPaketi();
            v.NazivPaketa = Paket;
            v.HDD = HDD;
            v.Promet = Promet;
            v.CPU = CPU;
            v.RAM = RAM;
            v.SWAP = SWAP;
            v.OS = OS;
            v.IPv4 = IPV4;
            v.IPv6 = IPV6;
            v.Konfiguracija = Konfiguracija;
            v.Cijena = Cijena;
            v.ServeriId = Server;

            s.VpsPaketi.Add(v);
            s.SaveChanges();

            return RedirectToAction("index", "Admin");
        }
        public ActionResult pregled()
        {
            mssql m = new mssql();
            List<VpsPaketi> v = m.VpsPaketi.Where(x=>x.Deaktiviran==false).ToList();
            ViewData["v"] = v;
            return View("pregled");
        }
        public ActionResult deaktiviraj(int id)
        {
            mssql m = new mssql();
            VpsPaketi v = m.VpsPaketi.SingleOrDefault(x=>x.Id==id);
            v.Deaktiviran = true;
            m.SaveChanges();
            return RedirectToAction("pregled");
        }
    }
}