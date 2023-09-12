using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using DataAccessLayer.Klase;

namespace Hosting.Controllers
{
    public class KlijentController : Controller
    {
        // GET: Klijent
        public ActionResult Dodaj(string Domena, int DomenePaketiId)
        {
            mssql s = new mssql();
            ViewData["g"] = s.Grad.ToList();
            ViewData["Domena"] = Domena;
            ViewData["DomenePaketiId"] = DomenePaketiId;
            return View("Dodaj");
        }

        public ActionResult Snimi(string Domena, int DomenePaketiId, string Ime, string Prezime, string Email,
            string Adresa, string Telefon, string Mobitel, string Fax, string JMBG, string IDBroj, string PDVBroj,
            string Password,
            int Grad)
        {
            mssql s = new mssql();
            Klijenti k = new Klijenti();
            k.Ime = Ime;
            k.Prezime = Prezime;
            k.Email = Email;
            k.Adresa = Adresa;
            k.Telefon = Telefon;
            k.Mobitel = Mobitel;
            k.Fax = Fax;
            k.JMBG = JMBG;
            k.IDBroj = IDBroj;
            k.PDVBroj = PDVBroj;
            k.Password = Password;
            k.GradId = Grad;

            s.Klijenti.Add(k);
            s.SaveChanges();

            Ugovor u = new Ugovor();
            u.DatumKreiranjaZahtjeva = DateTime.Now;
            u.Hidden = false;
            u.DatumOdobrenja = DateTime.Now;
            u.DatumVazenja = DateTime.Now;
            u.DomenaId = DomenePaketiId;

            Klijenti kl = s.Klijenti.Single(x => x.Email == k.Email);

            u.KlijentiId = kl.Id;
            u.StatusId = 1;
            u.boja = "crimson";

            s.Ugovor.Add(u);
            s.SaveChanges();

            return RedirectToAction("KlijentInfoPristup", "Klijent", new {Username = k.Email, Password = k.Password});
        }

        public ActionResult KlijentInfoPristup(string Username, string Password)
        {
            ViewData["username"] = Username;
            ViewData["password"] = Password;
            return View("KlijentInfoPristup");
        }

        public ActionResult FizickoDodaj(string Domena, int? DomenePaketiId, int? sslid, int? vpsid, int? did, int? hid)
        {
            mssql s = new mssql();
            ViewData["g"] = s.Grad.ToList();

            if (DomenePaketiId != null)
            {
                ViewData["Domena"] = Domena;
                ViewData["DomenePaketiId"] = DomenePaketiId;
            }
            if (sslid != null)
            {
                ViewData["Domena"] = Domena;
                ViewData["sslid"] = sslid;
            }
            if (vpsid != null)
                ViewData["vpsid"] = vpsid;

            if (did != null)
                ViewData["did"] = did;
            if (hid != null)
            {
                ViewData["hid"] = hid;
                ViewData["Domena"] = Domena;
            }

            return View("FizickoLice");
        }

        public ActionResult FizickoSnimi(string Domena, int? DomenePaketiId, string Ime, string Prezime, string Email,
            string Adresa, string Telefon, string Mobitel, string JMBG, string Password, int Grad, int? sslid,
            int? vpsid, int? did, int? hid)//kupovina domene/ssl
        {
            mssql s = new mssql();
            Klijenti k = new Klijenti();
            k.Ime = Ime;
            k.Prezime = Prezime;
            k.Email = Email;
            k.Adresa = Adresa;
            k.Telefon = Telefon;
            k.Mobitel = Mobitel;
            k.JMBG = JMBG;
            k.Password = Password;
            k.GradId = Grad;

            s.Klijenti.Add(k);
            s.SaveChanges();

            Ugovor u = new Ugovor();
            u.DatumKreiranjaZahtjeva = DateTime.Now;
            u.Hidden = false;
            u.DatumOdobrenja = DateTime.Now;
            u.DatumVazenja = DateTime.Now;
            if (DomenePaketiId != null) //Kupovina domene
            {
                u.DomenaId = DomenePaketiId;
                u.ZakupljeniDomen = Domena;
            }
            if (sslid != null) //Kupovina ssl
            {
                u.SSLPaketiId = sslid;
                u.SSLZaDomenu = Domena;
            }
            if (vpsid != null)
                u.VpsPaketiId = vpsid;
            if (did != null)
                u.DedicatedPaketiId = did;
            if (hid != null)
            {
                u.HostingPaketiId = hid;
                u.ZakupljeniDomen = Domena;
            }
            u.IP = Request.UserHostAddress;

            Klijenti kl = s.Klijenti.Single(x => x.Email == k.Email);

            u.KlijentiId = kl.Id;
            u.StatusId = 1;
            u.boja = "crimson";

            s.Ugovor.Add(u);
            s.SaveChanges();

            return RedirectToAction("KlijentInfoPristup", "Klijent", new {Username = k.Email, Password = k.Password});
        }

