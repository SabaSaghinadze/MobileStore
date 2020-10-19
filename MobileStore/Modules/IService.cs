using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileStore.Modules
{
    public interface IService<TRequest, TResponse>
    {
        Task<IEnumerable<TResponse>> Get();
        Task<TResponse> GetById(int id);
        Task<TResponse> Create(TRequest request);
        Task<TResponse> Update(int id, TRequest request);
        Task<TResponse> Delete(int id);
    }
}
