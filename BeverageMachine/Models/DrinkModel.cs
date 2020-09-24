using BeverageMachine.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeverageMachine.Models
{
    public class DrinkModel: GenericModel<DrinkRepository, DrinkViewModel>
    {
        private readonly DrinkRepository drinkRepository;
        public DrinkModel(ApplicationContext context)
        {
            drinkRepository = new DrinkRepository(context);
        }
    }
}