        public ActionResult PravnoDodaj(string Domena, int? DomenePaketiId, int? sslid, int? vpsid, int? did, int? hid)
        {
            mssql s = new mssql();
            ViewData["g"] = s.Grad.ToList();

            if (DomenePaketiId != null)
            {
                ViewData["Domena"] = Domena;
                ViewData["DomenePaketiId"] = DomenePaketiId;
            }
            if (sslid != null)
            {
                ViewData["Domena"] = Domena;
                ViewData["sslid"] = sslid;
            }
            if (vpsid != null)
                ViewData["vpsid"] = vpsid;
            if (did != null)
                ViewData["did"] = did;
            if (hid != null)
            {
                ViewData["hid"] = hid;
                ViewData["Domena"] = Domena;
            }

            return View("PravnoLice");
        }

        public ActionResult PravnoSnimi(string Domena, int? DomenePaketiId, string Firma, string Email, string Adresa,
            string Telefon, string Mobitel, string Fax, string IDBroj, string PDVBroj, string Password, int Grad,
            int? sslid, int? vpsid, int? did, int? hid)
        {
            mssql s = new mssql();
            Klijenti k = new Klijenti();
            k.Firma = Firma;
            k.Email = Email;
            k.Adresa = Adresa;
            k.Telefon = Telefon;
            k.Mobitel = Mobitel;
            k.Fax = Fax;
            k.IDBroj = IDBroj;
            k.PDVBroj = PDVBroj;
            k.Password = Password;
            k.GradId = Grad;

            s.Klijenti.Add(k);
            s.SaveChanges();

            Ugovor u = new Ugovor();
            u.DatumKreiranjaZahtjeva = DateTime.Now;
            u.Hidden = false;
            u.DatumOdobrenja = DateTime.Now;
            u.DatumVazenja = DateTime.Now;
            if (DomenePaketiId != null) //Kupovina domene
            {
                u.DomenaId = DomenePaketiId;
                u.ZakupljeniDomen = Domena;
            }
            if (sslid != null) //Kupovina ssl
            {
                u.SSLPaketiId = sslid;
                u.SSLZaDomenu = Domena;
            }
            if (vpsid != null) //Kupovina vps
                u.VpsPaketiId = vpsid;
            if (did != null) //kupovina dedicated
                u.DedicatedPaketiId = did;
            if (hid != null) //kupovina hostinga
            {
                u.HostingPaketiId = hid;
                u.ZakupljeniDomen = Domena;
            }
            u.IP = Request.UserHostAddress;

            Klijenti kl = s.Klijenti.Single(x => x.Email == k.Email);

            u.KlijentiId = kl.Id;
            u.StatusId = 1;
            u.boja = "crimson";

            s.Ugovor.Add(u);
            s.SaveChanges();

            return RedirectToAction("KlijentInfoPristup", "Klijent", new {Username = k.Email, Password = k.Password});
        }

        public ActionResult Lice(string Domena, int? DomenePaketiId, string upozorenje, int? sslid, int? vpsid, int? did,
            int? hid)
        {
            if (DomenePaketiId != null)
            {
                ViewData["Domena"] = Domena;
                ViewData["DomenePaketiId"] = DomenePaketiId;
            }
            if (sslid != null)
            {
                ViewData["Domena"] = Domena;
                ViewData["sslid"] = sslid;
            }

            if (string.IsNullOrEmpty(upozorenje))
                ViewData["upozorenje"] = "hidden";
            else
                ViewData["upozorenje"] = upozorenje;

            if (vpsid != null)
                ViewData["vpsid"] = vpsid;

            if (did != null)
                ViewData["did"] = did;

            if (hid != null)
            {
                ViewData["hid"] = hid;
                ViewData["Domena"] = Domena;
            }

            return View("Lice");
        }

