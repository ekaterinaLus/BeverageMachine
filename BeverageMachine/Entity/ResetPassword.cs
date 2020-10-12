using System.ComponentModel.DataAnnotations;

namespace BeverageMachine.Entity
{
    public class ResetPassword
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "пароли не совпадают")]
        public string PasswordCompare { get; set; }
        public string Code { get; set; }
    }
}
