using Crawler.API.Core.Interfaces;
using Crawler.API.Core.Models.Firebase;
using FireSharp.Core;
using System.Linq;
using System.Threading.Tasks;

namespace Crawler.API.Core.Services
{
    public class FirebaseUserInfoService : BaseFirebaseService, IFirebaseUserInfoService
    {
        public async Task<FirebaseUserInfo> GetByEmail(string email)
        {
            var client = CreateClient();
            var query = QueryBuilder.New().OrderBy("email").EqualTo(email).LimitToLast(1);
            var response = await client.GetAsync("userInfos", query);

            var userInfos = response.ResultAs<FirebaseCollection<FirebaseUserInfo>>()?.ToList();
            if (userInfos == null || userInfos.Count == 0) return null;

            return userInfos.First();
        }

        public async Task<FirebaseUserInfo> GetByUId(string uid)
        {
            var client = CreateClient();
            var query = QueryBuilder.New().OrderBy("uid").EqualTo(uid).LimitToLast(1);
            var response = await client.GetAsync("userInfos", query);

            var userInfos = response.ResultAs<FirebaseCollection<FirebaseUserInfo>>()?.ToList();
            if (userInfos == null || userInfos.Count == 0) return null;

            return userInfos.First();
        }
    }
}