        public ActionResult ProvjeraUserPassword(string Domena, int? DomenePaketiId, string Username, string Password,
            int? sslid, int? vpsid, int? did, int? hid)
        {
            mssql s = new mssql();
            Klijenti k = s.Klijenti.SingleOrDefault(x => x.Email == Username && x.Password == Password);
            if (k != null)
            {
                Ugovor u = new Ugovor();
                u.DatumKreiranjaZahtjeva = DateTime.Now;
                u.Hidden = false;
                u.DatumOdobrenja = DateTime.Now;
                u.DatumVazenja = DateTime.Now;
                if (DomenePaketiId != null) //kupovina domene
                {
                    u.DomenaId = DomenePaketiId;
                    u.ZakupljeniDomen = Domena;
                }
                if (sslid != null) //kupovina ssl
                {
                    u.SSLPaketiId = sslid;
                    u.SSLZaDomenu = Domena;
                }
                if (vpsid != null)
                    u.VpsPaketiId = vpsid;
                if (did != null)
                    u.DedicatedPaketiId = did;
                if (hid != null)
                {
                    u.HostingPaketiId = hid;
                    u.ZakupljeniDomen = Domena;
                }

                u.IP = Request.UserHostAddress;

                u.KlijentiId = k.Id;
                u.StatusId = 1;

                s.Ugovor.Add(u);
                s.SaveChanges();

                return RedirectToAction("KlijentInfoPristup", "Klijent", new {Username = k.Email, Password = k.Password});
            }
            else
            {
                return RedirectToAction("Lice", "Klijent",
                    new
                    {
                        Domena = Domena,
                        DomenePaketiId = DomenePaketiId,
                        upozorenje = "visible",
                        sslid = sslid,
                        vpsid = vpsid,
                        did = did
                    });
            }
        }

        public ActionResult Login()
        {
            var user = Session["user"] as Klijenti;
            if (Session["user"] != null)
                return RedirectToAction("KlijentPanel", "Klijent");
            return View("Login");
        }
        public ActionResult Logout()
        {
            Session.Clear();
            //Session["user"] = null; //ILI
            return View("Login");
        }

        public ActionResult Provjera(string username, string password)
        {
            mssql s = new mssql();
            Klijenti k = s.Klijenti.SingleOrDefault(x => x.Email == username && x.Password == password);
            if (k == null)
            {
                KlijentLog l= new KlijentLog();
                l.Datum=DateTime.Now;
                l.Hidden = false;
                l.IP = Request.UserHostAddress;
                l.Hostname = Request.UserHostName;
                l.Browser = Request.Browser.Browser;
                s.KlijentLog.Add(l);
                s.SaveChanges();

                return RedirectToAction("Login", "Klijent");
            }
            else
            {
                KlijentLog l = new KlijentLog();
                l.Datum = DateTime.Now;
                l.Hidden = false;
                l.IP = Request.UserHostAddress;
                l.Hostname = Request.UserHostName;
                l.Browser = Request.Browser.Browser;
                l.KlijentId = k.Id;
                s.KlijentLog.Add(l);
                s.SaveChanges();

                Session["user"] = new Klijenti() {Email = username, Ime = k.Ime, Prezime = k.Prezime, Id = k.Id};
                return RedirectToAction("KlijentPanel", "Klijent");
            }
        }
        public ActionResult KlijentPanel()
        {
            mssql s = new mssql();
            var user = Session["user"] as Klijenti;
            if (Session["user"]==null)
                return RedirectToAction("Login");

            int brojZakupljeniHostinga = s.Ugovor.Count(x=>x.HostingPaketiId!=null && x.KlijentiId==user.Id);
            ViewData["hostingPaketa"] = brojZakupljeniHostinga;
            List<Ugovor> hostingugovori = new List<Ugovor>(s.Ugovor.Where(x => x.HostingPaketiId != null && x.KlijentiId==user.Id));
            ViewData["hostingPaketaKupljeno"] = hostingugovori;

            int brojZakupljenihVPS = s.Ugovor.Count(x => x.VpsPaketiId != null && x.KlijentiId == user.Id);
            ViewData["brojZakupljenihVPS"] = brojZakupljenihVPS;
            List<Ugovor> vpsugovori = new List<Ugovor>(s.Ugovor.Where(x => x.VpsPaketiId != null && x.KlijentiId == user.Id));
            ViewData["vpsugovori"] = vpsugovori;

            int brojZakupljenihDEDICATED = s.Ugovor.Count(x => x.DedicatedPaketiId != null && x.KlijentiId == user.Id);
            ViewData["brojZakupljenihDEDICATED"] = brojZakupljenihDEDICATED;
            List<Ugovor> dedicatedugovori = new List<Ugovor>(s.Ugovor.Where(x => x.DedicatedPaketiId != null && x.KlijentiId == user.Id));
            ViewData["dedicatedugovori"] = dedicatedugovori;

            int brojZakupljenihDomena = s.Ugovor.Count(x => x.DomenaId != null && x.KlijentiId == user.Id);
            ViewData["brojZakupljenihDomena"] = brojZakupljenihDomena;
            List<Ugovor> domenepaketiugovori = new List<Ugovor>(s.Ugovor.Where(x => x.DomenaId != null && x.KlijentiId == user.Id).Include(x=>x.Domena.Domena));
            ViewData["domenepaketiugovori"] = domenepaketiugovori;

            int brojZakupljenihSSL = s.Ugovor.Count(x => x.SSLPaketiId != null && x.KlijentiId == user.Id);
            ViewData["brojZakupljenihSSL"] = brojZakupljenihSSL;
            List<Ugovor> SSLugovori = new List<Ugovor>(s.Ugovor.Where(x => x.SSLPaketiId != null && x.KlijentiId == user.Id).Include(x => x.Domena.Domena));
            ViewData["SSLugovori"] = SSLugovori;

            return View("KlijentPanel");
        }

