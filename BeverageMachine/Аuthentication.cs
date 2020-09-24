using BeverageMachine.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace BeverageMachine
{
    public static class Аuthentication
    {
        public static string CheckАuthentication(this ApplicationContext context, IIdentity user)
        {
            string userId = null;
            if (user.IsAuthenticated)
            {
                List<UserViewModel> users = context.Users.ToList();
                userId = users.Where(x => x.UserName == user.Name).Select(y => y.Id).FirstOrDefault();
            }
            return userId;
        }
    }
}
