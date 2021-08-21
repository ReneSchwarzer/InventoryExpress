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
    [Section(Section.Metadata)]
    [Application("InventoryExpress")]
    [Context("inventorydetails")]
    public sealed class ControlHeadlineInventoryMetadata : ControlText, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlHeadlineInventoryMetadata()
        {
            //Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
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
                var id = context.Page.GetParamValue("InventoryID");
                var inventory = ViewModel.Instance.Inventories.Where(x => x.Guid.Equals(id)).FirstOrDefault();

                Text = string.Format(context.I18N("inventoryexpress.inventory.metadata.created"), inventory.Created.ToString("d", context.Culture));

                if (inventory.Created != inventory.Updated)
                {
                    Text += " ";
                    Text += string.Format(context.I18N("inventoryexpress.inventory.metadata.lastchange"), inventory.Updated.ToString("d", context.Culture));
                }
            }

            return base.Render(context);
        }
    }
}
