using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeverageMachine.Models;
using BeverageMachine.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeverageMachine.Controllers
{
    public class ProductController : Controller
    {
        private readonly IDrinkRepository _drinkRepository;
        private readonly ApplicationContext _context;

        public ProductController(ApplicationContext context)
        {
            _context = context;
            _drinkRepository = new DrinkRepository(_context);
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(DrinkViewModel drink)
        {
            _drinkRepository.Create(drink);
            await _context.SaveChangesAsync();
            return Redirect("/");
        }

        [HttpGet]
        public IActionResult SelectAllDrinks()
        {
            var drinks = _drinkRepository.GetAll();
            return View(drinks);
        }
    }
}
