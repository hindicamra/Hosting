using DataAccessLayer;
using DataAccessLayer.Klase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hosting.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Dodaj()
        {
            mssql s = new mssql();
            ViewData["g"] = s.Grad.ToList();
            return View("Dodaj");
        }
        public ActionResult Snimi(string Ime, string Prezime, string Head, string Email, string Password, int Grad)
        {
            mssql s = new mssql();
            Admin a = new Admin();
            a.Ime = Ime;
            a.Prezime = Prezime;
            if (Head == "on")
                a.AdminHead = true;
            else
                a.AdminHead = false;
            a.Email = Email;
            a.Password = Password;
            a.GradId = Grad;
            s.Admin.Add(a);
            s.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            return View("Login");
        }
        public ActionResult Provjera(string username, string password)
        {
            mssql s = new mssql();
            Admin a = s.Admin.SingleOrDefault(x => x.Email == username && x.Password == password && x.Izbrisan==false);
            if (a == null)
            {
                AdminLog l = new AdminLog();
                l.Datum=DateTime.Now;
                l.Hidden = false;
                l.IP = Request.UserHostAddress+" "+Request.UserHostName+" "+Request.Browser.Browser;
                l.Poruka = "Logiranje ne postoji korisnik username: "+username+", password: "+password;
                s.AdminLog.Add(l);
                s.SaveChanges();

                return RedirectToAction("Login");
            }
            else
            {
                AdminLog l = new AdminLog();
                l.Datum = DateTime.Now;
                l.Hidden = false;
                l.IP = Request.UserHostAddress + " " + Request.UserHostName + " " + Request.Browser.Browser;
                l.AdminId = a.Id;
                l.Poruka = "Logiranje.";
                s.AdminLog.Add(l);
                s.SaveChanges();

                Session["admin"] = new Admin() {Ime = a.Ime, Prezime = a.Prezime, Id = a.Id, AdminHead = a.AdminHead};
                return RedirectToAction("Index");
            }
        }
        public ActionResult Logout()
        {
            var user = Session["admin"] as Admin;
            Session["admin"] = null;
            AdminLog l = new AdminLog();
            l.Datum = DateTime.Now;
            l.Hidden = false;
            l.IP = Request.UserHostAddress + " " + Request.UserHostName + " " + Request.Browser.Browser;
            l.Poruka = "Logout "+user.Ime+" "+user.Prezime;
            mssql s = new mssql();
            s.AdminLog.Add(l);
            s.SaveChanges();
            return RedirectToAction("Login");
        }

        public ActionResult AdminPregled()
        {
            mssql s = new mssql();
            List<Admin> a = s.Admin.Where(x=>x.Izbrisan==false).ToList();
            ViewData["a"] = a;
            return View("AdminPregled");
        }
        public ActionResult PrikaziPw(int Id)
        {
            mssql s = new mssql();
            Admin a = s.Admin.SingleOrDefault(x => x.Id == Id);
            ViewData["a"] = a;
            AdminLog l = new AdminLog();
            l.Datum = DateTime.Now;
            l.Hidden = false;
            l.IP = Request.UserHostAddress + " " + Request.UserHostName + " " + Request.Browser.Browser;
            var user = Session["admin"] as Admin;
            l.Poruka = "Admin: "+user.Ime+" "+ user.Prezime+", pleda pw od: "+a.Ime+" "+a.Prezime;
            s.AdminLog.Add(l);
            s.SaveChanges();
            return View("PrikaziPw");
        }

        public ActionResult Log()
        {
            mssql s = new mssql();
            ViewData["log"] = s.AdminLog.Include(x=>x.Admin).OrderByDescending(x => x.Datum).Take(100).ToList();

            List<Admin> administratori = s.Admin.ToList();
            Admin fadm= new Admin();
            fadm.Ime = "Odaberite admina";
            administratori.Insert(0, fadm);
            ViewData["administratori"] = administratori;
            return View("Log");
        }
        public ActionResult LogPretraga(DateTime? Od, DateTime? Do, int Adm)
        {
            mssql s = new mssql();

            List<Admin> administratori = s.Admin.Where(x=>x.Izbrisan==false).ToList();
            Admin fadm = new Admin();
            fadm.Ime = "Odaberite admina";
            administratori.Insert(0, fadm);
            ViewData["administratori"] = administratori;

            if (Od == null && Do == null && Adm == 0)
                return RedirectToAction("Log");
            else
            {
                if (Od == null && Do == null && Adm != 0)
                    ViewData["log"] = s.AdminLog.Include(x => x.Admin).Where(x => x.AdminId == Adm).OrderByDescending(x=>x.Id).ToList();
                if (Od == null && Do != null && Adm != 0)
                {
                    Do = Do.Value.AddDays(1);
                    ViewData["log"] = s.AdminLog.Include(x => x.Admin).Where(x => x.AdminId == Adm && x.Datum<=Do).OrderByDescending(x => x.Id).ToList();
                }
                if (Od != null && Do != null && Adm == 0)
                {
                    Do = Do.Value.AddDays(1);
                    ViewData["log"] = s.AdminLog.Include(x => x.Admin).Where(x => x.Datum >= Od && x.Datum <= Do).OrderByDescending(x => x.Id).ToList();
                }
                if (Od != null && Do == null && Adm == 0)
                    ViewData["log"] = s.AdminLog.Include(x => x.Admin).Where(x => x.Datum >= Od).OrderByDescending(x => x.Id).ToList();
                if (Od != null && Do != null && Adm != 0)
                {
                    Do = Do.Value.AddDays(1);
                    ViewData["log"] = s.AdminLog.Include(x => x.Admin).Where(x => x.Datum >= Od && x.Datum <= Do && x.AdminId==Adm).OrderByDescending(x => x.Id).ToList();
                }
                if (Od == null && Do != null && Adm != 0)
                {
                    Do = Do.Value.AddDays(1);
                    ViewData["log"] =
                        s.AdminLog.Include(x => x.Admin)
                            .Where(x => x.Datum <= Do && x.AdminId == Adm)
                            .OrderByDescending(x => x.Id)
                            .ToList();
                }
                if (Od == null && Do != null && Adm == 0)
                {
                    Do = Do.Value.AddDays(1);
                    ViewData["log"] =
                        s.AdminLog.Include(x => x.Admin)
                            .Where(x => x.Datum <= Do)
                            .OrderByDescending(x => x.Id)
                            .ToList();
                }
                if (Od != null && Do == null && Adm != 0)
                {
                    ViewData["log"] =
                        s.AdminLog.Include(x => x.Admin)
                            .Where(x => x.Datum >= Od && x.AdminId==Adm)
                            .OrderByDescending(x => x.Id)
                            .ToList();
                }
            }
            return View("Log");
        }

        public ActionResult PromjenaPassworda()
        {
            mssql s = new mssql();
            var user = Session["admin"] as Admin;
            ViewData["a"] = s.Admin.SingleOrDefault(x => x.Id == user.Id);
            return View("PromjenaPassworda");
        }
        public ActionResult PromjenaPasswordaSave(string pw)
        {
            mssql s = new mssql();
            var user = Session["admin"] as Admin;
            Admin k = s.Admin.SingleOrDefault(x => x.Id == user.Id);
            k.Password = pw;
            s.SaveChanges();

            AdminLog l = new AdminLog();
            l.Datum = DateTime.Now;
            l.Hidden = false;
            l.IP = Request.UserHostAddress + " " + Request.UserHostName + " " + Request.Browser.Browser;
            l.Poruka = "Admin: " + user.Ime + " " + user.Prezime + ", mjenja password od: "+k.Ime+" "+k.Prezime;
            s.AdminLog.Add(l);
            s.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }
        public ActionResult Brisi(int Id)
        {
            mssql s = new mssql();
            var user = Session["admin"] as Admin;
            Admin k = s.Admin.SingleOrDefault(x => x.Id == user.Id);
            k.Izbrisan = true;
            s.SaveChanges();

            AdminLog l = new AdminLog();
            l.Datum = DateTime.Now;
            l.Hidden = false;
            l.IP = Request.UserHostAddress + " " + Request.UserHostName + " " + Request.Browser.Browser;
            l.Poruka = "Admin: " + user.Ime + " " + user.Prezime + ", brise admina: " + k.Ime + " " + k.Prezime;
            s.AdminLog.Add(l);
            s.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }
    }
}