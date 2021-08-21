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
    [ID("SettingTemplate")]
    [Title("inventoryexpress.templates.label")]
    [Segment("templates", "inventoryexpress.templates.label")]
    [Path("/SettingGeneral")]
    [SettingSection(SettingSection.Primary)]
    [SettingIcon(TypeIcon.Clone)]
    [SettingGroup("inventoryexpress.setting.data.label")]
    [SettingContext("inventoryexpress.setting.general.label")]
    [Module("InventoryExpress")]
    [Context("general")]
    [Context("template")]
    public sealed class PageSettingTemplates : PageTemplateWebAppSetting, IPageTemplate
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageSettingTemplates()
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

            var grid = new ControlPanelGrid() { Fluid = TypePanelContainer.Fluid };
            var list = null as ICollection<Template>;

            lock (ViewModel.Instance.Database)
            {
                list = ViewModel.Instance.Templates.OrderBy(x => x.Name).ToList();
            }

            foreach (var template in list)
            {
                var card = new ControlCardTemplate()
                {
                    Template = template
                };

                grid.Content.Add(card);
            }

            Content.Primary.Add(grid);
        }
    }
}
