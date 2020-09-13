using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Smart_Trafic_Management_System.Models;

namespace UserPanel.Controllers
{
    public class ComplaintsController : Controller
    {
        private CodeTitansEntities1 db = new CodeTitansEntities1();

        // GET: Complaints
        public ActionResult Index()
        {
            int id = Convert.ToInt32(Session["user_id"].ToString());
            if ( id > 0)
            {

                var complaints = db.Complaints.Where(x=>x.User_ID==id);
                return View(complaints.ToList());
            }
            else
            {
                return RedirectToAction("Login","User");
            }
        }

        [HttpPost]
        public ActionResult Index(Complaint c)
        {
            var data = db.Complaints.Where(x => x.Complaint_ID == c.Complaint_ID).ToList();
            return View(data);

        }

        // GET: Complaints/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Complaint complaint = db.Complaints.Find(id);
            if (complaint == null)
            {
                return HttpNotFound();
            }
            return View(complaint);
        }

        // GET: Complaints/Create
        public ActionResult Create()
        {
            ViewBag.Category_ID = new SelectList(db.Complaint_Category, "Category_ID", "Category_Name");
           
           
            return View();
        }

        // POST: Complaints/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Complaint complaint)
        {
            if (ModelState.IsValid)
            {
                db.Complaints.Add(new Complaint {
                    User_ID = Convert.ToInt32( Session["user_id"].ToString()),
                    Category_ID = complaint.Category_ID,
                    Title = complaint.Title,
                    Description = complaint.Description,
                    Date =DateTime.Now ,
                    Status="In_Process"
                   
            }                 
                    
                    
                    );
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Category_ID = new SelectList(db.Complaint_Category, "Category_ID", "Category_Name", complaint.Category_ID);
         
            return View(complaint);
        }
        
        public ActionResult Delete(int id,Complaint c)
        {
            Complaint complaint = db.Complaints.Find(id);
              db.Complaints.Remove(complaint);
           
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Display of complains on user panel


        [HttpGet]
        public ActionResult Complaints(string search)
        {
            var value = Convert.ToInt32(Request["list"]);
            List<Complaint_Category> category = db.Complaint_Category.ToList();
            ViewBag.list = new SelectList(category, "Category_ID", "Category_Name");
            var complaints = db.Complaints.ToList();
            if (!string.IsNullOrEmpty(search))
            {


                complaints = db.Complaints.Where(x => x.Title == search || x.User.First_Name == search && x.Category_ID == value).ToList();
            }
            else if (search == null)
            {
                complaints = db.Complaints.Where(x => x.Category_ID == value).ToList();


            }
            else
            {

                complaints = db.Complaints.Where(x => x.Category_ID == value).ToList();



            }

            ViewBag.c = complaints;
            return View();
        }

        [HttpPost]
        public ActionResult Complaints(string value, int id)
        {



            if (value == "In Process")
            {
                Complaint c = db.Complaints.Find(id);

                db.Entry(c).CurrentValues.SetValues(new Complaint
                {
                    Complaint_ID = id,
                    User_ID = c.User_ID,
                    Category_ID = c.Category_ID,
                    Date = c.Date,
                    Title = c.Title,
                    Description = c.Description,
                    Status = "Resolved"

                });
                db.SaveChanges();
                return RedirectToAction("Complaints", "Complaints");

            }
            else if (value == "Resolved")
            {

                Complaint c = db.Complaints.Find(id);

                db.Entry(c).CurrentValues.SetValues(new Complaint
                {
                    Complaint_ID = id,
                    User_ID = c.User_ID,
                    Category_ID = c.Category_ID,
                    Date = c.Date,
                    Title = c.Title,
                    Description = c.Description,
                    Status = "In_Process"

                });

                db.SaveChanges();
                return RedirectToAction("Complaints", "Complaints");

            }


            return RedirectToAction("Complaints", "Complaints");

        }
        public JsonResult getcategory(int Category_ID)
        {

            db.Configuration.ProxyCreationEnabled = false;
            var cat = db.Complaint_Category.Where(x => x.Category_ID == Category_ID);
            return Json(cat, JsonRequestBehavior.AllowGet);
        }

    }



}
