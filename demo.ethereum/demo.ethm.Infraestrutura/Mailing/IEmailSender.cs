using System.Threading.Tasks;

namespace demo.ethm.Infraestrutura.Mailing
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
