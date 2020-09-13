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
    public class Complaint_CategoryController : Controller
    {
        CodeTitansEntities1 db = new CodeTitansEntities1();
        // GET: Complaint_Category
        public ActionResult Index()
        {
            return View(db.Complaint_Category.ToList());
        }

        // GET: Complaint_Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Complaint_Category complaint_Category = db.Complaint_Category.Find(id);
            if (complaint_Category == null)
            {
                return HttpNotFound();
            }
            return View(complaint_Category);
        }

        // GET: Complaint_Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Complaint_Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Category_ID,Category_Name")] Complaint_Category complaint_Category)
        {
            if (ModelState.IsValid)
            {
                db.Complaint_Category.Add(complaint_Category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(complaint_Category);
        }

        // GET: Complaint_Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Complaint_Category complaint_Category = db.Complaint_Category.Find(id);
            if (complaint_Category == null)
            {
                return HttpNotFound();
            }
            return View(complaint_Category);
        }

        // POST: Complaint_Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Category_ID,Category_Name")] Complaint_Category complaint_Category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(complaint_Category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(complaint_Category);
        }

        // GET: Complaint_Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Complaint_Category complaint_Category = db.Complaint_Category.Find(id);
            if (complaint_Category == null)
            {
                return HttpNotFound();
            }
            return View(complaint_Category);
        }

        // POST: Complaint_Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Complaint_Category complaint_Category = db.Complaint_Category.Find(id);
            db.Complaint_Category.Remove(complaint_Category);
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
