using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using BeverageMachine.Models;
using BeverageMachine.ViewModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace BeverageMachine.Controllers
{
    public class ShoppingController : Controller
    {
        private readonly ApplicationContext _context = new ApplicationContext();

    }
}
