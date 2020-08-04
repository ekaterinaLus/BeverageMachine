using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeverageMachine.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Имя")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Пароль")]
        public string Password { get; set; }
        [DisplayName("Запомнить?")]
        public bool Remember { get; set; }
        
        public string ReturnUrl { get; set; }
    }
}
