using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC5Course.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        [AllowAnonymous]
        // GET: Member
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel login)
        {

            if (CheckLogin(login.Email, login.Password))
            {
                FormsAuthentication.RedirectFromLoginPage(login.Email, login.RememberMe ? true : false);
                return  RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("Password", "帳號或密碼輸入錯誤！");
            return View();
        }

        private bool CheckLogin(string email, string password)
        {
            return (
                    email == "kevin@simsix.com" &&
                    password == "801217"
                   );
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Member");
        }

    }
}