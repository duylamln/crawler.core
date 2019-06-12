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
            var userUid= context.HttpContext.Request.Headers["uid"].SingleOrDefault();

            if (string.IsNullOrWhiteSpace(userUid))
                context.Result = new UnauthorizedResult();
            else
            {
                var firebaseAccountService = context.HttpContext.RequestServices.GetService<IFirebaseUserInfoService>();
                var userInfo = firebaseAccountService.GetByUId(userUid).Result;
                if (userInfo == null || string.IsNullOrWhiteSpace(userInfo.OpenProjectAPIKey)) context.Result = new UnauthorizedResult();
                else
                {
                    context.HttpContext.Request.Headers.Add("openProjectAPIKey", userInfo.OpenProjectAPIKey);
                }
            }

            base.OnActionExecuting(context);
        }
    }
}