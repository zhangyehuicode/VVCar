using System.Web;
using System.Web.Optimization;

namespace VVCar
{
    public class BundleConfig
    {
        // 有关捆绑的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                      "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jquerycookie").Include(
                        "~/Scripts/jquery.cookie*"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-weui").Include(
                        "~/Scripts/jquery-weui.js"));

            bundles.Add(new ScriptBundle("~/bundles/ko").Include(
                        "~/Scripts/knockout-3.4.0.js"));


            bundles.Add(new ScriptBundle("~/bundles/iscrollcc").Include(
                       "~/Scripts/iscroll.js", "~/Scripts/CouponCenter.js"));

            bundles.Add(new ScriptBundle("~/bundles/qrcode").Include(
                        "~/Scripts/JsBarcode.all.min.js",
                        "~/Scripts/jquery.qrcode.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-fileupd").Include(
                        "~/Scripts/jquery-fileupload/vendor/jquery.ui.widget.js",
                        "~/Scripts/jquery-fileupload/jquery.iframe-transport.js",
                        "~/Scripts/jquery-fileupload/jquery.fileupload.js",
                        "~/Scripts/jquery-fileupload/jquery.fileupload-process.js",
                        "~/Scripts/jquery-fileupload/jquery.fileupload-image.js",
                        "~/Scripts/jquery-fileupload/jquery.fileupload-validate.js"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootbox.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/couponCss").Include(
                      "~/Content/jquery-weui.css",
                      "~/Content/coupon/coupon.css"));

            bundles.Add(new StyleBundle("~/Content/uiboxCss").Include(
                "~/Content/coupon/ui-base.css",
                "~/Content/coupon/ui-box.css",
                "~/Content/coupon/ui-color.css",
                "~/Content/coupon/couponCenter.css"));

            bundles.Add(new StyleBundle("~/Content/jquery-weui").Include(
                      "~/Content/jquery-weui.css"));

            bundles.Add(new StyleBundle("~/Content/jquery-fileupd").Include(
                      "~/Content/jquery-fileupload/css/jquery.fileupload.css"));
#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}
