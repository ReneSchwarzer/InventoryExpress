using InventoryExpress.Model;
using InventoryExpress.WebControl;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.Attribute;
using WebExpress.WebApp.WebResource;

namespace InventoryExpress.WebResource
{
    [ID("SettingCondition")]
    [Title("inventoryexpress.condition.label")]
    [Segment("condition", "inventoryexpress.condition.label")]
    [Path("/SettingGeneral")]
    [SettingSection(SettingSection.Primary)]
    [SettingIcon(TypeIcon.StarHalf)]
    [SettingGroup("inventoryexpress.setting.data.label")]
    [SettingContext("inventoryexpress.setting.general.label")]
    [Module("InventoryExpress")]
    [Context("general")]
    [Context("condition")]
    public sealed class PageSettingConditions : PageTemplateWebAppSetting, IPageTemplate
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageSettingConditions()
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

            var context = new RenderContext(this);

            var table = new ControlTable()
            {
                Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.Three),
            };

            table.AddColumn(context.I18N("inventoryexpress.condition.name.label"));
            table.AddColumn(context.I18N("inventoryexpress.condition.description.label"));
            table.AddColumn(context.I18N("inventoryexpress.condition.order.label"));
            table.AddColumn(context.I18N("inventoryexpress.condition.action.label"));


            Content.Preferences.Add(table);
        }
    }
}
