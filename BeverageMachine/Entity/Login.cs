using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BeverageMachine.Entity
{
    public class Login
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
