using System.Threading.Tasks;

namespace Helper
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}