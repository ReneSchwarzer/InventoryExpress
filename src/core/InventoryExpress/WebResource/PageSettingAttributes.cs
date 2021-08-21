using InventoryExpress.Model;
using InventoryExpress.WebControl;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.Attribute;
using WebExpress.WebApp.WebResource;

namespace InventoryExpress.WebResource
{
    [ID("SettingAttribute")]
    [Title("inventoryexpress.attribute.label")]
    [Segment("attribute", "inventoryexpress.attribute.label")]
    [Path("/SettingGeneral")]
    [SettingSection(SettingSection.Primary)]
    [SettingIcon(TypeIcon.Cubes)]
    [SettingGroup("inventoryexpress.setting.data.label")]
    [SettingContext("inventoryexpress.setting.general.label")]
    [Module("InventoryExpress")]
    [Context("general")]
    [Context("condition")]
    public sealed class PageSettingAttributes : PageTemplateWebAppSetting, IPageTemplate
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageSettingAttributes()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Initialization()
        {
            base.Initialization();
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();

           
        }
    }
}
