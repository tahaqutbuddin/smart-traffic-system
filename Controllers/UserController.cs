using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Smart_Trafic_Management_System.Models;

namespace UserPanel.Controllers
{
    public class UserController : Controller
    {
        // Database Connection ********************
        CodeTitansEntities1 db = new CodeTitansEntities1();

        // USER dashboard version1 ********************
        public ActionResult Indexlogin()
        {
            Session.Clear();
            return View();
        }
        public ActionResult Indexlogout()
        {
            if (Session["user_id"]!=null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        // USER Registration ********************
        public ActionResult Register()
        {
            return View();
        }
        // USER Registration Post Method ********************
        [HttpPost]
        public ActionResult Register(User u, string password)
        {
            db.Users.Add(new User {
                First_Name = u.First_Name,
                Last_Name = u.Last_Name,
                Address = u.Address,
                Email_ID = u.Email_ID,
                Password = u.Password,
                ID_Proof = u.ID_Proof,
                Contact = u.Contact,
                Roles = 0,
                Status = "Unverified"
            });
            db.SaveChanges();
            ViewBag.register = "Your Request for registration is under process. Wait for Account Verification";
            return View(u);
        }



        // USER Login **************

        public ActionResult Login()
        {

            if (Session["user_id"]==null)
            {
                return View();

            }
            else
            {
                return RedirectToAction("Indexlogout", "User");
            }
        }
        // USER Login  Post Method **************
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(User u)
        {
            var data = db.Users.Where(x => x.Email_ID == u.Email_ID && x.Password == u.Password).FirstOrDefault();
            if (data != null)
            {

                //User user = new User() { Email_ID = model.Email, Password = model.Password };
                var user = db.Users.Where(x => x.Email_ID == u.Email_ID && x.Password == u.Password).FirstOrDefault();
                if (data.Status == "Verified")
                {
                    FormsAuthentication.SetAuthCookie(u.First_Name, false);
                    var authTicket = new FormsAuthenticationTicket(1, user.Email_ID, DateTime.Now, DateTime.Now.AddMinutes(20), false, user.Roles.ToString());
                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    HttpContext.Response.Cookies.Add(authCookie);

                    if (user.Roles == 0)
                    {
                        Session["user_id"] = user.User_id;
                        Session["username"] = user.First_Name;
                        return RedirectToAction("Indexlogout", "User");

                    }
                    else
                    {
                        Session["admin_id"] = u.User_id;
                        Session["admin_username"] = user.First_Name;
                        return RedirectToAction("AdminFront", "Users");
                    }

                }
                else
                {
                    ViewBag.msg = "Your Request for registration is under process. Wait for Account Verification";
                    return View(u);
                }


            }
            else
            {
                ViewBag.msg = "User does not exist.Please Re-enter in case of any mistake";
                return View(u);
            }

        }
        // USER Feedback **************
        public ActionResult Feedback() {

            //ViewBag.date = String.Format("{0: MM.dd.yyyy}", DateTime.Now); 

            return View();

        }


        // USER Feedback Post Method **************

        [HttpPost]
        public ActionResult Feedback(Feedback f)
        {
            if (Session["user_id"] != null)
            {
                Feedback fed = new Feedback();
                fed.date = DateTime.Now;
                fed.User_ID = Convert.ToInt32(Session["user_id"].ToString());
                fed.Description = f.Description;
                db.Feedbacks.Add(fed);
                db.SaveChanges();
                return RedirectToAction("Indexlogout", "User");
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        // USER Emergency help **************
        public ActionResult User_Emergency()
        {
            if (Session["user_id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }



        // USER Emergency Post Method **************
        [HttpPost]
        public ActionResult User_Emergency(Emergency e)
        {


            db.Emergencies.Add(new Emergency
            {
                User_ID = Convert.ToInt32(Session["user_id"].ToString()),
                Location = e.Location
            });
            db.SaveChanges();
            return RedirectToAction("Indexlogin", "User");

        }

        // USER Signout ****************
        public ActionResult Signout()
        {
            if (Session["user_id"] != null || Session["admin_id"] != null)
            {

                Session.Clear();
                return RedirectToAction("Indexlogin", "User");
            }
            else
            {
               return RedirectToAction("Indexlogout", "User");
            }
        }
       


    }
}