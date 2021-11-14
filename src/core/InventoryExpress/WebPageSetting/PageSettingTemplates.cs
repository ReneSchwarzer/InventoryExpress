using InventoryExpress.Model;
using InventoryExpress.Model.Entity;
using InventoryExpress.WebControl;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.Attribute;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.WebSettingPage;
using WebExpress.WebResource;

namespace InventoryExpress.WebPageSetting
{
    [ID("SettingTemplate")]
    [Title("inventoryexpress:inventoryexpress.templates.label")]
    [Segment("templates", "inventoryexpress:inventoryexpress.templates.label")]
    [Path("/Setting")]
    [SettingSection(SettingSection.Primary)]
    [SettingIcon(TypeIcon.Clone)]
    [SettingGroup("inventoryexpress:inventoryexpress.setting.data.label")]
    [SettingContext("webexpress.webapp:setting.tab.general.label")]
    [Module("inventoryexpress")]
    [Context("general")]
    [Context("template")]
    public sealed class PageSettingTemplates : PageWebAppSetting
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
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var visualTree = context.VisualTree;

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

            visualTree.Content.Primary.Add(grid);
        }
    }
}
