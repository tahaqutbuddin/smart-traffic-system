using Smart_Trafic_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace vehicles_owners.Controllers
{
    public class vehicle_SearchController : Controller
    {
        CodeTitansEntities1 db = new CodeTitansEntities1();
        // GET: vehicle_Search
        public ActionResult Index(Vehicle_Registration v)
        {
            var data = db.Vehicle_Registration.Where(x=> x.Registration_Number == v.Registration_Number).ToList();
            return View(data);
        }

        public ActionResult Index2(Vehicle_Registration v)
        {
            var data = db.Vehicle_Registration.Where(x => x.Registration_Number == v.Registration_Number).ToList();
            return View(data);
        }
    }
}