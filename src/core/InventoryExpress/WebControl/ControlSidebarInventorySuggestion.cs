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
            var guid = context.Page.GetParamValue("InventoryID");
            var inventory = ViewModel.Instance.Inventories.Where(x => x.Guid == guid).FirstOrDefault();
          
            Items.Add(new ControlNavigationItemLink() { Text = inventory?.Name });
            

            return base.Render(context);
        }
    }
}
