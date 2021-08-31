using InventoryExpress.Model;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.Attribute;
using WebExpress.UI.Component;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.Components;
using WebExpress.WebApp.WebControl;

namespace InventoryExpress.WebControl
{
    /// <summary>
    /// Modal zum Löschen eines Sachkontos. Wird von der Komponetne ControlMoreLedgerAccountDelete aufgerufen
    /// </summary>
    [Section(Section.ContentSecondary)]
    [Application("InventoryExpress")]
    [Context("ledgeraccountedit")]
    public sealed class ControlContentLedgerAccountModalDelete : ControlModalFormConfirmDelete, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlContentLedgerAccountModalDelete()
           : base("del_ledgeraccount")
        {
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Confirm += (s, e) =>
            {
                lock (ViewModel.Instance.Database)
                {
                    var guid = context.Page.GetParamValue("LedgerAccountID");
                    var ledgerAccount = ViewModel.Instance.LedgerAccounts.Where(x => x.Guid == guid).FirstOrDefault();

                    if (ledgerAccount != null)
                    {
                        ViewModel.Instance.LedgerAccounts.Remove(ledgerAccount);
                        ViewModel.Instance.SaveChanges();
                    }
                }
            };

            Header = context.Page.I18N("inventoryexpress.ledgeraccount.delete.label");
            Content = new ControlFormularItemStaticText() { Text = context.I18N("inventoryexpress.ledgeraccount.delete.description") };
            RedirectUri = context.Uri.Take(-1);

            return base.Render(context);
        }
    }
}
