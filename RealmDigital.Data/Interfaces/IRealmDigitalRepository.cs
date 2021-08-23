using MessagingComponent.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealmDigital.Data.Interfaces
{
    public interface IRealmDigitalRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<IEnumerable<int>> GetExcludedEmployeeIdsAsync();
        Task UpdateEmployeeAsync(Employee employee);
    }
}