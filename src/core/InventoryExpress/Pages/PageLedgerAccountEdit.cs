using InventoryExpress.Controls;
using InventoryExpress.Model;
using System;
using System.Linq;
using WebExpress.UI.Controls;

namespace InventoryExpress.Pages
{
    public class PageLedgerAccountEdit : PageBase, IPageLedgerAccount
    {
        /// <summary>
        /// Formular
        /// </summary>
        private ControlFormularLedgerAccount form;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageLedgerAccountEdit()
            : base("inventoryexpress.ledgeraccount.edit.label")
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Init()
        {
            base.Init();

            form = new ControlFormularLedgerAccount()
            {
                RedirectUrl = Uri.Take(-1)
            };
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();

            var id = Convert.ToInt32(GetParam("id"));
            var gLAccounts = ViewModel.Instance.GLAccounts.Where(x => x.ID == id).FirstOrDefault();

            Content.Content.Add(form);

            form.GLAccountName.Value = gLAccounts?.Name;
            form.Discription.Value = gLAccounts?.Discription;

            form.GLAccountName.Validation += (s, e) =>
            {
                if (e.Value.Count() < 1)
                {
                    e.Results.Add(new ValidationResult() { Text = "Geben Sie einen gültigen Namen ein!", Type = TypesInputValidity.Error });
                }
                else if (ViewModel.Instance.GLAccounts.Where(x => x.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase)).Count() > 0)
                {
                    e.Results.Add(new ValidationResult() { Text = "Das Sachkonto wird bereits verwendet. Geben Sie einen anderen Namen an!", Type = TypesInputValidity.Error });
                }
            };

            form.ProcessFormular += (s, e) =>
            {
                // Herstellerobjekt ändern und speichern
                gLAccounts.Name = form.GLAccountName.Value;
                //Tag = form.Tag.Value;
                gLAccounts.Discription = form.Discription.Value;

                ViewModel.Instance.SaveChanges();
            };
        }

        /// <summary>
        /// In String konvertieren
        /// </summary>
        /// <returns>Das Objekt als String</returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
