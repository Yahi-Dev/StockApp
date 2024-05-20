using StockApp.Core.Application.ViewModels.Users;
using Microsoft.AspNetCore.Http;
using StockApp.Core.Application.Helpers;
using StockApp.Core.Application.Dtos.Account;

namespace StockApp.Middlewares
{
    public class ValidateUserSession
    {
        private readonly IHttpContextAccessor httpContext;


        public ValidateUserSession(IHttpContextAccessor httpContextAccessor)
        {
            httpContext = httpContextAccessor;
        }

        public bool HasUser()
        {
            AuthenticationResponse vm = httpContext.HttpContext.Session.Get<AuthenticationResponse>("user");
            if (vm == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
