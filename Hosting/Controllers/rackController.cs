using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using DataAccessLayer.Klase;

namespace Hosting.Controllers
{
    public class rackController : Controller
    {
        // GET: rack
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RackDodaj()
        {
            mssql s = new mssql();
            ViewData["r"] = s.Rack.ToList();
            return View("RackDodaj");
        }
        public ActionResult RackSnimi(string Rack)
        {
            mssql s = new mssql();
            Rack r = new Rack();
            r.Naziv = Rack;

            s.Rack.Add(r);
            s.SaveChanges();

            return RedirectToAction("index", "Admin");
        }
    }
}