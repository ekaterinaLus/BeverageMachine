using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeverageMachine.ViewModel
{
    public class ForgetPasswordViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Введите Email")]
        public string Email { get; set; }
    }
}
