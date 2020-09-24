using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeverageMachine.Models
{
    public class UserViewModel : IdentityUser
    {
        public override string Email { get; set; }
    }
}
