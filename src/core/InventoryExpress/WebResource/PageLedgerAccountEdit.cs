using InventoryExpress.WebControl;
using InventoryExpress.Model;
using System;
using System.Linq;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebResource;
using WebExpress.Attribute;
using WebExpress.Message;
using WebExpress.Internationalization;

namespace InventoryExpress.WebResource
{
    [ID("LedgerAccountEdit")]
    [Title("inventoryexpress.ledgeraccount.edit.label")]
    [SegmentGuid("LedgerAccountID", "inventoryexpress.ledgeraccount.edit.display")]
    [Path("/LedgerAccount")]
    [Module("InventoryExpress")]
    [Context("general")]
    [Context("ledgeraccountedit")]
    public sealed class PageLedgerAccountEdit : PageTemplateWebApp, IPageLedgerAccount
    {
        /// <summary>
        /// Formular
        /// </summary>
        private ControlFormularLedgerAccount form;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageLedgerAccountEdit()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Initialization()
        {
            base.Initialization();

            form = new ControlFormularLedgerAccount()
            {
                RedirectUri = Uri.Take(-1),
                Edit = true,
                EnableCancelButton = true,
                BackUri = Uri.Take(-1),
            };
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();

            var guid = GetParamValue("LedgerAccountID");
            var ledgerAccount = ViewModel.Instance.LedgerAccounts.Where(x => x.Guid == guid).FirstOrDefault();

            Content.Primary.Add(form);

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
        }
    }
}
