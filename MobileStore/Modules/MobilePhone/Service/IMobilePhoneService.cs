using MobileStore.Models;
using System.Threading.Tasks;

namespace MobileStore.Modules
{
    public interface IMobilePhoneService : IService<MobilePhoneRequest, MobilePhone>
    {
        Task<MobilePhone> GetByName(string name);
    }
}
