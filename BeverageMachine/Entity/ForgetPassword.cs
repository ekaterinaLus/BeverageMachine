using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BeverageMachine.Entity
{
    public class ForgetPassword
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Введите Email")]
        public string Email { get; set; }
    }
}
