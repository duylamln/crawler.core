using Crawler.API.Core.Models.Firebase;
using System.Threading.Tasks;

namespace Crawler.API.Core.Interfaces
{
    public interface IFirebaseUserInfoService
    {
        Task<FirebaseUserInfo> GetByUId(string uid);
        Task<FirebaseUserInfo> GetByEmail(string email);
    }
}
