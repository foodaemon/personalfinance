using System;
using System.Web.Mvc;

namespace Web
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters (GlobalFilterCollection filters)
		{
			filters.Add (new AuthorizeAttribute ());
			filters.Add (new HandleErrorAttribute ());
		}
	} // class
} // namespace