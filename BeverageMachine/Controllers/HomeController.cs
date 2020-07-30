using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BeverageMachine.Models;
using Microsoft.EntityFrameworkCore;

namespace BeverageMachine.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        private ApplicationContext context;
        public HomeController(ApplicationContext _context)
        {
            context = _context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await context.Drinks.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DrinkModel drink)
        {
            //List<obj> listOb
            // await context.Drinks.AddUniqueElementAsync(drink);
            context.Add(drink);

            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public IActionResult SelectAllDrinks()
        {
            List<DrinkModel> drinks = context.Drinks.AsNoTracking().ToList();
            return View(drinks);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
