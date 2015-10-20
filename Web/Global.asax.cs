using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web
{
	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterGlobalFilters (GlobalFilterCollection filters)
		{
			filters.Add (new HandleErrorAttribute ());
		}

		protected void Application_Start ()
		{
			ViewEngines.Engines.Clear (); // clear all view engines
			ViewEngines.Engines.Add (new RazorViewEngine()); // add razor view engines

			AreaRegistration.RegisterAllAreas ();

			FilterConfig.RegisterGlobalFilters (GlobalFilters.Filters);
			RouteConfig.RegisterRoutes (RouteTable.Routes);
		}
	} // class
} // namespace