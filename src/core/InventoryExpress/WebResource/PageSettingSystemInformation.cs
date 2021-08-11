using WebExpress.Attribute;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.Attribute;
using WebExpress.WebApp.WebResource;

namespace InventoryExpress.WebResource
{
    [ID("SystemInformation")]
    [Title("inventoryexpress.settings.systeminformation.label")]
    [Segment("systeminformation", "inventoryexpress.settings.systeminformation.label")]
    [Path("/setting")]
    [SettingIcon(TypeIcon.InfoCircle)]
    [SettingGroup("inventoryexpress.settings.system.label")]
    [Module("InventoryExpress")]
    [Context("admin")]
    public sealed class PageSettingSystemInformation : PageTemplateWebAppSettingSystemInformation, IPageInventory
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageSettingSystemInformation()
        {
        }
    }
}
