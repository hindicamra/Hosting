using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using DataAccessLayer.Klase;

namespace Hosting.Controllers
{
    public class webhostController : Controller
    {
        // GET: webhost
        public ActionResult Index()
        {
            mssql m = new mssql();
            List<HostingPaketi> hpaketi = m.HostingPaketi.Where(x=>x.Aktivan==true).ToList();
            ViewData["hp"] = hpaketi;

            return View();
        }
        public ActionResult HostingPaketiDodaj()
        {
            mssql s = new mssql();
            ViewData["s"] = s.Serveri.ToList();
            return View("HostingPaketiDodaj");
        }
        public ActionResult HostingPaketiSnimi(string Paket, string Prostor, string Promet, string Baza, string EmailAdresa, string Cijena, string IPV4, string IPV6, int AddonDomena, int ParkedDomena, string FTPNalog, string PodDomena, int Server)
        {
            mssql s = new mssql();
            HostingPaketi h = new HostingPaketi();
            h.NazivPaketa = Paket;
            h.Prostor = Prostor;
            h.Promet = Promet;
            h.Baza = Baza;
            h.EmailAdresa = EmailAdresa;
            h.Cijena = Cijena;
            h.IPv4 = IPV4;
            h.IPv6 = IPV6;
            h.AddonDomene = AddonDomena;
            h.ParkedDomene = ParkedDomena;
            h.FTPNaloga = FTPNalog;
            h.PodDomena = PodDomena;
            h.ServeriId = Server;
            h.Aktivan = true;

            s.HostingPaketi.Add(h);
            s.SaveChanges();

            return RedirectToAction("index", "Admin");
        }
        public ActionResult pregled()
        {
            mssql m = new mssql();
            List<HostingPaketi> h = m.HostingPaketi.Where(x=>x.Aktivan==true).ToList();
            ViewData["h"] = h;
            return View();
        }
        public ActionResult edit(int id)
        {
            mssql s = new mssql();
            ViewData["h"] = s.HostingPaketi.SingleOrDefault(x => x.Id == id);
            ViewData["s"] = s.Serveri.ToList();
            return View("edit");
        }
        public ActionResult HostingPaketiSnimiEdit(int id, string Paket, string Prostor, string Promet, string Baza, string EmailAdresa, string Cijena, string IPV4, string IPV6, int AddonDomena, int ParkedDomena, string FTPNalog, string PodDomena, int Server)
        {
            mssql s = new mssql();
            HostingPaketi h = s.HostingPaketi.SingleOrDefault(x => x.Id == id);
            h.NazivPaketa = Paket;
            h.Prostor = Prostor;
            h.Promet = Promet;
            h.Baza = Baza;
            h.EmailAdresa = EmailAdresa;
            h.Cijena = Cijena;
            h.IPv4 = IPV4;
            h.IPv6 = IPV6;
            h.AddonDomene = AddonDomena;
            h.ParkedDomene = ParkedDomena;
            h.FTPNaloga = FTPNalog;
            h.PodDomena = PodDomena;
            h.ServeriId = Server;
            s.SaveChanges();
            return RedirectToAction("index", "Admin");
        }
        public ActionResult deaktiviraj(int id)
        {
            mssql s = new mssql();
            HostingPaketi h = s.HostingPaketi.SingleOrDefault(x => x.Id == id);
            h.Aktivan = false;
            s.SaveChanges();
            return RedirectToAction("index", "Admin");
        }
    }
}