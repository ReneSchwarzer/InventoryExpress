using InventoryExpress.WebControl;
using InventoryExpress.Model;
using System;
using System.Linq;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebResource;
using WebExpress.Attribute;
using WebExpress.Message;

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
                RedirectUrl = Uri.Take(-1),
                Edit = true
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
            form.Tag.Value = "";

            form.LedgerAccountName.Validation += (s, e) =>
            {
                if (e.Value.Count() < 1)
                {
                    e.Results.Add(new ValidationResult() { Text = "Geben Sie einen gültigen Namen ein!", Type = TypesInputValidity.Error });
                }
                else if (!ledgerAccount.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase) && ViewModel.Instance.Suppliers.Where(x => x.Name.Equals(e.Value)).Count() > 0)
                {
                    e.Results.Add(new ValidationResult() { Text = "Das Sachkonto wird bereits verwendet. Geben Sie einen anderen Namen an!", Type = TypesInputValidity.Error });
                }
            };

            form.ProcessFormular += (s, e) =>
            {
                // Sachkonto ändern und speichern
                ledgerAccount.Name = form.LedgerAccountName.Value;
                ledgerAccount.Description = form.Description.Value;
                ledgerAccount.Updated = DateTime.Now;
                //ledgerAccount.Tag = form.Tag.Value;

                ViewModel.Instance.SaveChanges();
            };
        }
    }
}
