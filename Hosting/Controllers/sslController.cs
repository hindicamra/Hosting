using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using DataAccessLayer.Klase;

namespace Hosting.Controllers
{
    public class sslController : Controller
    {
        // GET: ssl
        public ActionResult Index()
        {
            mssql m = new mssql();
            List<SSLPaketi> sslp = m.SSLPaketi.Where(x=>x.Deaktivirano==false).ToList();
            ViewData["sslp"] = sslp;
            return View();
        }
        public ActionResult sslDodaj()
        {
            return View("sslDodaj");
        }
        public ActionResult Snimi(string Paket, string VrstaProvjere, string NivoPovjerenja, string ZelenaTraka, string GarancijaIzdavaca, string BrojOsiguranihPoddomena, string BrzinaIzdavanja, 
            string SSLEnkripcija, string Kompaktibilno, string KompaktibilnoMob, string Cijena)
        {
            mssql s = new mssql();
            SSLPaketi p = new SSLPaketi();
            p.Naziv = Paket;
            p.VrstaProvjere = VrstaProvjere;
            p.NivoPovjerenja = NivoPovjerenja;
            if(ZelenaTraka=="on")
                p.ZelenaTraka = true;
            else
                p.ZelenaTraka = false;
            p.GrancijaIzdavaca = GarancijaIzdavaca;
            p.BrojOsiguranihPoddomena = BrojOsiguranihPoddomena;
            p.BrzinaIzdavanja = BrzinaIzdavanja;
            p.SSLEnkripcija = SSLEnkripcija;
            if (Kompaktibilno == "on")
                p.Kompaktibilno = true;
            else
                p.Kompaktibilno = false;
            if (KompaktibilnoMob == "on")
                p.KompaktibilnoSaMobitelima = true;
            else
                p.KompaktibilnoSaMobitelima = false;
            p.Cijena = Cijena;

            s.SSLPaketi.Add(p);
            s.SaveChanges();

            return RedirectToAction("index", "Admin");
        }
        public ActionResult pregled()
        {
            mssql m = new mssql();
            List<SSLPaketi> sslp = m.SSLPaketi.Where(x=>x.Deaktivirano==false).ToList();
            ViewData["s"] = sslp;
            return View("pregled");
        }
        public ActionResult editSSL(int id)
        {
            mssql m = new mssql();
            ViewData["ssl"] = m.SSLPaketi.SingleOrDefault(x => x.Id == id);
            return View("edit");
        }
        public ActionResult SnimiEdit(int Id, string Paket, string VrstaProvjere, string NivoPovjerenja, string ZelenaTraka, string GarancijaIzdavaca, string BrojOsiguranihPoddomena, string BrzinaIzdavanja,
            string SSLEnkripcija, string Kompaktibilno, string KompaktibilnoMob, string Cijena)
        {
            mssql s = new mssql();
            SSLPaketi p = s.SSLPaketi.SingleOrDefault(x=>x.Id==Id);
            p.Naziv = Paket;
            p.VrstaProvjere = VrstaProvjere;
            p.NivoPovjerenja = NivoPovjerenja;
            if (ZelenaTraka == "on")
                p.ZelenaTraka = true;
            else
                p.ZelenaTraka = false;
            p.GrancijaIzdavaca = GarancijaIzdavaca;
            p.BrojOsiguranihPoddomena = BrojOsiguranihPoddomena;
            p.BrzinaIzdavanja = BrzinaIzdavanja;
            p.SSLEnkripcija = SSLEnkripcija;
            if (Kompaktibilno == "on")
                p.Kompaktibilno = true;
            else
                p.Kompaktibilno = false;
            if (KompaktibilnoMob == "on")
                p.KompaktibilnoSaMobitelima = true;
            else
                p.KompaktibilnoSaMobitelima = false;
            p.Cijena = Cijena;
            s.SaveChanges();

            return RedirectToAction("index", "Admin");
        }
        public ActionResult delete(int Id)
        {
            mssql s = new mssql();
            SSLPaketi p = s.SSLPaketi.SingleOrDefault(x => x.Id == Id);
            p.Deaktivirano = true;
            s.SaveChanges();

            return RedirectToAction("pregled");
        }
    }
}