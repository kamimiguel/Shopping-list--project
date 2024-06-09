using Microsoft.AspNetCore.Mvc;
using ShoppingList.Data;
using ShoppingList.Models;
//using System.Web;
//using System.Web.Mvc;
using System.Linq;
//using Microsoft.AspNetCore.Http;
using System.Configuration;
using Microsoft.AspNetCore.Authentication;

namespace ShoppingList.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            using (AccountDbContext db = new AccountDbContext())
            {
                return View(db.userAccount.ToList());
            }
          
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(UserAccount account)
        {
            if (!ModelState.IsValid) 
            {
                using (AccountDbContext db = new AccountDbContext())
                {
                    db.userAccount.Add(account);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = account.FirstName + " " + account.LastName + "Successfully Resgistered.";
                return RedirectToAction("Login", "Account");
            }
            return RedirectToAction("Register", "Account");
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserAccount user)
        {

            using (AccountDbContext db = new AccountDbContext())
            {
                var usr = db.userAccount.FirstOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);
                if (usr != null)
                {
                    HttpContext.Session.SetString("UserId", usr.UserId.ToString());
                    HttpContext.Session.SetString("UserName", usr.UserName.ToString());
                    HttpContext.Session.SetInt32("UserId", usr.UserId);
                    return RedirectToAction("Index", "Item");
                }
                else
                {
                    ModelState.AddModelError("", "Username or Password is wrong");
                }

                return View();
            }
        }
        public ActionResult LoggedIn()
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
      //   [HttpPost]
       // public IActionResult Logout()
       // {
       //     // Remove the user's ID from the session
       //     HttpContext.SignOutAsync();
       //     return RedirectToAction("Login", "Account");
      //  }
    }
}
