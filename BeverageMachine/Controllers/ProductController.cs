using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeverageMachine.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeverageMachine.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationContext context;
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task Create(DrinkViewModel drink)
        {
            context.Add(drink);
            await context.SaveChangesAsync();
        }

    }
}
