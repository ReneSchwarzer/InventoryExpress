using InventoryExpress.Model;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Html;
using WebExpress.UI.Attribute;
using WebExpress.UI.Component;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.Components;

namespace InventoryExpress.WebControl
{
    [Section(Section.SidebarPrimary)]
    [Application("InventoryExpress")]
    [Context("inventorydetails")]
    [Context("attachment")]
    [Context("inventoryedit")]
    public sealed class ControlSidebarInventorySuggestion : ControlNavigation, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlSidebarInventorySuggestion()
        {
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            lock (ViewModel.Instance.Database)
            {
                var guid = context.Page.GetParamValue("InventoryID");
                var inventories = ViewModel.Instance.Inventories.OrderBy(x => x.Name).ToList();

                var index = inventories.FindIndex(x => x.Guid == guid);
                var from = index - 30 >= 0 ? index - 30 : 0;
                var till = index + 30 <= inventories.Count ? index + 30 : inventories.Count;

                Layout = TypeLayoutTab.Pill;
                Orientation = TypeOrientationTab.Vertical;
                Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);

                for (int i = from; i < till; i++)
                {
                    var inventory = inventories[i];
                    Items.Add(new ControlNavigationItemLink()
                    {
                        Text = inventory?.Name,
                        Uri = context.Uri.Root.Append(inventory.Guid),
                        Active = inventory.Guid == guid ? TypeActive.Active : TypeActive.None
                    });
                }

                return base.Render(context);
            }
        }
    }
}
