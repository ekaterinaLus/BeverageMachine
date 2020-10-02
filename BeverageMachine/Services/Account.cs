using BeverageMachine.ViewModel;
using BeverageMachine.Models;
using Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BeverageMachine.Services
{
    public static class Account
    {
        public static async Task SendLetter(this ForgetPasswordViewModel model, UserManager<UserViewModel> _userManager, SignInManager<UserViewModel> _signInManager,
            IUrlHelper url, HttpContext httpContext)
        {
            var user = await _userManager.FindByNameAsync(model.Email); 
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var codeUrl = url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: httpContext.Request.Scheme);
            EmailService emailService = new EmailService();
            await emailService.SendEmailAsync(model.Email, "Letter", $"Для сброса пароля пройдите по ссылке: <a href='{codeUrl}'>link</a>");
        }

        public static async Task<IdentityResult> Registration(this RegisterViewModel model, UserManager<UserViewModel> _userManager, SignInManager<UserViewModel> _signInManager)
        {
            UserViewModel user = new UserViewModel { Email = model.Email, UserName = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
            }
            return result;
        }
    }
}
