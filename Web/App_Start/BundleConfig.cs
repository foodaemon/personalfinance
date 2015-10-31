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
			bundles.Add(new StyleBundle("~/content/css").Include(
				"~/Content/bootstrap.css",
				"~/Content/font-awesome.css",
				"~/toastr.css",
				"~/Content/site.css"));

			bundles.Add (new StyleBundle ("~/content/datatables").Include (
				"~/Content/datatables.css",
				"~/Content/scroller.bootstrap.css",
				"~/Content/select.bootstrap.css"
			));
		}
	} // class
} // namespace