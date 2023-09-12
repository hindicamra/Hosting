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
    public class tiketController : Controller
    {
        // GET: tiket
        public ActionResult Novi()
        {
            TiketiEditViewModel tevm = new TiketiEditViewModel();
            return View("Novi", tevm);
        }
        public ActionResult Kreiraj(TiketiEditViewModel tevm)
        {
            if(!ModelState.IsValid)
                return View("Novi", tevm);

            var user = Session["user"] as Klijenti;

            mssql s = new mssql();
            Tiketi t = new Tiketi();
            t.Naslov = tevm.Naslov;
            t.Datum=DateTime.Now;
            t.KlijentiId = user.Id;
            t.Zavrsen = false;
            t.Odgovoreno = false;

            s.Tiketi.Add(t);
            s.SaveChanges();

            Poruke p = new Poruke();
            p.Datum=DateTime.Now;
            p.Sadrzaj = tevm.Poruka;
            p.TiketiId = s.Tiketi.Single(x => x.Naslov == tevm.Naslov && x.KlijentiId == user.Id && x.Zavrsen==false).Id;

            s.Poruke.Add(p);
            s.SaveChanges();


            return View("Novi");
        }

        public ActionResult PregledAdmin(string error)
        {
            mssql s= new mssql();
            List<Tiketi> t = s.Tiketi.Include(x => x.Klijenti).Include(x=>x.Admin).Where(x=>x.Zavrsen==false).ToList();
            ViewData["t"] = t;
            ViewData["error"] = error;
            return View("PregledAdmin");
        }
        public ActionResult preuzmi(int id)
        {
            mssql s = new mssql();
            Tiketi t = s.Tiketi.SingleOrDefault(x => x.Id == id);

            var user = Session["admin"] as Admin;
            if (t.AdminId == null)
            {
                t.AdminId = user.Id;
                s.SaveChanges();
                return RedirectToAction("Index", "Admin");
            }   
            else
            {
                return RedirectToAction("PregledAdmin", new {error= "Tiket je upravo preuzeo drugi admin!" });
            }
        }
        public ActionResult zatvoriA(int id)
        {
            mssql s = new mssql();
            Tiketi t = s.Tiketi.SingleOrDefault(x => x.Id == id);
            t.Zavrsen = true;
            s.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }
        public ActionResult zatvoriK(int id)
        {
            mssql s = new mssql();
            Tiketi t = s.Tiketi.SingleOrDefault(x => x.Id == id);
            t.Zavrsen = true;
            s.SaveChanges();

            return RedirectToAction("KlijentPanel", "Klijent");
        }
        public ActionResult otvoriA(int id)
        {
            mssql s = new mssql();
            ViewData["t"] = s.Tiketi.SingleOrDefault(x => x.Id == id);
            ViewData["p"] =
                s.Poruke.Include(x => x.Admin)
                    .Include(x => x.Tiketi)
                    .Include(x => x.Tiketi.Klijenti)
                    .Where(x => x.TiketiId == id)
                    .ToList();

            return View("PregledPorukaAdmin");
        }
        public ActionResult snimiA(int Id, string poruka)
        {
            mssql s = new mssql();
            Poruke p = new Poruke();
            p.Sadrzaj = poruka;
            p.TiketiId = Id;
            var user = Session["admin"] as Admin;
            p.AdminId = user.Id;
            p.Datum=DateTime.Now;

            s.Poruke.Add(p);
            s.SaveChanges();

            Tiketi t = s.Tiketi.SingleOrDefault(x => x.Id == Id);
            t.Odgovoreno = true;
            s.SaveChanges();


            return RedirectToAction("Index", "Admin");
        }

        public ActionResult PregledKlijent()
        {
            var user = Session["user"] as Klijenti;
            mssql s = new mssql();
            List<Tiketi> t = s.Tiketi.Include(x => x.Klijenti).Include(x => x.Admin).Where(x => x.Zavrsen == false && x.KlijentiId==user.Id).ToList();
            ViewData["t"] = t;
            return View("PregledKlijent");
        }
        public ActionResult otvoriK(int id)
        {
            mssql s = new mssql();
            ViewData["t"] = s.Tiketi.Include(x=>x.Admin).SingleOrDefault(x => x.Id == id);
            ViewData["p"] =
                s.Poruke.Include(x => x.Admin)
                    .Include(x => x.Tiketi)
                    .Include(x => x.Tiketi.Klijenti)
                    .Where(x => x.TiketiId == id)
                    .ToList();

            return View("PregledPorukaKlijent");
        }
        public ActionResult snimiK(int Id, string poruka)
        {
            mssql s = new mssql();
            Poruke p = new Poruke();
            p.Sadrzaj = poruka;
            p.TiketiId = Id;
            p.Datum = DateTime.Now;

            s.Poruke.Add(p);
            s.SaveChanges();

            Tiketi t = s.Tiketi.SingleOrDefault(x => x.Id == Id);
            t.Odgovoreno = false;
            s.SaveChanges();


            return RedirectToAction("KlijentPanel", "Klijent");
        }
    }
}