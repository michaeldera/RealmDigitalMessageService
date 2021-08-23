using MessagingComponent.Models;
using Microsoft.Extensions.Configuration;
using RealmDigital.Data.Interfaces;
using RealmDigitial.Libraries.Messaging.Interfaces;
using RealmDigitial.Libraries.Messaging.Models;
using RealmDigital.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealmDigital.Logic.Core
{
    public class EmployeeMessagesLogic : IEmployeeMessagesLogic
    {
        private readonly IRealmDigitalRepository _repository;
        private readonly IMessagingHandler _messaging;
        private readonly string _emailAddress;

        public EmployeeMessagesLogic(IConfiguration config, IRealmDigitalRepository repository, IMessagingHandler messaging)
        {
            _repository = repository;
            _messaging = messaging;
            _emailAddress = config["EmailAddress"];
        }

        /// <summary>
        /// Send work anniversary messages
        /// </summary>
        /// <returns></returns>
        public async Task SendBirthdayMessagesAsync()
        {
            var eligibleEmployees = await FilterEligibleEmployees();
            
            // Filter messages that have already been sent
            var recepients = eligibleEmployees.Where(employee => employee.LastNotification < DateTime.Now.Date);

            // Returns task of messaging results
            recepients.Select(recepient => SendBirthdayMessageAsync(recepient));
        }

        private async Task<MessagingResult> SendBirthdayMessageAsync(Employee recepient)
        {
            var subject = "Happy Birthday!";

            var message = $"Happy Birthday {recepient.Name}, {recepient.Lastname}";

            var result = await _messaging.SendEmailAsync(subject, message, _emailAddress);

            if (result.Success) {

                recepient.LastNotification = result.TimeSent.HasValue ? result.TimeSent.Value.DateTime : DateTime.Now ;

                UpdateLastNotificationAsync(recepient);
            }

            return result;
        }


        /// <summary>
        /// Update the last time a employeee received a notification
        /// </summary>
        /// <param name="recepient">The employee that just received a notification</param>
        private async void UpdateLastNotificationAsync(Employee recepient)
        {
            await _repository.UpdateEmployeeAsync(recepient);
        }


        /// <summary>
        /// Filters out employees who are supposed to be excluded and employees that are no longer employed by the company
        /// </summary>
        /// <returns>A list of employees who are still eligible to eligible to recieve messages</returns>
        public async Task<IEnumerable<Employee>> FilterEligibleEmployees()
        {   
            try
            {
                var employees = await _repository.GetAllEmployeesAsync();

                var excludedEmployeeeIds = await _repository.GetExcludedEmployeeIdsAsync();

                var eligibleEmployees = employees.Where(employee => !excludedEmployeeeIds.Contains(employee.Id) && employee.EmploymentEndDate > DateTimeOffset.Now);

                return eligibleEmployees;

            } catch
            {
                throw;
            }
        }

        /// <summary>
        /// Send Work anniversary message
        /// </summary>
        /// <returns></returns>
        public Task SendWorkAnniversayMessagesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
