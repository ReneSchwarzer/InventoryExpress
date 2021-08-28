using WebExpress.Attribute;
using WebExpress.Message;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.Attribute;
using WebExpress.WebApp.WebResource.PageSetting;

namespace InventoryExpress.WebResource.PageSetting
{
    [ID("SettingLog")]
    [Title("inventoryexpress.setting.log.label")]
    [Segment("log", "inventoryexpress.setting.log.label")]
    [Path("/SettingGeneral")]
    [SettingSection(SettingSection.Secondary)]
    [SettingIcon(TypeIcon.FileMedicalAlt)]
    [SettingGroup("inventoryexpress.setting.system.label")]
    [SettingContext("inventoryexpress.setting.general.label")]
    [Module("InventoryExpress")]
    [Context("admin")]
    public sealed class PageSettingLog : PageTemplateWebAppSettingLog
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageSettingLog()
        {
        }

        /// <summary>
        /// Vorverarbeitung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        public override void PreProcess(Request request)
        {
            DownloadUri = Uri.Append("download");
            
            base.PreProcess(request);
        }
    }
}
