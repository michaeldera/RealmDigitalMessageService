using RealmDigitial.Libraries.Messaging.Models;
using System.Threading.Tasks;

namespace RealmDigitial.Libraries.Messaging.Interfaces
{
    public interface IMessagingHandler
    {
        Task<MessagingResult> SendEmailAsync(string subject, string body, string email);
    }
}
