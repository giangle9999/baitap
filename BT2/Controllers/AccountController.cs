using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BT2.Models;

namespace BT2.Controllers
{
    public class AccountController : Controller
    {
        Encrytion encry = new Encrytion();
        LTQLEntities db = new LTQLEntities();

        // GET: Account
       [HttpGet]
       public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(Account Acc)
        {
            if (ModelState.IsValid)
            {
                string encrytionpass = encry.PasswordEncrytion(Acc.Password);
                var model = db.Accounts.Where(m => m.UserName == Acc.UserName && m.Password == encrytionpass).Tolist().Count();
                if (model == 1)
                {
                    FormsAuthentication.SetAuthCookie(Acc.UserName, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Thông tin đăng nhập không chính xác");

                }    
                   

            }
            return View(Acc);
            
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");

        }
            
            
    }

}