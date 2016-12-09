using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Mission2.Models;
using Mission2.DAL;

namespace Mission2.Controllers
{
    public class HomeController : Controller
    {
        private Mission2Context db = new Mission2Context();

        public ActionResult Index()
        {
            return View();
        }

        //Actionresult will display the about view!
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        //Actionresult method will display the contact view!
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.DropDownValues = new SelectList(new[] { "I want to help Mission Accomplished :)", "I found a problem on your site :O", "I want to donate!" });
            return View();
        }

        // GET: Home
        public ActionResult Login()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Login(FormCollection form, bool rememberMe = false)
        {
            String email = form["Email Address"].ToString();
            String password1 = form["Password"].ToString();

            //if (string.Equals(email, "greg@test.com") && (string.Equals(password, "greg")))
            MissionUsers user = db.MissionsUser.SingleOrDefault(user1 => user1.UserEmail == email && user1.Password == password1);


            //Try Catch will look to see if the username and password match!
            try
            {
                if (string.Equals(email, user.UserEmail) && string.Equals(password1, user.Password))
                {
                    FormsAuthentication.SetAuthCookie(email, rememberMe);
                    Session["userID"] = user.UserID;
                    return RedirectToAction("Index", "Missions");

                }
            }
            //If the username and password don't match, display an error message: "Sorry, we don't recognize that information...."
            catch
            {
                ViewBag.error = "Sorry, we don't recognize that information.  Please log in again, or register.";
                return View();
            }


            return View();

        }

        //When the user selects "logout", the system will clear the session!
        public ActionResult Logout()
        {
            Session.Clear();
            //or Session["User"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}