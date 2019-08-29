﻿using System.Web;
using System.Web.Optimization;

namespace JobsProjectApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css/JobFiles").Include(
                "~/Content/css/animate-3.7.0.css",
                "~/Content/css/font-awesome-4.7.0.min.css",
                "~/fonts/fonts/flat-icon/flaticon.css",
                "~/Content/css/bootstrap-4.1.3.min.css",
                "~/Content/css/owl-carousel.min.css",
                "~/Content/css/nice-select.css",
                "~/Content/css/style.css"));

            bundles.Add(new ScriptBundle("~/bundles/JobFile").Include(
                "~/Scripts/js/vendor/jquery-2.2.4.min.js",
                "~/Scripts/js/vendor/bootstrap-4.1.3.min.js",
                "~/Scripts/js/vendor/wow.min.js",
                "~/Scripts/js/vendor/owl-carousel.min.js",
                "~/Scripts/js/vendor/jquery.nice-select.min.js",
                "~/Scripts/js/vendor/ion.rangeSlider.js",
                "~/Scripts/js/main.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
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
        }
    }
}
