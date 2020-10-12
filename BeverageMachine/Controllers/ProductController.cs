using BeverageMachine.Entity;
using BeverageMachine.Models;
using BeverageMachine.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BeverageMachine.Controllers
{
    //calling the appropriate methods for adding a drink to the database (by the administrator), selecting drinks from the database
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
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create(Drink drink)
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
