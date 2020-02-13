using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using CarPoolApplication.Concerns;
using CarPoolApplication.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarPoolApplication.Controllers
{
    public class AccountController : Controller
    {

        private readonly IAccountService _repos;
        public AccountController(IAccountService repos)
        {
            _repos = repos;
        }
        //[HttpGet]
        //public ActionResult Login()
        //{

        //}
        

        //[HttpPost]
        //public ActionResult Login(Login login)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(login);
        //    }
        //    else
        //    {
        //        if(login.Username=="AMAN" && login.Password == "JAIN")
        //        {
        //            Session["UserID"] = Guid.NewGuid();
        //            return RedirectToAction("GetAll", "Driver");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Invalid");
        //            return View(login);
        //        }
        //    }
        //}
    }
}
