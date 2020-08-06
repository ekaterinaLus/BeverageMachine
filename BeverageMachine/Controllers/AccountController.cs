using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using BeverageMachine.Models;
using BeverageMachine.ViewModel;
using Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace BeverageMachine.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
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
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Неправильный логин или пароль");
            }          
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                User user = new User { Email = model.Email, UserName = model.Email };

                var result = await _userManager.CreateAsync(user, model.Password);
                if(result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }                
            }
            return View();
        }

        public async Task SendEmailAsync(string email)
        {
            EmailService emailService = new EmailService();
            await emailService.SendEmailAsync(email, "Тема письма", "Тест письма: тест!"); ;
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
                //Проверить что будет, если юзеров с одинаковым имейлом несколько
                var user = await _userManager.FindByNameAsync(model.Email); //Users.FirstOrDefault(x => x.Email == model.Email);  
                if (user == null)
                {
                    return View(model);
                }
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var codeUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                EmailService emailService = new EmailService();
                await emailService.SendEmailAsync(model.Email, "Letter", $"Для сброса пароля пройдите по ссылке: <a href='{codeUrl}'>link</a>");
                return View("ForgetPasswordMessage");
                //return RedirectToAction("Index", "Home");
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
