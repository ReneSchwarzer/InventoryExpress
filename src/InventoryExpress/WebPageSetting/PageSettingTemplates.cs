using InventoryExpress.Model;
using InventoryExpress.WebControl;
using System.Linq;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.WebSettingPage;
using WebExpress.WebApp.Wql;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebPageSetting
{
    [WebExTitle("inventoryexpress:inventoryexpress.templates.label")]
    [WebExSegment("templates", "inventoryexpress:inventoryexpress.templates.label")]
    [WebExContextPath("/setting")]
    [WebExSettingSection(WebExSettingSection.Primary)]
    [WebExSettingIcon(TypeIcon.Clone)]
    [WebExSettingGroup("inventoryexpress:inventoryexpress.setting.data.label")]
    [WebExSettingContext("webexpress.webapp:setting.tab.general.label")]
    [WebExModule<Module>]
    [WebExContext("general")]
    [WebExContext("template")]
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
            var list = ViewModel.GetTemplates(new WqlStatement()).OrderBy(x => x.Name);

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
