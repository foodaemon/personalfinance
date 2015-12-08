using System;
using System.Net.Http;
using System.Web.Http;

namespace Web.Controllers
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