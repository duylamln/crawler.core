using Crawler.API.Core.Interfaces;
using Crawler.API.Core.Models.Firebase;
using FireSharp.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crawler.API.Core.Services
{
    public class FirebaseAccountService : BaseFirebaseService, IFirebaseAccountService
    {
        public async Task<FirebaseAccount> GetAccountByEmail(string email)
        {
            var client = CreateClient();
            var query = QueryBuilder.New().OrderBy("email").EqualTo(email).LimitToLast(1);
            var response = await client.GetAsync("accounts", query);

            var accounts = response.ResultAs<FirebaseCollection<FirebaseAccount>>()?.ToList();
            if (accounts == null || accounts.Count == 0) return null;

            return accounts.First();
        }
    }
}
