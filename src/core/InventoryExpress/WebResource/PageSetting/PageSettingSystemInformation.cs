using WebExpress.Attribute;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.Attribute;
using WebExpress.WebApp.WebResource.PageSetting;

namespace InventoryExpress.WebResource.PageSetting
{
    [ID("SettingSystemInformation")]
    [Title("inventoryexpress.setting.systeminformation.label")]
    [Segment("systeminformation", "inventoryexpress.setting.systeminformation.label")]
    [Path("/SettingGeneral")]
    [SettingSection(SettingSection.Secondary)]
    [SettingIcon(TypeIcon.InfoCircle)]
    [SettingGroup("inventoryexpress.setting.system.label")]
    [SettingContext("inventoryexpress.setting.general.label")]
    [Module("InventoryExpress")]
    [Context("admin")]
    public sealed class PageSettingSystemInformation : PageTemplateWebAppSettingSystemInformation
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageSettingSystemInformation()
        {
        }
    }
}
