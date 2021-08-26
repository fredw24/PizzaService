using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PizzaService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace PizzaService.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private  MyContext dbContext;
        public HomeController(ILogger<HomeController> logger, MyContext context)
        {
            _logger = logger;
            dbContext = context;
        }

        public IActionResult Index()
        {
            int? id = HttpContext.Session.GetInt32("id");
            if (id != null){
                ViewBag.Confirm = true;
            }
            else{
                ViewBag.Confirm = false;
            }
            return View();
        }
        [HttpGet("LoginAndReg")]
        public IActionResult LoginAndReg()
        {
            return View();
        }

        [HttpPost("NewUser")]
        public IActionResult NewUser(User user)
        {
            if(ModelState.IsValid)
            {
                dbContext.createUser(HttpContext, user);
                return RedirectToAction("Index", "Pizza");
            }
            else
            {

                return View("LoginAndReg");
            }

        }

        [HttpPost("LoginValid")]
        public IActionResult LoginUser(Login login)
        {
            if(ModelState.IsValid)
            {
                 // If inital ModelState is valid, query for a user with provided email
                var userInDb = dbContext.User.FirstOrDefault(u => u.Email == login.Email);
                // If no user exists with provided email
                if(userInDb == null)
                {
                    // Add an error to ModelState and return to View!
                    ModelState.AddModelError("Email", "Invalid Email/Password");
                    return View("LoginAndReg");
                }
                
                // Initialize hasher object
                var hasher = new PasswordHasher<Login>();
                
                // verify provided password against hash stored in db
                var result = hasher.VerifyHashedPassword(login, userInDb.Password, login.Password);
                
                // result can be compared to 0 for failure
                if(result == 0)
                {
                    // handle failure (this should be similar to how "existing email" is handled)
                    ModelState.AddModelError("Password", "Invalid Password");
                    return View("LoginAndReg");
                }
                HttpContext.Session.SetInt32("id", userInDb.UserId);
                return RedirectToAction("Index", "Pizza");
            }
            return View("LoginAndReg");
    
        
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
