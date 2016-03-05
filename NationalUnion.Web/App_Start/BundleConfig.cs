using System.Web;
using System.Web.Optimization;

namespace NationalUnion.Web
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                        "~/Scripts/common.js"));

            bundles.Add(new ScriptBundle("~/bundles/home").Include(
                       "~/Scripts/home.js"));

            //easyui themes
            bundles.Add(new StyleBundle("~/Content/themes/bootstrap/css").Include
                (
                "~/Content/themes/bootstrap/easyui.css",
                "~/Content/themes/bootstrap/accordion.css",
                "~/Content/themes/bootstrap/calendar.css",
                "~/Content/themes/bootstrap/combo.css",
                "~/Content/themes/bootstrap/combobox.css",
                "~/Content/themes/bootstrap/datagrid.css",
                "~/Content/themes/bootstrap/datebox.css",
                "~/Content/themes/bootstrap/dialog.css",
                "~/Content/themes/bootstrap/layout.css",
                "~/Content/themes/bootstrap/linkbutton.css",
                "~/Content/themes/bootstrap/menu.css",
                "~/Content/themes/bootstrap/menubutton.css",
                "~/Content/themes/bootstrap/messager.css",
                "~/Content/themes/bootstrap/pagination.css",
                "~/Content/themes/bootstrap/panel.css",
                "~/Content/themes/bootstrap/progressbar.css",
                "~/Content/themes/bootstrap/propertygrid.css",
                "~/Content/themes/bootstrap/searchbox.css",
                "~/Content/themes/bootstrap/slider.css",
                "~/Content/themes/bootstrap/spinner.css",
                "~/Content/themes/bootstrap/splitbutton.css",
                "~/Content/themes/bootstrap/tabs.css",
                "~/Content/themes/bootstrap/tree.css",
                "~/Content/themes/bootstrap/validatebox.css",
                "~/Content/themes/bootstrap/window.css"
                ));

            bundles.Add(new StyleBundle("~/Content/default/blue/css").Include("~/Content/themes/default/easyui.css"));

            bundles.Add(new StyleBundle("~/Content/themes/gray/css").Include("~/Content/themes/gray/easyui.css"));

            bundles.Add(new StyleBundle("~/Content/themes/metro/css").Include("~/Content/themes/metro/easyui.css"));


            // 使用 Modernizr 的开发版本进行开发和了解信息。然后，当你做好生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/tree/css").Include("~/Content/tree/css/tree.css"));
        }
    }
}