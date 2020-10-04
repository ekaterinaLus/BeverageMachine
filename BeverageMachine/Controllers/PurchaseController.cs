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

        [HttpGet]
        [Authorize(Roles = "User")]
        public IActionResult BuyDrink()
        {
            var drinks = drinkRepository.GetAll();
            return View(drinks);
        }

        //[HttpPost]
        //public async Task<IActionResult> BuyDrink(string elem)
        //{
        //    var drinks = drinkRepository.GetAll();
        //    return View(drinks);
        //}

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

        [HttpPost]
        public async Task<IActionResult> AddToCart(int drinkId)
        {
            string userId = _context.CheckАuthentication(User.Identity);
            var drink = drinkRepository.Get(drinkId);
            _context.AddDrink(drink, 1, userId);
            return Redirect("/Purchase/BuyDrink");
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

        [HttpGet]
        public IActionResult Check()
        {

            ShoppingBasketViewModel basket = _context.GetBasket(User.Identity.Name);
            decimal sum = basket.Goods.Summa();
            _context.AddOrder(basket, sum);
            OrderViewModel order = _context.Orders
                .Where(x => x.Basket.Id == basket.Id).FirstOrDefault();
            ViewBag.Sum = sum;
            ViewBag.Basket = basket;
            ViewBag.Order = order.Id;
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
