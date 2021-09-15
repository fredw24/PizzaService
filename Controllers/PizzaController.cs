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
    public class PizzaController : Controller
    {
        private readonly ILogger<PizzaController> _logger;
        private  MyContext dbContext;
        public PizzaController(ILogger<PizzaController> logger, MyContext context)
        {
            _logger = logger;
            dbContext = context;
        }
        public IActionResult Index()
        {
            string? saved = HttpContext.Session.GetString("saved");
            if (saved =="Confirm")
            {
                ViewBag.saved = true;
                HttpContext.Session.SetString("saved", "blank");

            }
            else
            {
                ViewBag.saved = false;
            }

            int? id = HttpContext.Session.GetInt32("id");
            if (id != null)
            {
                
                ViewBag.User = dbContext.User.FirstOrDefault(d => d.UserId == id);

                ViewBag.Pizzas = dbContext.Pizza.Where( p => p.UserId == id)
                                                .OrderBy(d => d.OrderedAt);

                return View();
            }

            return RedirectToAction("Index", "Home");
        }
        
        [HttpGet("Pizza/Order")]
        public IActionResult Create()
        {
            
            return View();
        }

        public IActionResult CreatePizza(Pizza pizza)
        {
            
            Console.WriteLine(pizza.Price);
            pizza.UserId = (int)HttpContext.Session.GetInt32("id");
            if (ModelState.IsValid)
            {
                dbContext.Add(pizza);
                dbContext.SaveChanges();
                HttpContext.Session.SetString("saved", "Confirm");
                return RedirectToAction("Index");
            }
            return View("Create");
        }
        [HttpGet("DetailPizza/{id}")]
        public IActionResult DetailPizza(int id)
        {
            
            ViewBag.Pizza = dbContext.Pizza.FirstOrDefault(p => p.PizzaId == id);
            ViewBag.Pizza.Price = Math.Round(ViewBag.Pizza.Price,2);
            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
        }

    }
}