using BeverageMachine.Models;
using BeverageMachine.Services;
using BeverageMachine.ViewModel;
using Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;
using System.Threading.Tasks;

namespace BeverageMachine.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<UserViewModel> _userManager;
        private readonly SignInManager<UserViewModel> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly ApplicationContext _context;

        public AccountController(UserManager<UserViewModel> userManager, SignInManager<UserViewModel> signInManager, //RoleManager<Role> roleManager,
            IEmailService emailService, ApplicationContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            //_roleManager = roleManager;
            _emailService = emailService;
            _context = context;
        }

        [HttpGet]
        public IActionResult Login(string stringUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = stringUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.Remember, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return Redirect("/");//return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Неправильный логин или пароль");
            }          
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
 
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        { 
            if(ModelState.IsValid)
            {
                var result = await model.Registration(_userManager, _signInManager);
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
        public async Task SendEmailAsync(string email)
        {
            await _emailService.SendEmailAsync(email, "Тема письма", "Тест письма: тест!"); 
            RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                await model.SendLetter(_userManager, _signInManager, Url, HttpContext);
                return View("ForgetPasswordMessage");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            if (ModelState.IsValid && user != null)
            {
               await  _userManager.ResetPasswordAsync(user, model.Code, model.Password);
               return View("ResetPasswordMessage");
            }
            return View(model);
        }
    }
}
