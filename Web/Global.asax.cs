using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using Core.Logging;
using System.Web.Http;

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

			// Register WebApi
			GlobalConfiguration.Configure (WebApiConfig.Register);

			AreaRegistration.RegisterAllAreas ();

			FilterConfig.RegisterGlobalFilters (GlobalFilters.Filters);
			RouteConfig.RegisterRoutes (RouteTable.Routes);

			BundleConfig.RegisterBundles(BundleTable.Bundles);

		}

		/// <summary>
		/// Handles the Error event of the Application control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		protected void Application_Error(object sender, EventArgs e)
		{
			// Code that runs when an unhandled error occurs
			var ex = Server.GetLastError();
			Logger<MvcApplication>.GetInstance.Error (ex.Message);
		}
	} // class
} // namespace