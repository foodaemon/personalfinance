using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Service;
using Core.Models;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
		private readonly IAccountService _accountService;

		public AccountController()
		{
			_accountService = new AccountService ();
		}

		[HttpGet]
        public ActionResult Index()
        {
            return View ();
        }

		[HttpGet]
        public ActionResult Details(int id)
        {
            return View ();
        }

		[HttpGet]
        public ActionResult Create()
        {
            return View ();
        } 

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try {
                return RedirectToAction ("Index");
            } catch {
                return View ();
            }
        }
        
		[HttpGet]
        public ActionResult Edit(int id)
        {
            return View ();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try {
                return RedirectToAction ("Index");
            } catch {
                return View ();
            }
        }

		[HttpGet]
        public ActionResult Delete(int id)
        {
            return View ();
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try {
                return RedirectToAction ("Index");
            } catch {
                return View ();
            }
        }


		/// <summary>
		/// GET /account/signin
		/// </summary>
		[HttpGet]
		[AllowAnonymous]
		public ActionResult Signin()
		{
			return View ("Signin");
		}
			
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public ActionResult Signin(FormCollection collection)
		{
			var email = collection ["email"];
			var password = collection ["password"];
			var createPersistentCookie = collection ["remember_me"];

			var isValidAccount = _accountService.ValidateAccount (email, password);
			if (isValidAccount == false)
				return RedirectToAction ("Signin", "Account");

			var now = DateTime.UtcNow.ToLocalTime ();
			var expiry = now.AddMinutes (30);

			var ticket = new FormsAuthenticationTicket (
				version: 1, 
				name: email, 
				issueDate: now, 
				expiration: expiry, 
				isPersistent: true, 
				userData: email, 
				cookiePath: FormsAuthentication.FormsCookiePath);

			var encryptedTicket = FormsAuthentication.Encrypt (ticket);
			var cookie = new HttpCookie (FormsAuthentication.FormsCookieName, encryptedTicket);
			cookie.HttpOnly = true;

			if (ticket.IsPersistent)
				cookie.Expires = ticket.Expiration;

			Response.Cookies.Add (cookie);

			return RedirectToAction ("Index", "Home");
		}

		[HttpGet]
		public ActionResult Signout()
		{
			FormsAuthentication.SignOut ();
			return RedirectToAction ("Signin", "Account");
		}
    } // class
} // namespace