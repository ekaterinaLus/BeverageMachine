using BeverageMachine.Models;
using BeverageMachine.Repository;
using BeverageMachine.Services;
using BeverageMachine.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BeverageMachine.Controllers
{
    //calling the appropriate methods for calculating the amount of goods, the ability to add an item to the cart, view all items in the cart
    public class PurchaseController : Controller
    {
        private static ApplicationContext _context ;
        private IDrinkRepository _drinkRepository;
        private IOrderRepository _orderRepository;
        private IShoppingBasketsRepository _shoppingRepository;
        private IPurchasedGoodRepository _purchasedRepository;

        public PurchaseController(ApplicationContext context)
        {
            _context = context;
            _shoppingRepository = new ShoppingBasketsRepository(_context);
            _drinkRepository = new DrinkRepository(_context);
            _orderRepository = new OrderRepository(_context);
            _purchasedRepository = new PurchasedGoodRepository(_context);
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public IActionResult BuyDrink()
        {
            var drinks = _drinkRepository.GetAll();
            return View(drinks);
        }

        [HttpGet]
        [Authorize]
        public IActionResult SelectShopping()
        {
            var drinks = _drinkRepository.GetAll();
            if (drinks != null)
            {
                return View(drinks);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int drinkId, int quantity)
        {
            string userId = _context.CheckАuthentication(User.Identity);
            var drink = _drinkRepository.Get(drinkId);
            _context.AddDrink(drink, quantity, userId);
            return Redirect("/Purchase/BuyDrink");
        }

        [HttpPost]
        public decimal Amount(int id)
        {
            var baskets = _shoppingRepository.Get(id);
            return baskets.Goods.GetTotal();
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

            ShoppingBasket basket = _context.GetBasket(User.Identity.Name);
            decimal sum = basket.Goods.GetTotal();
            _context.AddOrder(basket, sum);
            Order order = _context.Orders
                .Where(x => x.Basket.Id == basket.Id).FirstOrDefault();
            ViewBag.Sum = sum;
            ViewBag.Basket = basket;
            ViewBag.Order = order.Id;
            return View();
        }
        [HttpPost]
        public IActionResult Check(decimal money, int id)
        {
            Order order = _orderRepository.Get(id);
            ViewBag.Change = order.GetChange(money);
            string userId = _context.CheckАuthentication(User.Identity);
            return View();
        }
    }
}
