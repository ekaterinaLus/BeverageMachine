using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeverageMachine.ViewModel
{
    public class RegisterViewModel
    {
			[RequiredAttribute]
			[DataType(DataType.EmailAddress)]
			[DisplayName("Email")]
			public string Email { get; set; }

			[RequiredAttribute]
			//[DataType(DataType.Password)]
			[DisplayName("Пароль")]
			public string Password { get; set; }

			[RequiredAttribute]
			//[DataType(DataType.Password)]
			[DisplayName("Повторите пароль")]
			[Compare("Password", ErrorMessage = "Пароли не совпадают")]
			public string PasswordCompare { get; set; }
	}
}
