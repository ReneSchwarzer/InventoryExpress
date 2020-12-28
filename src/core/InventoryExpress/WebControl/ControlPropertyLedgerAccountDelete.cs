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
    [Section(Section.PropertySecondary)]
    [Application("InventoryExpress")]
    [Context("ledgeraccountedit")]
    public sealed class ControlPropertyLedgerAccountDelete : ControlButtonLink, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlPropertyLedgerAccountDelete()
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var guid = context.Page.GetParamValue("LedgerAccountID");
            var ledgerAccount = ViewModel.Instance.LedgerAccounts.Where(x => x.Guid == guid).FirstOrDefault();
            var form = new ControlFormular("del") { EnableSubmitAndNextButton = false, EnableCancelButton = false, RedirectUri = Uri };
            form.SubmitButton.Text = context.Page.I18N("inventoryexpress.delete.label");
            form.SubmitButton.Icon = new PropertyIcon(TypeIcon.TrashAlt);
            form.SubmitButton.Color = new PropertyColorButton(TypeColorButton.Danger);
            form.ProcessFormular += (s, e) =>
            {
                if (ledgerAccount != null)
                {
                    ViewModel.Instance.LedgerAccounts.Remove(ledgerAccount);
                    ViewModel.Instance.SaveChanges();

                    context.Page.Redirecting(context.Uri.Take(-1));
                }
            };

            Text = context.Page.I18N("inventoryexpress.delete.label");
            Icon = new PropertyIcon(TypeIcon.Trash);
            BackgroundColor = new PropertyColorButton(TypeColorButton.Danger);
            Value = ledgerAccount?.Created.ToString(context.Page.Culture.DateTimeFormat.ShortDatePattern);

            Modal = new ControlModal
            (
                "delete",
                context.Page.I18N("inventoryexpress.ledgeraccount.delete.label"),
                new ControlText()
                {
                    Text = context.Page.I18N("inventoryexpress.ledgeraccount.delete.description")
                },
                form
            );

            return base.Render(context);
        }
    }
}
