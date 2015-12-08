using System;
using System.Net.Http;
using System.Web.Http;
using Web.Infrastructure;

namespace Web.Api.v1
{
	[VersionedRoute("api/auth", 1)]
	public class AuthV1Controller : BaseApiController
	{
		public HttpResponseMessage Get()
		{
			var response = new {message = "Test"};
			return Ok(response);
		}
	} // class
} // namespace