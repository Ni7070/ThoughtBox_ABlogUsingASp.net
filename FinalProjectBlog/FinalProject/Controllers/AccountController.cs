using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FinalProject.Models;
using Membership = FinalProject.Models.Membership;

namespace FinalProject.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Membership model)
        {
            using (var context = new ThoughtBoxEntities())
            {
                bool isValid = context.myUsers.Any(x => x.Username == model.Username && x.Password == model.Password);
                if(isValid)
                {
                    return RedirectToAction("Create","Posts");
                }
                ModelState.AddModelError("", "Invalid username or password");
                return View();

            }
        }
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(myUser model)
        {   
            using(var context = new ThoughtBoxEntities())
            {
                context.myUsers.Add(model);
                context.SaveChanges();
            }
            return RedirectToAction("Login");
        }
    }
}