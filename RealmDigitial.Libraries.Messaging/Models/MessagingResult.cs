using System;

namespace RealmDigitial.Libraries.Messaging.Models
{
    public class MessagingResult
    {
        public bool Success { get; set; }
        public DateTimeOffset? TimeSent {  get; set; }
    }
}
