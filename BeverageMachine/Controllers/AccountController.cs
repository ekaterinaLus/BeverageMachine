using BeverageMachine.Entity;
using BeverageMachine.Models;
using BeverageMachine.Services;
using Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BeverageMachine.Controllers
{
    //calling the appropriate methods for registration, user login/logout, password reset, email messages to create a new password.
    public class AccountController: Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly ApplicationContext _context;
        private readonly IServiceProvider _provider;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, //RoleManager<Role> roleManager,
            IEmailService emailService, ApplicationContext context, IServiceProvider provider)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            //_roleManager = roleManager;
            _emailService = emailService;
            _context = context;
            _provider =provider;
        }

        [HttpGet]
        public IActionResult Login(string stringUrl = null)
        {
            return View(new Login { ReturnUrl = stringUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Login(Login model)
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
                        return Redirect("/");
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
        public async Task<IActionResult> Register(Register model)
        {
            if (ModelState.IsValid)
            {
                await model.Registration(_context, _provider);
                return Redirect("/");
            }
            else {
                return View(model); 
            }
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
        public async Task<IActionResult> ForgetPassword(ForgetPassword model)
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
        public async Task<IActionResult> ResetPassword(ResetPassword model)
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
