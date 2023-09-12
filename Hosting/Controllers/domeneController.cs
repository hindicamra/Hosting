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
    public class domeneController : Controller
    {
        // GET: domene
        public ActionResult Index()
        {
            mssql m = new mssql();
            List<DomenePaketi> dp = m.DomenePaketi.Include(x=>x.Domena).Where(x => x.Deaktiviran == false).ToList();
            ViewData["dp"] = dp;
            return View();
        }
        public ActionResult DomeneDodaj()
        {
            return View("DomeneDodaj");
        }
        public ActionResult DomeneSnimi(string Domen)
        {
            mssql s = new mssql();
            Domena d = new Domena();
            d.Naziv = Domen;

            s.Domena.Add(d);
            s.SaveChanges();

            return RedirectToAction("index", "Admin");
        }

        public ActionResult DomenePaketiDodaj()
        {
            mssql s = new mssql();
            ViewData["d"] = s.Domena.Where(x=>x.Deaktiviran==false).ToList();
            return View("DomenePaketiDodaj");
        }
        public ActionResult DomenePaketiSnimi(int Domen, string CijenaAktivacije, string CijenaZaPrvuGodinu, string CijenaZaSvakuNarednuGodinu, string CijenaUzWebHosting)
        {
            mssql s = new mssql();
            DomenePaketi dp = new DomenePaketi();
            dp.DomenaId = Domen;
            dp.CijenaAktivacije = CijenaAktivacije;
            dp.CijenaZaPrvuGodinu = CijenaZaPrvuGodinu;
            dp.SvakaNarednaGodina = CijenaZaSvakuNarednuGodinu;
            dp.CijenaUzWebHosting = CijenaUzWebHosting;

            s.DomenePaketi.Add(dp);
            s.SaveChanges();

            return RedirectToAction("index", "Admin");
        }
        public ActionResult DodajDomenDomenPaket()
        {
            mssql s = new mssql();
            ViewData["dp"] = s.DomenePaketi.Include(x => x.Domena).Where(x=>x.Deaktiviran==false).ToList();
            ViewData["d"] = s.Domena.Where(x=>x.Deaktiviran==false).ToList();
            return View("DodajDomenDomenPaket");
        }

        public ActionResult deaktivacijapaketa(int id)
        {
            mssql s = new mssql();
            DomenePaketi dp = s.DomenePaketi.SingleOrDefault(x => x.Id == id);
            dp.Deaktiviran = true;
            s.SaveChanges();
            return RedirectToAction("DodajDomenDomenPaket");
        }
        public ActionResult deaktivacijadomen(int id)
        {
            mssql s = new mssql();
            Domena d = s.Domena.SingleOrDefault(x => x.Id == id);
            d.Deaktiviran = true;
            s.SaveChanges();
            return RedirectToAction("DodajDomenDomenPaket");
        }
    }
}