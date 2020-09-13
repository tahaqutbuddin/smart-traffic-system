using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Smart_Trafic_Management_System.Models;

namespace CodeTitans_STMS.Controllers
{
    public class FeedbacksController : Controller
    {
        CodeTitansEntities1 db = new CodeTitansEntities1();
       

     


        [HttpGet]
        public ActionResult feedback(string search) {
            if (Session["admin_id"] != null)
            {
             
           

            var feedback = db.Feedbacks.ToList();


            if (!string.IsNullOrEmpty(search))
            {
feedback = db.Feedbacks.Where(x => x.User.First_Name == search || x.User.Contact == search || x.User.Email_ID == search).ToList();

            }
            return View(feedback);
            }
            else
            {
                return RedirectToAction("Indexlogout", "User");
            }
        }

       

       
    }
}
