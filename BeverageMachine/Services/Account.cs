using BeverageMachine.Models;
using Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;
using BeverageMachine.Entity;

namespace BeverageMachine.Services
{
    public static class Account
    {
        public static async Task SendLetter(this ForgetPassword model, UserManager<User> _userManager, SignInManager<User> _signInManager,
            IUrlHelper url, HttpContext httpContext) //TO DO: перенести этот метод
        {
            var user = await _userManager.FindByNameAsync(model.Email); 
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var codeUrl = url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: httpContext.Request.Scheme);
            EmailService emailService = new EmailService();
            await emailService.SendEmailAsync(model.Email, "Letter", $"Для сброса пароля пройдите по ссылке: <a href='{codeUrl}'>link</a>");
        }

        public static async Task Registration(this Register model, ApplicationContext context, IServiceProvider provider)
        { 
            var userId = await SeedData.EnsureUserCreated(context, provider, model.Password, model.Email);
            await SeedData.EnsureRoleCreated(context, provider, userId, ApplicationContext.RoleName.User.ToString());
        }
    }
}
