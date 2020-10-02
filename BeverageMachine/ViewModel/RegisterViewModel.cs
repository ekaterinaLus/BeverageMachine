using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace BeverageMachine.ViewModel
{
    public class RegisterViewModel
    {
		[Required]
		[DataType(DataType.EmailAddress)]
		[DisplayName("Email")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Поле должно быть установлено")]
		[DataType(DataType.Password)]
		[DisplayName("Пароль")]
		public string Password { get; set; }


        [Required]
        [Compare(nameof(Password), ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [DisplayName("Повторите пароль")]
        public string PasswordCompare { get; set; }
	}
}
