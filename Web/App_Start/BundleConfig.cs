using System.Web.Optimization;

namespace Web
{
	public class BundleConfig
	{
		public static void RegisterBundles(BundleCollection bundles)
		{
			// scripts bundle
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
				"~/Scripts/jquery.js"));
			bundles.Add(new ScriptBundle("~/bundles/site").Include(
				"~/Scripts/app.js"));
			bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
				"~/Scripts/datatables.js",
				"~/Scripts/dataTables.scroller.js"
			));

			// styles bundles
			bundles.Add(new StyleBundle("~/Content/css").Include(
				"~/Content/bootstrap.css",
				"~/Content/font-awesome.css",
				"~/Content/site.css"));
		}
	} // class
} // namespace