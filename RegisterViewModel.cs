using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel.DataAnnotations;


public class RegisterViewModel
{
	[RequiredAttribute]
	[DisplayName("Логин")]
	public string Login { get; set; }

	[RequiredAttribute]
	[DisplayName("Email")]
	public string Email { get; set; }

	[RequiredAttribute]
	[DataType(DataType.Password)]
	[DisplayName("Пароль")]
	public string Password { get; set; }

	[RequiredAttribute]
	[DataType(DataType.Password)]
	[DisplayName("Повторите пароль")]
	[Compare("Password", ErrorMessage = "Пароли не совпадают")]
	public string PasswordCompare { get; set; }
}
