using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using Service.Interfaces;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Web.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class ApiAuthorizationFilter: AuthorizationFilterAttribute
    {
        private readonly bool _isActive = true;
        private const string Token = "Token";

        /// <summary>
        /// Default Authentication Constructor
        /// </summary>
        public ApiAuthorizationFilter()
        {
        }

        /// <summary>
        /// AuthenticationFilter constructor with isActive parameter
        /// </summary>
        /// <param name="isActive"></param>
        public ApiAuthorizationFilter(bool isActive)
        {
            _isActive = isActive;
        }

        public override void OnAuthorization(HttpActionContext filterContext)
        {
            if (!_isActive)
                return;

            var accountService = filterContext.ControllerContext.Configuration.DependencyResolver
                .GetService(typeof (IAccountService)) as IAccountService;

            var token = FetchAuthorizationHeader(filterContext);

            if (token != null && accountService != null)
            {
                var account = accountService.GetByToken(token);
                if (account == null)
                {
                    var response = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "Invalid Token." };
                    filterContext.Response = response;
                }
                else
                {
                    var userIdClaim = new Claim(ClaimTypes.NameIdentifier, account.Id.ToString());
                    var usernameClaim = new Claim(ClaimTypes.Name, account.Email);
                    var roleId = new Claim(ClaimTypes.Role, account.Role.ToString());
                    var identity = new ClaimsIdentity(new[] { usernameClaim, userIdClaim, roleId }, @"Token");
                    var principal = new ClaimsPrincipal(identity);
                    Thread.CurrentPrincipal = principal;
                    HttpContext.Current.User = Thread.CurrentPrincipal;
                }
            }
            else
            {
                filterContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    ReasonPhrase = "No Authorization Token present."
                };
            }
            base.OnAuthorization(filterContext);
        }

        /// <summary>
        /// Checks for autrhorization header in the request and parses it
        /// </summary>
        /// <param name="filterContext"></param>
        protected virtual string FetchAuthorizationHeader(HttpActionContext filterContext)
        {
            string authHeaderValue = null;
            var authRequest = filterContext.Request.Headers.Authorization;
            if (authRequest != null && !string.IsNullOrEmpty(authRequest.Scheme) && authRequest.Scheme == "Token")
                authHeaderValue = authRequest.Parameter;

            if (string.IsNullOrEmpty(authHeaderValue))
                return null;
            return authHeaderValue;
        }
    } // class
} // namespace