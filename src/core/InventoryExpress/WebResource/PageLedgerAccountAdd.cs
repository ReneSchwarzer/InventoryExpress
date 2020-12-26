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
    [ID("LedgerAccountAdd")]
    [Title("inventoryexpress.ledgeraccount.add.label")]
    [Segment("add", "inventoryexpress.ledgeraccount.add.label")]
    [Path("/LedgerAccount")]
    [Module("InventoryExpress")]
    [Context("general")]
    public sealed class PageLedgerAccountAdd : PageTemplateWebApp, IPageLedgerAccount
    {
        /// <summary>
        /// Formular
        /// </summary>
        private ControlFormularLedgerAccount form;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageLedgerAccountAdd()
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
                RedirectUrl = Uri.Take(-1)
            };
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();

            Content.Primary.Add(form);

            form.LedgerAccountName.Validation += (s, e) =>
            {
                if (e.Value.Count() < 1)
                {
                    e.Results.Add(new ValidationResult() { Text = "Geben Sie einen gültigen Namen ein!", Type = TypesInputValidity.Error });
                }
                else if (ViewModel.Instance.LedgerAccounts.Where(x => x.Name.Equals(e.Value)).Count() > 0)
                {
                    e.Results.Add(new ValidationResult() { Text = "Das Sachkonto wird bereits verwendet. Geben Sie einen anderen Namen an!", Type = TypesInputValidity.Error });
                }
            };

            form.ProcessFormular += (s, e) =>
            {
                // Neues Sachkonto erstellen und speichern
                var ledgerAccount = new LedgerAccount()
                {
                    Name = form.LedgerAccountName.Value,
                    Description = form.Description.Value,
                    //Tag = form.Tag.Value,
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                    Guid = Guid.NewGuid().ToString()
                };

                if (GetParam(form.Image.Name) is ParameterFile file)
                {
                    if (ledgerAccount.Media == null)
                    {
                        ledgerAccount.Media = new Media()
                        {
                            Name = file.Value,
                            Data = file.Data,
                            Created = DateTime.Now,
                            Updated = DateTime.Now,
                            Guid = Guid.NewGuid().ToString()
                        };
                    }
                    else
                    {
                        ledgerAccount.Media.Name = file.Value;
                        ledgerAccount.Media.Data = file.Data;
                        ledgerAccount.Media.Updated = DateTime.Now;
                    }
                }

                ViewModel.Instance.LedgerAccounts.Add(ledgerAccount);
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
