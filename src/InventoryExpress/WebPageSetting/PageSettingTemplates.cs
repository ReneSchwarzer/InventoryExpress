using InventoryExpress.Model;
using InventoryExpress.WebControl;
using System.Linq;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.WebSettingPage;
using WebExpress.WebCore.WebAttribute;
using WebExpress.WebCore.WebResource;
using WebExpress.WebUI.WebAttribute;
using WebExpress.WebUI.WebControl;

namespace InventoryExpress.WebPageSetting
{
    [Title("inventoryexpress:inventoryexpress.templates.label")]
    [Segment("templates", "inventoryexpress:inventoryexpress.templates.label")]
    [ContextPath("/setting")]
    [SettingSection(SettingSection.Primary)]
    [SettingIcon(TypeIcon.Clone)]
    [SettingGroup("inventoryexpress:inventoryexpress.setting.data.label")]
    [SettingContext("webexpress.webapp:setting.tab.general.label")]
    [Module<Module>]
    public sealed class PageSettingTemplates : PageWebAppSetting
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PageSettingTemplates()
        {
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);
        }

        /// <summary>
        /// Processing of the resource.
        /// </summary>
        /// <param name="context">The context for rendering the page.</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var visualTree = context.VisualTree;

            var grid = new ControlPanelGrid() { Fluid = TypePanelContainer.Fluid };
            var list = ViewModel.GetTemplates().OrderBy(x => x.Name);

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
