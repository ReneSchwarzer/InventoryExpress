using InventoryExpress.Model;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.Attribute;
using WebExpress.UI.Component;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.Components;

namespace InventoryExpress.WebControl
{
    [Section(Section.PropertyPrimary)]
    [Application("InventoryExpress")]
    [Context("inventories")]
    public sealed class ControlPropertyInventoriesDetails : ControlList, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlPropertyInventoriesDetails()
        {
            Layout = TypeLayoutList.Flush;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var inventories = ViewModel.Instance.Inventories.ToList();

            Add(new ControlListItem(new ControlAttribute()
            {
                Name = context.I18N("inventoryexpress.inventory.details.count.label"),
                Value = inventories.Count().ToString(),
                TextColor = new PropertyColorText(TypeColorText.Secondary)
            }));

            Add(new ControlListItem(new ControlAttribute()
            {
                Name = context.I18N("inventoryexpress.inventory.details.totalacquisitioncosts.label"),
                Icon = new PropertyIcon(TypeIcon.EuroSign),
                Value = inventories.Sum(x => x.CostValue).ToString(context.Culture) + " €",
                TextColor = new PropertyColorText(TypeColorText.Secondary)
            }));

            return base.Render(context);
        }
    }
}
