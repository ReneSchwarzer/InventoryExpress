using WebExpress.Attribute;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.Attribute;
using WebExpress.WebApp.WebResource.PageSetting;

namespace InventoryExpress.WebResource.PageSetting
{
    [ID("SettingPlugin")]
    [Title("inventoryexpress.setting.plugin.label")]
    [Segment("plugin", "inventoryexpress.setting.plugin.label")]
    [Path("/SettingGeneral")]
    [SettingSection(SettingSection.Secondary)]
    [SettingIcon(TypeIcon.PuzzlePiece)]
    [SettingGroup("inventoryexpress.setting.system.label")]
    [SettingContext("inventoryexpress.setting.general.label")]
    [Module("InventoryExpress")]
    [Context("admin")]
    public sealed class PageSettingPlugin : PageTemplateWebAppSettingPlugin
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageSettingPlugin()
        {
        }
    }
}
