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
        // GET: Account

        LTQLDbContext db = new LTQLDbContext();
        Encrytion ecry = new Encrytion();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Register(AccountModel acc)
        {
            if (ModelState.IsValid)
            {
                acc.Password = ecry.PasswordEncrytion(acc.Password);
                db.AccountModels.Add(acc);
                db.SaveChanges();
                return RedirectToAction("Login", "Account");
            }
            return View(acc);
        }

        [AllowAnonymous]





        public ActionResult Login(string returnUrl)
        {
            if (CheckSession() == 1)
            {
                return RedirectToAction("Index", "HomeAdmin", new { Area = "Admins" });
            }
            else if (CheckSession() == 2)
            {
                return RedirectToAction("Index", "HomeEmp", new { Area = "Employees" });
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(AccountModel acc, string returnUrl)
        {
            try
            {
                if (!string.IsNullOrEmpty(acc.Username) && !string.IsNullOrEmpty(acc.Password))
                {
                    using (var db = new LTQLDbContext())
                    {
                        var passToMD5 = ecry.PasswordEncrytion(acc.Password);
                        var account = db.AccountModels.Where(m => m.Username.Equals(acc.Username) && m.Password.Equals(passToMD5)).Count();
                        if (account == 1)
                        {
                            FormsAuthentication.SetAuthCookie(acc.Username, false);
                            Session["idUser"] = acc.Username;
                            Session["roleUser"] = acc.RoleID;
                            return RedirectToLocal(returnUrl);
                        }
                        ModelState.AddModelError("", "Thông tin đăng nhập chưa chính xác");
                    }
                }

                ModelState.AddModelError("", "Username and password is required.");
            }
            catch
            {
                ModelState.AddModelError("", "Hệ thống đang được bảo trì, vui lòng liên hệ với quản trị viên");
            }
            return View(acc);
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {

            if (string.IsNullOrEmpty(returnUrl) || returnUrl == "/")
            {
                if (CheckSession() == 1)
                {
                    return RedirectToAction("Index", "HomeAdmin", new { Area = "Admins" });
                }
                else if (CheckSession() == 2)

                {
                    return RedirectToAction("Index", "HomeEmp", new { Area = "Employees" });
                }

            }
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private int CheckSession()
        {
            using (var db = new LTQLDbContext())
            {
                var user = HttpContext.Session["idUser"];

                if (user != null)
                {
                    var role = db.AccountModels.Find(user.ToString()).RoleID;

                    if (role != null)
                    {
                        if (role.ToString() == "Admin")

                        {
                            return 1;
                        }

                        else if (role.ToString() == "nv")

                        {
                            return 2;
                        }
                    }
                }
            }
            return 0;
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}