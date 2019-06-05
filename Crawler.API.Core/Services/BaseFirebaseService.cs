using FireSharp.Core;
using FireSharp.Core.Config;
using FireSharp.Core.Interfaces;

namespace Crawler.API.Core.Services
{
    public class BaseFirebaseService
    {
        protected IFirebaseClient CreateClient()
        {
            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = "GMOaOxZPVCGHMmzxDP73w6OApbhgpUBN4EoiL5GN",
                BasePath = "https://t2ptimesheetmanagement.firebaseio.com/"
                //AuthSecret = "s4slfg7z0FZB0TUmc3P1IO9gUU6PkIKqLaykYCTw",
                //BasePath = "https://testfirebase-b5b33.firebaseio.com/"
            };

            return new FirebaseClient(config);
        }
    }
}