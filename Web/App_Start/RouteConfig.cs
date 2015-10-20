using System;
using System.Web.Routing;
using System.Web.Mvc;

namespace Web
{
	public class RouteConfig
	{
		public static void RegisterRoutes (RouteCollection routes)
		{
			routes.IgnoreRoute ("{resource}.axd/{*pathInfo}");

			routes.MapRoute (
				"Default",
				"{controller}/{action}/{id}",
				new { controller = "Home", action = "Index", id = "" }
			);

			routes.MapRoute (
				"Transaction",
				"{controller}/year/{year}/month/{month}",
				new { controller = "Transaction", action = "Index", year = UrlParameter.Optional, month = UrlParameter.Optional }
			);
		}
	} // class
} // namespace