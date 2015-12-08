using System;
using System.Web.Http.ExceptionHandling;
using System.Web.Http;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Web
{
	public class GlobalExceptionHandler : ExceptionHandler
	{

		public override void Handle(ExceptionHandlerContext context)
		{
			if (context.Exception is ValidationException)
			{
				var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
				{
					Content = new StringContent(context.Exception.Message),
					ReasonPhrase = "ValidationException"
				};

				context.Result = new ErrorMessageResult(context.Request, resp);
			}
			else
			{
				// Do something here...
			}
		}

		public class ErrorMessageResult : IHttpActionResult
		{
			private HttpRequestMessage _request;
			private HttpResponseMessage _httpResponseMessage;


			public ErrorMessageResult(HttpRequestMessage request, HttpResponseMessage httpResponseMessage)
			{
				_request = request;
				_httpResponseMessage = httpResponseMessage;
			}

			public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
			{
				return Task.FromResult(_httpResponseMessage);
			}
		}

	}
}