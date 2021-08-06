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
    [Context("ledgeraccountedit")]
    public sealed class ControlPropertyLedgerAccountDetails : ControlList, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlPropertyLedgerAccountDetails()
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
                var guid = context.Page.GetParamValue("LedgerAccountID");
                var ledgerAccount = ViewModel.Instance.LedgerAccounts.Where(x => x.Guid == guid).FirstOrDefault();

                Add(new ControlListItem(new ControlAttribute()
                {
                    Name = context.Page.I18N("inventoryexpress.ledgeraccount.creationdate.label") + ":",
                    Icon = new PropertyIcon(TypeIcon.CalendarPlus),
                    Value = ledgerAccount?.Created.ToString(context.Culture.DateTimeFormat.ShortDatePattern),
                    TextColor = new PropertyColorText(TypeColorText.Secondary)
                }));

                Add(new ControlListItem(new ControlAttribute()
                {
                    Name = context.Page.I18N("inventoryexpress.ledgeraccount.updatedate.label") + ":",
                    Icon = new PropertyIcon(TypeIcon.Save),
                    Value = ledgerAccount?.Updated.ToString(context.Culture.DateTimeFormat.ShortDatePattern + " " + context.Culture.DateTimeFormat.ShortTimePattern),
                    TextColor = new PropertyColorText(TypeColorText.Secondary)
                }));

                return base.Render(context);
            }
        }
    }
}
