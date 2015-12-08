using System;
using System.Web.Http;
using System.Net.Http;
using System.Net;

namespace Web.Controllers
{
	public class BaseApiController : ApiController
	{
		protected HttpResponseMessage Ok(object content)
        {
            return Request.CreateResponse(HttpStatusCode.OK, content);
        }

        protected HttpResponseMessage NotFound(string reason)
        {
            var content = new { Message = reason };
            return Request.CreateResponse(HttpStatusCode.NotFound, content);
        }
	} // class
} // namespace