using Crawler.API.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Crawler.API.Core.Filter
{
    public class OpenProjectFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var emailHeader = context.HttpContext.Request.Headers["email"].SingleOrDefault();

            if (string.IsNullOrWhiteSpace(emailHeader))
                context.Result = new UnauthorizedResult();
            else
            {
                var firebaseAccountService = context.HttpContext.RequestServices.GetService<IFirebaseAccountService>();
                var accountInfo = firebaseAccountService.GetAccountByEmail(emailHeader).Result;
                if (accountInfo == null || string.IsNullOrWhiteSpace(accountInfo.OpenProjectAPIKey)) context.Result = new UnauthorizedResult();
                else
                {
                    context.HttpContext.Request.Headers.Add("openProjectAPIKey", accountInfo.OpenProjectAPIKey);
                }
            }

            base.OnActionExecuting(context);
        }
    }
}