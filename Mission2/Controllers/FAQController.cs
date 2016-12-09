using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mission2.Controllers
{
    [Authorize]
    public class FAQController : Controller
    {
        // GET: FAQ
        public ActionResult Index(int? id)
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            return View();
        }
    }
}