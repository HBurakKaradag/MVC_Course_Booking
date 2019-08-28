using System.Web.Optimization;

namespace BookingSystem.WebUI
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //BundleTable.EnableOptimizations = true;

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/bundles/cssDatatables").Include(
                 "~/Content/plugins/datatables/datatables.min.css",
                 "~/Content/plugins/datatables/dataTables.semanticui.min.css",
                 "~/Content/plugins/datatables/select.semanticui.min.css",
                 "~/Content/plugins/datatables/buttons.semanticui.min.css",
                 "~/Content/plugins/datatables/fixedColumns.semanticui.min.css",
                 "~/Content/plugins/datatables/fixedHeader.semanticui.min.css",
                 "~/Content/plugins/datatables/jquery.dataTables.min.css",
                 "~/Content/plugins/datatables/responsive.semanticui.min.css",
                 "~/Content/plugins/datatables/scroller.semanticui.min.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jsDatatables").Include(
                  "~/Scripts/plugins/datatables/datatables.min.js",
                  "~/Scripts/plugins/datatables/buttons.jqueryui.min.js",
                  "~/Scripts/plugins/datatables/buttons.colVis.min.js",
                  "~/Scripts/plugins/datatables/dataTables.buttons.min.js",
                  "~/Scripts/plugins/datatables/dataTables.semanticui.min.js",
                  "~/Scripts/plugins/datatables/select.semanticui.min.js",
                  "~/Scripts/plugins/datatables/buttons.semanticui.min.js",
                  "~/Scripts/plugins/datatables/fixedColumns.semanticui.min.js",
                  "~/Scripts/plugins/datatables/fixedHeader.semanticui.min.js",
                  "~/Scripts/plugins/datatables/responsive.semanticui.min.js",
                  "~/Scripts/plugins/datatables/scroller.semanticui.min.js",
                  "~/Scripts/plugins/datatables/dataTables.select.min.js"

                ));
        }
    }
}