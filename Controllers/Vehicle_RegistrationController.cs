using Smart_Trafic_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace CodeTitans_STMS.Controllers
{
    public class Vehicle_RegistrationController : Controller
    {
        CodeTitansEntities1 db = new CodeTitansEntities1();

        // GET: Vehicle_Registration
        public ActionResult Index()
        {
            if (Session["admin_id"] != null)
            {
                
            
            return View(db.Vehicle_Registration.ToList());
            }
            else
            {
                return RedirectToAction("Indexlogout", "User");
            }
        }

        // GET: Vehicle_Registration/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["admin_id"] != null)
            {
             
           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle_Registration vehicle_Registration = db.Vehicle_Registration.Find(id);
            if (vehicle_Registration == null)
            {
                return HttpNotFound();
            }
            return View(vehicle_Registration);
            }
            else
            {
                return RedirectToAction("Indexlogout", "User");
            }
        }

        // GET: Vehicle_Registration/Create
        public ActionResult Create()
        {
            if (Session["admin_id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Indexlogout", "User");
            }
        }

        // POST: Vehicle_Registration/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Vehicle_ID,Manufacturer,Date_of_Registration,Registration_Number,Model,Remarks,Vehicle_Type")] Vehicle_Registration vehicle_Registration)
        {
            if (ModelState.IsValid)
            {
                db.Vehicle_Registration.Add(vehicle_Registration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vehicle_Registration);
        }

        // GET: Vehicle_Registration/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["admin_id"] != null)
            {
           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle_Registration vehicle_Registration = db.Vehicle_Registration.Find(id);
            if (vehicle_Registration == null)
            {
                return HttpNotFound();
            }
            return View(vehicle_Registration);
                    
            }
            else
            {
                return RedirectToAction("Indexlogout", "User");
            }
        }

        // POST: Vehicle_Registration/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Vehicle_ID,Manufacturer,Date_of_Registration,Registration_Number,Model,Remarks,Vehicle_Type")] Vehicle_Registration vehicle_Registration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicle_Registration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vehicle_Registration);
        }

        // GET: Vehicle_Registration/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["admin_id"] != null)
            {
                
           

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle_Registration vehicle_Registration = db.Vehicle_Registration.Find(id);
            if (vehicle_Registration == null)
            {
                return HttpNotFound();
            }
            return View(vehicle_Registration);
            }
            else
            {
                return RedirectToAction("Indexlogout", "User");
            }
        }

        // POST: Vehicle_Registration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vehicle_Registration vehicle_Registration = db.Vehicle_Registration.Find(id);
            db.Vehicle_Registration.Remove(vehicle_Registration);
            db.SaveChanges();
            return RedirectToAction("Index");
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
