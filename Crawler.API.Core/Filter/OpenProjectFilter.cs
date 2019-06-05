using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Mvc;
using FireSharp.Config;
using FireSharp;
using FireSharp.Response;
using Newtonsoft.Json.Linq;

namespace Crawler.API.Core.Filter
{
    public class OpenProjectFilter : ActionFilterAttribute
    {
        private IFirebaseConfig _firebaseConfig;

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            //IEnumerable<string> headerEmail;
            var emailHeader = context.HttpContext.Request.Headers["email"].SingleOrDefault();

            if (string.IsNullOrWhiteSpace(emailHeader))
                context.Result = new BadRequestObjectResult(context.ModelState);
            else
                //FireBase();

            
            base.OnResultExecuting(context);
        }

        //private async void FireBase()
        //{
        //    _firebaseConfig = new FirebaseConfig
        //    {
        //        AuthSecret = "s4slfg7z0FZB0TUmc3P1IO9gUU6PkIKqLaykYCTw",
        //        BasePath = "https://testfirebase-b5b33.firebaseio.com/",
        //    };

        //    var _client = new FirebaseClient(_firebaseConfig);
        //    var response = await _client.GetTaskAsync("accounts");

        //    var jsonObject = JObject.Parse(response.Body);

        //    Console.WriteLine();
        //}
    }
}
