using FireSharp.Core;
using FireSharp.Core.Config;
using FireSharp.Core.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace Crawler.API.Core.Filter
{
    public class OpenProjectFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var emailHeader = context.HttpContext.Request.Headers["email"].SingleOrDefault();
            if (string.IsNullOrWhiteSpace(emailHeader))
            {
            }

            var firebaseConfig = new FirebaseConfig
            {
                AuthSecret = "",
                BasePath = "",
            };

            IFirebaseClient firebaseClient = new FirebaseClient(firebaseConfig);
        }
    }
}