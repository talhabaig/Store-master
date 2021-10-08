using System.Web;
using System.Web.Optimization;

namespace BaigMedicalStore
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                  "~/Scripts/jquery.unobtrusive-ajax*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new ScriptBundle("~/bundles/qtip").Include("~/Scripts/qTip/jquery.qtip.js"));


            bundles.Add(new ScriptBundle("~/bundles/SiteScript").Include(
                       "~/Scripts/Application/Common/NameSpaces.js",
                       "~/Scripts/Application/Common/AppVar.js",
                       "~/Scripts/Application/Common/AppConstants.js",
                       "~/Scripts/Application/Common/ServiceManager.js",
                        "~/Scripts/jquery.tmpl.min.js",
                       "~/Scripts/Extension/javascriptExtensions.js",
                       "~/Scripts/Extension/jQueryExtensions.js",
                       "~/Scripts/Application/Common/UtilityFunctions.js",
                       "~/Scripts/Application/Common/DataAnnotation.js",
                       "~/Scripts/SiteScript.js"
                       ));

            //Kendo UI script bundle
            bundles.Add(new ScriptBundle("~/bundles/KendoScript").Include(
                        "~/Content/Kendo/js/kendo.all.min.js",
                        "~/Content/Kendo/js/kendo.aspnetmvc.min.js"));

            //Distributor Management Bundles

            bundles.Add(new ScriptBundle("~/bundles/DistributorFormManager").Include("~/Scripts/Application/Distributor/DistributorFormManager.js"));

            bundles.Add(new ScriptBundle("~/bundles/DistributorGridManager").Include("~/Scripts/Application/Distributor/DistributorGridManager.js"));

            //Manufacturer Management Bundles

            bundles.Add(new ScriptBundle("~/bundles/ManufacturerFormManager").Include("~/Scripts/Application/Manufacturer/ManufacturerFormManager.js"));

            bundles.Add(new ScriptBundle("~/bundles/ManufacturerGridManager").Include("~/Scripts/Application/Manufacturer/ManufacturerGridManager.js"));

            //Item Management Bundles

            bundles.Add(new ScriptBundle("~/bundles/ItemFormManager").Include("~/Scripts/Application/Item/ItemFormManager.js"));

            bundles.Add(new ScriptBundle("~/bundles/ItemGridManager").Include("~/Scripts/Application/Item/ItemGridManager.js"));
            //Report Management Bundles
            bundles.Add(new ScriptBundle("~/bundles/ReportGridManager").Include("~/Scripts/Application/Report/ReportGridManager.js"));

            //Invoice Management Bundles

            bundles.Add(new ScriptBundle("~/bundles/InvoiceFormManager").Include("~/Scripts/Application/Invoice/InvoiceFormManager.js"));

            bundles.Add(new ScriptBundle("~/bundles/InvoiceGridManager").Include("~/Scripts/Application/Invoice/InvoiceGridManager.js"));

            bundles.Add(new StyleBundle("~/Content/kendo/css").Include(
                        "~/Content/Kendo/css/kendo.dataviz.default.min.css",
                        "~/Content/Kendo/css/kendo.common.min.css",
                        "~/Content/Kendo/css/kendo.rtl.min.css",
                        "~/Content/Kendo/css/kendo.dataviz.min.css",
                        "~/Content/Kendo/css/kendo.blueopal.min.css",
                        "~/Content/Kendo/css/kendo.dataviz.blueopal.min.css"));
        }
    }
}
