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
    [Context("supplieredit")]
    public sealed class ControlPropertySupplierDetails : ControlList, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlPropertySupplierDetails()
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
            lock (ViewModel.Instance.Database)
            {
                var guid = context.Page.GetParamValue("SupplierID");
                var supplier = ViewModel.Instance.Suppliers.Where(x => x.Guid == guid).FirstOrDefault();

                Add(new ControlListItem(new ControlAttribute()
                {
                    Name = context.Page.I18N("inventoryexpress.supplier.creationdate.label") + ":",
                    Icon = new PropertyIcon(TypeIcon.CalendarPlus),
                    Value = supplier?.Created.ToString(context.Culture.DateTimeFormat.ShortDatePattern),
                    TextColor = new PropertyColorText(TypeColorText.Secondary)
                }));

                Add(new ControlListItem(new ControlAttribute()
                {
                    Name = context.Page.I18N("inventoryexpress.supplier.updatedate.label") + ":",
                    Icon = new PropertyIcon(TypeIcon.Save),
                    Value = supplier?.Updated.ToString(context.Culture.DateTimeFormat.ShortDatePattern + " " + context.Culture.DateTimeFormat.ShortTimePattern),
                    TextColor = new PropertyColorText(TypeColorText.Secondary)
                }));

                return base.Render(context);
            }
        }
    }
}
