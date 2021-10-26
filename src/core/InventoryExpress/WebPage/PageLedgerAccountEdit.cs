using InventoryExpress.Model;
using InventoryExpress.WebControl;
using System;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [ID("LedgerAccountEdit")]
    [Title("inventoryexpress:inventoryexpress.ledgeraccount.edit.label")]
    [SegmentGuid("LedgerAccountID", "inventoryexpress:inventoryexpress.ledgeraccount.edit.display")]
    [Path("/LedgerAccount")]
    [Module("inventoryexpress")]
    [Context("general")]
    [Context("ledgeraccountedit")]
    public sealed class PageLedgerAccountEdit : PageWebApp, IPageLedgerAccount
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageLedgerAccountEdit()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var form = new ControlFormularLedgerAccount();
            var guid = context.Request.GetParameter("LedgerAccountID")?.Value;
            var ledgerAccount = ViewModel.Instance.LedgerAccounts.Where(x => x.Guid == guid).FirstOrDefault();

            form.Edit = true;
            form.RedirectUri = context.Uri.Take(-1);
            form.BackUri = context.Uri.Take(-1);
            form.LedgerAccountName.Value = ledgerAccount?.Name;
            form.Description.Value = ledgerAccount?.Description;
            form.Tag.Value = ledgerAccount?.Tag;

            form.LedgerAccountName.Validation += (s, e) =>
            {
                if (e.Value.Count() < 1)
                {
                    e.Results.Add(new ValidationResult() { Text = this.I18N("inventoryexpress.ledgeraccount.validation.name.invalid"), Type = TypesInputValidity.Error });
                }
                else if (!ledgerAccount.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase) && ViewModel.Instance.Suppliers.Where(x => x.Name.Equals(e.Value)).Count() > 0)
                {
                    e.Results.Add(new ValidationResult() { Text = this.I18N("inventoryexpress.ledgeraccount.validation.name.used"), Type = TypesInputValidity.Error });
                }
            };

            form.ProcessFormular += (s, e) =>
            {
                // Sachkonto ändern und speichern
                ledgerAccount.Name = form.LedgerAccountName.Value;
                ledgerAccount.Description = form.Description.Value;
                ledgerAccount.Tag = form.Tag.Value;
                ledgerAccount.Updated = DateTime.Now;

                ViewModel.Instance.SaveChanges();
            };

            context.VisualTree.Content.Primary.Add(form);
        }
    }
}
