using System.IO;
using WebExpress.Attribute;
using WebExpress.WebApp.WebResource.PageSetting;

namespace InventoryExpress.WebResource.PageSetting
{
    [ID("SettingLogDownload")]
    [Segment("download", "")]
    [Path("/SettingGeneral/SettingLog")]
    [Module("InventoryExpress")]
    [Context("admin")]
    public sealed class PageSettingLogDownload : PageTemplateWebAppSettingLogDownload
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageSettingLogDownload()
        {

        }
    }
}
