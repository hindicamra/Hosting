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
    public class DrzavaKantonGradController : Controller
    {
        // GET: DrzavaKantonGrad
        public ActionResult DrzavaDodaj()
        {
            return View("DrzavaDodaj");
        }
        public ActionResult DrzavaSnimi(string Naziv)
        {
            mssql s = new mssql();
            Drzave d = new Drzave();
            d.Naziv = Naziv;
            s.Drzave.Add(d);
            s.SaveChanges();

            return RedirectToAction("DrzavaDodaj");
        }

        public ActionResult KantonDodaj()
        {
            mssql s = new mssql();
            ViewData["d"] = s.Drzave.ToList();
            return View("KantonDodaj");
        }
        public ActionResult kantonSnimi(string Naziv, int Drzava)
        {
            mssql s = new mssql();
            Kanton k = new Kanton();
            k.Naziv = Naziv;
            k.Drzava = Drzava;
            k.Drzave = s.Drzave.SingleOrDefault(x => x.Id == Drzava);
            s.Kanton.Add(k);
            s.SaveChanges();

            return RedirectToAction("KantonDodaj");
        }

        public ActionResult GradDodaj()
        {
            mssql s = new mssql();
            ViewData["k"] = s.Kanton.ToList();
            return View("GradDodaj");
        }
        public ActionResult GradSnimi(string Naziv, int Kanton)
        {
            mssql s = new mssql();
            Grad g = new Grad();
            g.Naziv = Naziv;
            g.KantonId = Kanton;

            s.Grad.Add(g);
            s.SaveChanges();

            return RedirectToAction("GradDodaj");
        }
        public ActionResult pregled()
        {
            mssql s = new mssql();
            List<Drzave> d = s.Drzave.OrderBy(x=>x.Naziv).ToList();
            ViewData["d"] = d;

            List<Kanton> k = s.Kanton.Include(x=>x.Drzave).OrderBy(x=>x.Drzava).ToList();
            ViewData["k"] = k;

            List<Grad> g = s.Grad.Include(x=>x.Kanton).Include(x=>x.Kanton.Drzave).OrderBy(x=>x.Kanton.Drzava & x.KantonId).ToList();
            ViewData["g"] = g;

            return View("pregled");
        }
    }
}