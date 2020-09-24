using BeverageMachine.Models;
using BeverageMachine.Repository;
using BeverageMachine.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BeverageMachine.Controllers
{
    public class PurchaseController : Controller
    {
        private static ApplicationContext _context = new ApplicationContext();
        private IDrinkRepository drinkRepository;
        private IOrderRepository orderRepository;
        private IShoppingBasketsRepository shoppingRepository;
        private IPurchasedGoodRepository purchasedRepository;

        public PurchaseController()//(ApplicationContext context)
        {
            //ApplicationContext  _context = new ApplicationContext();
            shoppingRepository = new ShoppingBasketsRepository(_context);
            drinkRepository = new DrinkRepository(_context);
            orderRepository = new OrderRepository(_context);
            purchasedRepository = new PurchasedGoodRepository(_context);
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        //[Authorize]
        public IActionResult BuyDrink()
        {
            var drinks = drinkRepository.GetAll();
            return View(drinks);
        }

        [HttpPost]
        public async Task<IActionResult> BuyDrink(string elem)
        {
            //await context.AddToCart(elem);
            var drinks = drinkRepository.GetAll();
            return View(drinks);
        }

        [HttpGet]
        [Authorize]
        public IActionResult SelectShopping()
        {
            var drinks = drinkRepository.GetAll();
            if (drinks != null)
            {
                return View(drinks);
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AddToCart()
        {
            var drinks = drinkRepository.GetAll();
            return View(drinks);
        }
        [HttpPost]
        public string Index(string[] countries)
        {
            string result = "";
            foreach (string c in countries)
            {
                result += c;
                result += ";";
            }
            return "Вы выбрали: " + result;
        }

        [HttpPost]
        public async Task AddToCart(int drinkId)
        {
            var drink = drinkRepository.Get(drinkId);
            _context.AddDrink(drink, 1);
            //context.Add(el);
            //await context.SaveChangesAsync();
        }

        [HttpPost]
        public decimal Amount(int id)
        {
            var baskets = shoppingRepository.Get(id);
            return baskets.Goods.Summa();
        }

        [HttpGet]
        public IActionResult SelectAllProducts()
        {
            string userId = _context.CheckАuthentication(User.Identity);
            return View(_context.GetGoods(userId));
        }

        //[HttpPost]
        //public IActionResult SelectAllProducts()
        //{
        //    string userId = Аuthentication.CheckАuthentication(User.Identity);
        //    return View(context.GetGoods(userId));
        //}

        [HttpGet]
        public IActionResult Check(int id)
        {
            var baskets = shoppingRepository.Get(id);
            decimal sum = baskets.Goods.Summa();
            OrderViewModel order = _context.Orders
                .Where(x => x.Basket.Id == baskets.Id).FirstOrDefault();
            ViewBag.Sum = sum;
            ViewBag.Basket = baskets;
            ViewBag.Order = order.Id;
            _context.AddOrder(baskets, sum);
            return View();
        }
        [HttpPost]
        public IActionResult Check(decimal money, int id)
        {
            OrderViewModel order = orderRepository.Get(id);
            ViewBag.Change = order.GetChange(money);
            string userId = _context.CheckАuthentication(User.Identity);
            return View();
        }
    }
}
