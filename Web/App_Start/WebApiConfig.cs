﻿using System;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Web.Infrastructure;

namespace Web
{
	public class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
			config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
		}
	} // class
} // namespace