        public ActionResult Log()
        {
            mssql s = new mssql();
            ViewData["log"] = s.KlijentLog.Include(x=>x.Klijent).OrderByDescending(x=>x.Datum).Take(100).ToList();

            List<Klijenti> klijenti = s.Klijenti.ToList();
            Klijenti kl = new Klijenti();
            kl.Ime = "Odaberite klijenta";
            klijenti.Insert(0, kl);
            ViewData["klijenti"] = klijenti;

            return View("Log");
        }
        public ActionResult LogPregled()
        {
            var user = Session["user"] as Klijenti;
            mssql s = new mssql();
            ViewData["log"] = s.KlijentLog.Include(x => x.Klijent).OrderByDescending(x => x.Datum).Where(x=>x.KlijentId==user.Id).Take(100).ToList();
            return View("LogPregled");
        }
        public ActionResult LogPretraga(DateTime? Od, DateTime? Do, int Kl)
        {
            mssql s = new mssql();

            List<Klijenti> klijenti = s.Klijenti.ToList();
            Klijenti kl = new Klijenti();
            kl.Ime = "Odaberite klijenta";
            klijenti.Insert(0, kl);
            ViewData["klijenti"] = klijenti;

            if (Od == null && Do == null && Kl == 0)
                return RedirectToAction("Log");
            else
            {
                if (Od == null && Do == null && Kl != 0)
                    ViewData["log"] = s.KlijentLog.Include(x => x.Klijent).Where(x => x.KlijentId == Kl).OrderByDescending(x => x.Id).ToList();
                if (Od == null && Do != null && Kl != 0)
                {
                    Do = Do.Value.AddDays(1);
                    ViewData["log"] = s.KlijentLog.Include(x => x.Klijent).Where(x => x.KlijentId == Kl && x.Datum <= Do).OrderByDescending(x => x.Id).ToList();
                }
                if (Od != null && Do != null && Kl == 0)
                {
                    Do = Do.Value.AddDays(1);
                    ViewData["log"] = s.KlijentLog.Include(x => x.Klijent).Where(x => x.Datum >= Od && x.Datum <= Do).OrderByDescending(x => x.Id).ToList();
                }
                if (Od != null && Do == null && Kl == 0)
                    ViewData["log"] = s.KlijentLog.Include(x => x.Klijent).Where(x => x.Datum >= Od).OrderByDescending(x => x.Id).ToList();
                if (Od != null && Do != null && Kl != 0)
                {
                    Do = Do.Value.AddDays(1);
                    ViewData["log"] = s.KlijentLog.Include(x => x.Klijent).Where(x => x.Datum >= Od && x.Datum <= Do && x.KlijentId == Kl).OrderByDescending(x => x.Id).ToList();
                }
                if (Od == null && Do != null && Kl != 0)
                {
                    Do = Do.Value.AddDays(1);
                    ViewData["log"] =
                        s.KlijentLog.Include(x => x.Klijent)
                            .Where(x => x.Datum <= Do && x.KlijentId == Kl)
                            .OrderByDescending(x => x.Id)
                            .ToList();
                }
                if (Od == null && Do != null && Kl == 0)
                {
                    Do = Do.Value.AddDays(1);
                    ViewData["log"] =
                        s.KlijentLog.Include(x => x.Klijent)
                            .Where(x => x.Datum <= Do)
                            .OrderByDescending(x => x.Id)
                            .ToList();
                }
            }
            return View("Log");
        }

        public ActionResult PromjenaPassworda()
        {
            mssql s = new mssql();
            var user = Session["user"] as Klijenti;
            ViewData["k"] = s.Klijenti.SingleOrDefault(x => x.Id == user.Id);
            return View("PromjenaPassworda");
        }
        public ActionResult PromjenaPasswordaSave(string pw)
        {
            mssql s = new mssql();
            var user = Session["user"] as Klijenti;
            Klijenti k = s.Klijenti.SingleOrDefault(x => x.Id == user.Id);
            k.Password = pw;
            s.SaveChanges();
            return RedirectToAction("KlijentPanel", "Klijent");
        }
    }
}