using Microsoft.AspNetCore.Http;

namespace MobileStore.Modules
{
    public class MediaRequest
    {
        public IFormFile File { get; set; }
        public int? MobilePhoneId { get; set; }
    }
}
