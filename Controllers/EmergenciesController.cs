using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using Smart_Trafic_Management_System.Models;

namespace CodeTitans_STMS.Controllers
{
    public class EmergenciesController : Controller
    {
        CodeTitansEntities1 db = new CodeTitansEntities1();
          
        [HttpGet]
        public ActionResult Emergency() {
            if (Session["admin_id"] != null)
            {


                var e = db.Emergencies.ToList();
            
            return View(e);
        }
            else
            {
                return RedirectToAction("Indexlogout", "User");
    }
}
        [HttpPost]
        public ActionResult Emergency(int id) {

            Emergency EM = db.Emergencies.Find(id);
            db.Emergencies.Remove(EM);
            db.SaveChanges();


            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("codetitans007@gmail.com", "stms1234");
            smtp.EnableSsl = true;
            MailMessage msg = new MailMessage();
            msg.Subject = "Emegency Status Forward";
            msg.Body = "Your Emergency complain has been recieved and team has been deployed at your location";

            string addressto = EM.User.Email_ID;
            msg.To.Add(addressto);
            msg.From = new MailAddress(String.Format("Emergency Status<{0}>", "codetitans007@gmail.com"));

            try
            {
                smtp.Send(msg);
            }
            catch (Exception ex)
            {
                ViewBag.Succes = ex.Message;
                return View();
            }
            ViewBag.Succes = "Message is successfully send";
            

            return RedirectToAction("Emergency", "Emergencies");

        }

       
     
    }
}
