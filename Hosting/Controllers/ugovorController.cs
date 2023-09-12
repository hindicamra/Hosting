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
    public class ugovorController : Controller
    {
        // GET: ugovor
        public ActionResult Index()
        {
            mssql s = new mssql();
            ViewData["status"] = s.Status.ToList();

            List<Ugovor> u = new List<Ugovor>(s.Ugovor.Where(x => x.Hidden == false).OrderByDescending(x => x.DatumKreiranjaZahtjeva)
                .Include(x => x.Klijenti)
                .Include(x=>x.Status)
                .Include(x=>x.SSLPaketi)
                .Include(x=>x.HostingPaketi)
                .Include(x=>x.DedicatedPaketi)
                .Include(x=>x.VpsPaketi)
                .Include(x=>x.Domena)
                .Include(x=>x.Domena.Domena)
                .OrderBy(x=>x.DatumVazenja)
                );
            ViewData["u"] = u;
            return View();
        }
        public ActionResult Pretraga(string I, string P)
        {
            mssql s = new mssql();
            ViewData["status"] = s.Status.ToList();
            if (I!="" && P!= "")
            {
                List<Ugovor> u = new List<Ugovor>(s.Ugovor.Where(x => x.Hidden == false && x.Klijenti.Ime == I && x.Klijenti.Prezime == P).OrderByDescending(x => x.DatumKreiranjaZahtjeva)
                .Include(x => x.Klijenti)
                .Include(x => x.Status)
                .Include(x => x.SSLPaketi)
                .Include(x => x.HostingPaketi)
                .Include(x => x.DedicatedPaketi)
                .Include(x => x.VpsPaketi)
                .Include(x => x.Domena)
                .Include(x => x.Domena.Domena)
                .OrderBy(x => x.DatumVazenja)
                );
                ViewData["u"] = u;
                return View("index");
            }
            if (I == "" && P != "")
            {
                List<Ugovor> u = new List<Ugovor>(s.Ugovor.Where(x => x.Hidden == false && x.Klijenti.Prezime == P).OrderByDescending(x => x.DatumKreiranjaZahtjeva)
                .Include(x => x.Klijenti)
                .Include(x => x.Status)
                .Include(x => x.SSLPaketi)
                .Include(x => x.HostingPaketi)
                .Include(x => x.DedicatedPaketi)
                .Include(x => x.VpsPaketi)
                .Include(x => x.Domena)
                .Include(x => x.Domena.Domena)
                .OrderBy(x => x.DatumVazenja)
                );
                ViewData["u"] = u;
                return View("index");
            }
            if (I != "" && P == "")
            {
                List<Ugovor> u = new List<Ugovor>(s.Ugovor.Where(x => x.Hidden == false && x.Klijenti.Ime == I).OrderByDescending(x => x.DatumKreiranjaZahtjeva)
                .Include(x => x.Klijenti)
                .Include(x => x.Status)
                .Include(x => x.SSLPaketi)
                .Include(x => x.HostingPaketi)
                .Include(x => x.DedicatedPaketi)
                .Include(x => x.VpsPaketi)
                .Include(x => x.Domena)
                .Include(x => x.Domena.Domena)
                .OrderBy(x => x.DatumVazenja)
                );
                ViewData["u"] = u;
                return View("index");
            }
            if (I == "" && P == "")
            {
                return RedirectToAction("index");
            }
            return RedirectToAction("index");
        }
        public ActionResult Promjeni(int uid, int status)
        {
            mssql s = new mssql();
            Ugovor u = s.Ugovor.SingleOrDefault(x => x.Id == uid);
            u.StatusId = status;
            if (status==2)
                u.boja = "coral";
            if (status == 4)
                u.boja = "indianred";
            s.SaveChanges();

            var user = Session["admin"] as Admin;
            AdminLog l = new AdminLog();
            l.Datum = DateTime.Now;
            l.Hidden = false;
            l.IP = Request.UserHostAddress + " " + Request.UserHostName + " " + Request.Browser.Browser;
            l.AdminId = user.Id;
            l.Poruka = "Ugovor id: "+u.Id+", status "+u.Status.Naziv;
            s.AdminLog.Add(l);
            s.SaveChanges();


            return RedirectToAction("Index", "ugovor");
        }
        public ActionResult Deaktiviraj(int id)
        {
            mssql s = new mssql();
            Ugovor u = s.Ugovor.SingleOrDefault(x => x.Id == id);
            u.Hidden=true;
            s.SaveChanges();

            var user = Session["admin"] as Admin;
            AdminLog l = new AdminLog();
            l.Datum = DateTime.Now;
            l.Hidden = false;
            l.IP = Request.UserHostAddress + " " + Request.UserHostName + " " + Request.Browser.Browser;
            l.AdminId = user.Id;
            l.Poruka = "Ugovor id: " + u.Id + ", deaktiviran";
            s.AdminLog.Add(l);
            s.SaveChanges();

            return RedirectToAction("Index", "ugovor");
        }
        public ActionResult Aktiviraj(int id)
        {
            mssql s = new mssql();
            Ugovor u = s.Ugovor.SingleOrDefault(x => x.Id == id);
            u.DatumOdobrenja = DateTime.Now;
            if (u.VpsPaketiId!=null || u.DedicatedPaketiId!=null)
            {
                u.DatumVazenja = DateTime.Now.AddMonths(1);
            }
            else
            {
                u.DatumVazenja = DateTime.Now.AddYears(1);
            }
            u.StatusId = 3;
            u.boja = "aqua";
            s.SaveChanges();


            var user = Session["admin"] as Admin;
            AdminLog l = new AdminLog();
            l.Datum = DateTime.Now;
            l.Hidden = false;
            l.IP = Request.UserHostAddress + " " + Request.UserHostName + " " + Request.Browser.Browser;
            l.AdminId = user.Id;
            l.Poruka = "Ugovor id: " + u.Id + ", Aktiviran ";
            s.AdminLog.Add(l);
            s.SaveChanges();

            return RedirectToAction("Index", "ugovor");
        }
        public ActionResult produzi(int id)
        {
            mssql s = new mssql();
            Ugovor u = s.Ugovor.SingleOrDefault(x => x.Id == id);
            u.produzenje = true;
            u.boja = "crimson";
            s.SaveChanges();

            Produzenja p = new Produzenja();
            p.UgovorId = id;
            p.DatumZahtjeva = p.DatumRealizacije = DateTime.Now;
            s.Produzenja.Add(p);
            s.SaveChanges();

            var user = Session["admin"] as Admin;
            AdminLog l = new AdminLog();
            l.Datum = DateTime.Now;
            l.Hidden = false;
            l.IP = Request.UserHostAddress + " " + Request.UserHostName + " " + Request.Browser.Browser;
            l.AdminId = user.Id;
            l.Poruka = "Ugovor id: " + u.Id + ", Produzen";
            s.AdminLog.Add(l);
            s.SaveChanges();

            return RedirectToAction("KlijentPanel", "Klijent");
        }
        public ActionResult ProduziUslugu(int id)
        {
            mssql s = new mssql();
            Ugovor u = s.Ugovor.SingleOrDefault(x => x.Id == id);
            u.produzenje = false;
            u.boja = "aqua";
            if (u.VpsPaketiId != null || u.DedicatedPaketiId != null)
            {
                u.DatumVazenja = DateTime.Now.AddMonths(1);
            }
            else
            {
                u.DatumVazenja = DateTime.Now.AddYears(1);
            }
            s.SaveChanges();

            Produzenja p = s.Produzenja.Where(x => x.UgovorId == id).OrderByDescending(x => x.Id).First();
            p.DatumRealizacije=DateTime.Now;
            s.SaveChanges();

            var user = Session["admin"] as Admin;
            AdminLog l = new AdminLog();
            l.Datum = DateTime.Now;
            l.Hidden = false;
            l.IP = Request.UserHostAddress + " " + Request.UserHostName + " " + Request.Browser.Browser;
            l.AdminId = user.Id;
            l.Poruka = "Ugovor id: " + u.Id + ", Produzenje";
            s.AdminLog.Add(l);
            s.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }
        public ActionResult DodajPristupnePodatke(int id)
        {
            mssql s = new mssql();
            ViewData["u"] = s.Ugovor.SingleOrDefault(x => x.Id == id);

            return View("DodajPristupnePodatke");
        }
        public ActionResult DodajPristupnePodatkeSnimi(int id, string PA, string U, string P)
        {
            mssql s = new mssql();
            Ugovor u = s.Ugovor.SingleOrDefault(x => x.Id == id);
            u.Username = U;
            u.Password = P;
            u.PristupnaAdresa = PA;
            s.SaveChanges();

            var user = Session["admin"] as Admin;
            AdminLog l = new AdminLog();
            l.Datum = DateTime.Now;
            l.Hidden = false;
            l.IP = Request.UserHostAddress + " " + Request.UserHostName + " " + Request.Browser.Browser;
            l.AdminId = user.Id;
            l.Poruka = "Dodavanje pristupnih podataka";
            s.AdminLog.Add(l);
            s.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }
    }
}