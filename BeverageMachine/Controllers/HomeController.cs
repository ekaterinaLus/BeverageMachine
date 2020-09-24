using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BeverageMachine.Models;
using Microsoft.EntityFrameworkCore;
using BeverageMachine.Repository;

namespace BeverageMachine.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static ApplicationContext context = new ApplicationContext();
        private readonly IDrinkRepository drinkRepository = new DrinkRepository(context);

        public HomeController(ApplicationContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(DrinkViewModel drink)
        {
            drinkRepository.Create(drink);
            return RedirectToAction("Index");
        }

        public IActionResult SelectAllDrinks()
        {
            var drinks = drinkRepository.GetAll();
            return View(drinks);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
