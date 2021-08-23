using RealmDigitial.Libraries.Messaging.Interfaces;
using RealmDigitial.Libraries.Messaging.Models;
using System;
using System.Threading.Tasks;

namespace RealmDigitial.Libraries.Messaging.Core
{
    public class MessagingHandler : IMessagingHandler
    {
        public Task<MessagingResult> SendEmailAsync(string email, string subject, string body)
        {
            // Library that sends Emails here.
            throw new NotImplementedException();
        }
    }
}
