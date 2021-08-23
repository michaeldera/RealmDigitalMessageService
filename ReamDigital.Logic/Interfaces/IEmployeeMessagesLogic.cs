using System.Threading.Tasks;

namespace RealmDigital.Logic.Interfaces
{
    public interface IEmployeeMessagesLogic
    {
        Task SendBirthdayMessagesAsync();
        Task SendWorkAnniversayMessagesAsync();
    }
}
