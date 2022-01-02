using InventoryExpress.Model;
using System.Linq;
using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebComponent;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebComponent;
using WebExpress.WebAttribute;
using WebExpress.WebPage;
using static WebExpress.Internationalization.InternationalizationManager;

namespace InventoryExpress.WebComponent
{
    /// <summary>
    /// Modal zum Löschen eines Sachkontos. Wird von der Komponetne ControlMoreLedgerAccountDelete aufgerufen
    /// </summary>
    [Section(Section.ContentSecondary)]
    [Module("inventoryexpress")]
    [Context("ledgeraccountedit")]
    public sealed class ComponentContentLedgerAccountModalDelete : ComponentControlModalFormConfirmDelete
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ComponentContentLedgerAccountModalDelete()
           : base("del_ledgeraccount")
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IComponentContext context)
        {
            base.Initialization(context);
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
                    var guid = context.Request.GetParameter("LedgerAccountID")?.Value;
                    var ledgerAccount = ViewModel.Instance.LedgerAccounts.Where(x => x.Guid == guid).FirstOrDefault();

                    if (ledgerAccount != null)
                    {
                        ViewModel.Instance.LedgerAccounts.Remove(ledgerAccount);
                        ViewModel.Instance.SaveChanges();
                    }
                }
            };

            Header = I18N(context.Culture, "inventoryexpress:inventoryexpress.ledgeraccount.delete.label");
            Content = new ControlFormularItemStaticText() { Text = I18N(context.Culture, "inventoryexpress:inventoryexpress.ledgeraccount.delete.description") };
            RedirectUri = context.Uri.Take(-1);

            return base.Render(context);
        }
    }
}
