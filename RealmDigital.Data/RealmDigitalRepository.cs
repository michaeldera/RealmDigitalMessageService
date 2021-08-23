using MessagingComponent.Models;
using Microsoft.Extensions.Configuration;
using RealmDigital.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RealmDigital.Data
{
    public class RealmDigitalRepository : IRealmDigitalRepository
    {
        private readonly string _endpoint;
        private readonly HttpClient _httpClient;

        public RealmDigitalRepository(IConfiguration configuration)
        {
            _endpoint = configuration["ApiEndpoint"];
            _httpClient = new HttpClient();
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            try
            {
                var json = JsonSerializer.Serialize(employee);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"{_endpoint}/employees/{employee.Id}", content);
            } catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"{ _endpoint}/employees");

                var employees = JsonSerializer.Deserialize<IEnumerable<Employee>>(response);


                if (employees == null)
                {
                    throw new Exception("List of Employees is null");
                }

                return employees;
            } catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<int>> GetExcludedEmployeeIdsAsync()
        {
            var response = await _httpClient.GetStringAsync($"{ _endpoint}/do-not-send-birthday-wishes");
            
            var excludedEmployeeIds = JsonSerializer.Deserialize<IEnumerable<int>>(response);

            if (excludedEmployeeIds == null)
            {
                throw new Exception("List of Employees is null");
            }

            return excludedEmployeeIds;
        }
    }
}
