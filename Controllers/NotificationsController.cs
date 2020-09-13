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
    public class NotificationsController : Controller
    {
        CodeTitansEntities1 db = new CodeTitansEntities1();
        // GET: Notifications
        public ActionResult Index()
        {
            return View(db.Notifications.ToList());
        }

        // GET: Notifications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification notification = db.Notifications.Find(id);
            if (notification == null)
            {
                return HttpNotFound();
            }
            return View(notification);
        }

        // GET: Notifications/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Notifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Notification_ID,Date,Time,Location,Description")] Notification notification)
        {
            if (ModelState.IsValid)
            {
                db.Notifications.Add(notification);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(notification);
        }

        // GET: Notifications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification notification = db.Notifications.Find(id);
            if (notification == null)
            {
                return HttpNotFound();
            }
            return View(notification);
        }

        // POST: Notifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Notification_ID,Date,Time,Location,Description")] Notification notification)
        {
            if (ModelState.IsValid)
            {
                db.Entry(notification).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(notification);
        }

        // GET: Notifications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification notification = db.Notifications.Find(id);
            if (notification == null)
            {
                return HttpNotFound();
            }
            return View(notification);
        }

        // POST: Notifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Notification notification = db.Notifications.Find(id);
            db.Notifications.Remove(notification);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ShowNotifications(string search) {
            if (Session["admin_id"] != null)
            {
                
          
            var notify = db.Notifications.ToList();
            if(!string.IsNullOrEmpty(search))
            {

                notify = db.Notifications.Where(x => x.To == search).ToList();

            }
            return View(notify);
            }
            else
            {
                return RedirectToAction("Indexlogout", "User");
            }
        }

            public ActionResult CreateNote() {
            if (Session["admin_id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Indexlogout", "User");
            }
        }

            [HttpPost]
          public ActionResult CreateNote(Notification eml) {
            db.Notifications.Add(new Notification()
            {
                From = eml.From,
                Notification_ID = eml.Notification_ID,
                Password = eml.Password,
                To = eml.To,
                Location = eml.Location,
                Date = eml.Date,
                Description = eml.Description,
            });
            db.SaveChanges();
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential(eml.From, eml.Password);
            smtp.EnableSsl = true;
            MailMessage msg = new MailMessage();
            msg.Subject = "Smart Traffic Management System Notification";
            msg.Body = " Dear STMS," + "\n Location : " + eml.Location + "\n Date : " + eml.Date + " \n Description : " + eml.Description;
            string addressto = eml.To;
            msg.To.Add(addressto);
            msg.From = new MailAddress(String.Format("Notification STMS<{0}>", eml.From));
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
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
