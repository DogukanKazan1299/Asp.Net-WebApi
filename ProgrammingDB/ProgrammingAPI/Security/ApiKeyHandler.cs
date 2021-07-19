using Programming.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ProgrammingAPI.Security
{
    public class ApiKeyHandler:DelegatingHandler
    {

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var queryString = request.RequestUri.ParseQueryString();
            var apiKey = queryString["apiKey"];

            UsersDAL userDAL = new UsersDAL();
            var user = userDAL.GetUserByApiKey(apiKey);

            if (user != null)
            {
                var principal = new ClaimsPrincipal(new GenericIdentity(user.Name, "apiKey"));
                HttpContext.Current.User = principal;
            }

            return base.SendAsync(request, cancellationToken);
        }

    }
}