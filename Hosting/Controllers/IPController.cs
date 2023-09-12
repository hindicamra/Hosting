using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using DataAccessLayer.Klase;

namespace Hosting.Controllers
{
    public class IPController : Controller
    {
        // GET: IP
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DostupneIPDodaj()
        {
            mssql s = new mssql();
            ViewData["IP"] = s.DostupneIP.ToList();
            return View("DostupneIPDodaj");
        }
        public ActionResult DostupneIPSnimi(string IP, string Subnet)
        {
            mssql s = new mssql();
            DostupneIP dip = new DostupneIP();
            dip.IP = IP;
            dip.Subnet = Subnet;
            dip.Zauzeto = true;

            s.DostupneIP.Add(dip);
            s.SaveChanges();

            return RedirectToAction("index", "Admin");
        }
        public ActionResult CallAjax()
        {
            return View("DodajNoveIP");
        }
    }
}