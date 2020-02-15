using System;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Security.Principal;

namespace CarPoolApplication
{
    public class BasicAuthenticationAttribute:AuthorizationFilterAttribute
    {
        private const string Realm = "My_Realm";
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request
                    .CreateResponse(System.Net.HttpStatusCode.Unauthorized);
            }
            else
            {
                string AuthenticationToken = actionContext.Request.Headers.Authorization.Parameter;
                string decodedAuthenticationToken= Encoding.UTF8.GetString( Convert.FromBase64String(AuthenticationToken));
                string[] UsernamePasswordArray= decodedAuthenticationToken.Split(':');
                string Username = UsernamePasswordArray[0];
                string Password = UsernamePasswordArray[1];

                if (Security.login(Username, Password))
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(Username), null);
                }
                else
                {
                    actionContext.Response = actionContext.Request
                        .CreateResponse(System.Net.HttpStatusCode.Unauthorized);
                }
            }
        }
    }
}
