using MobileStore.Models;
using System.Threading.Tasks;

namespace MobileStore.Modules
{
    public interface IMediaService
    {
        Task<Media> Create(MediaRequest request);
        Task<Media> Delete(int id);
        Task<Media> GetById(int id);
    }
}
