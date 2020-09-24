using BeverageMachine.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SeedData
{
    public class DataService : IDataService
    {
        private readonly RoleManager<Role> _roleManager;

        public DataService(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public void InitializeRole()
        {
            List<Role> roles = new List<Role>(new Role("2", "user"), );
            roles.Add(new Role("2", "user"));
            Role role = new Role("2", "user");
            roleManager.CreateAsync(role);
        }

    }
}
