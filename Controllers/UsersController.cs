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
    public class UsersController : Controller
    {
        CodeTitansEntities1 db = new CodeTitansEntities1();
        // GET: Users


        [HttpGet]
        public ActionResult users(string search)
        {
            if (Session["admin_id"] != null)
            {



                var u = db.Users.Where(x => x.Status == "Unverified");
                if (!string.IsNullOrEmpty(search))
                {

                    u = db.Users.Where(x => x.First_Name == search || x.Last_Name == search || x.Email_ID == search && x.Status.Contains("Unverified"));
                }
                return View(u);

            }
        
            else
            {

                return RedirectToAction("Indexlogout", "User");
    }
}
        [HttpPost]
        public ActionResult users(string value, int id)
        {
            if (value == "Approve")
            {
                User u = db.Users.Find(id);

                db.Entry(u).CurrentValues.SetValues(new User
                {
                    User_id = u.User_id,
                    First_Name = u.First_Name,
                    Last_Name = u.Last_Name,
                    Address = u.Address,
                    Email_ID = u.Email_ID,
                    Password = u.Password,
                    ID_Proof = u.ID_Proof,
                    Contact = u.Contact,
                    Roles = u.Roles,
                    Status = "Verified"

                });
                db.SaveChanges();
                return RedirectToAction("users", "Users");

            }
            else if (value == "Reject")
            {

                User u = db.Users.Find(id);

                db.Entry(u).CurrentValues.SetValues(new User
                {
                    User_id = u.User_id,
                    First_Name = u.First_Name,
                    Last_Name = u.Last_Name,
                    Address = u.Address,
                    Email_ID = u.Email_ID,
                    Password = u.Password,
                    ID_Proof = u.ID_Proof,
                    Contact = u.Contact,
                    Roles = u.Roles,
                    Status = "Rejected"

                });

                db.SaveChanges();
                return RedirectToAction("users", "Users");

            }

            return RedirectToAction("users", "Users");

            //Create Admin

        }
        [HttpGet]
        public ActionResult CreateAdmin() {

            if (Session["admin_id"] != null)
            {
                return View();
            }
            else {

                return RedirectToAction("Indexlogout", "User");
            }
        }
        [HttpPost]
        public ActionResult CreateAdmin(User u)
        {
           
            db.Users.Add(new User {

                User_id= u.User_id,
                First_Name = u.First_Name,
                Last_Name= u.Last_Name,
                Address = u.Address,
                Email_ID= u.Email_ID,
                Password=u.Password,
                ID_Proof=u.ID_Proof,
                Roles=1,
                Status="Verified"
            });


            return View();
        }

        public ActionResult GetAdmin() {
            if (Session["admin_id"] != null)
            {
                var admin = db.Users.Where(x => x.Roles == 1);
            return View(admin);
            }
            else
            {
                return RedirectToAction("Indexlogout", "User");
            }
        }
        public ActionResult AdminFront() {
            if (Session["admin_id"] != null)
            {
                return View();
            }
            else {
                return RedirectToAction("Indexlogout", "User");
            }
        }

      
    }
}
