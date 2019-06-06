using Crawler.API.Core.Models.Firebase;
using System.Threading.Tasks;

namespace Crawler.API.Core.Interfaces
{
    public interface IFirebaseAccountService
    {
        Task<FirebaseAccount> GetAccountByEmail(string email);
    }
}
