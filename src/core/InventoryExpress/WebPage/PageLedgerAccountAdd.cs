using InventoryExpress.Model;
using InventoryExpress.WebControl;
using System;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [ID("LedgerAccountAdd")]
    [Title("inventoryexpress:inventoryexpress.ledgeraccount.add.label")]
    [Segment("add", "inventoryexpress:inventoryexpress.ledgeraccount.add.label")]
    [Path("/LedgerAccount")]
    [Module("inventoryexpress")]
    [Context("general")]
    public sealed class PageLedgerAccountAdd : PageWebApp, IPageLedgerAccount
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageLedgerAccountAdd()
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
            form.EnableCancelButton = true;
            form.RedirectUri = context.Request.Uri.Take(-1);
            form.BackUri = context.Request.Uri.Take(-1);

            form.LedgerAccountName.Validation += (s, e) =>
            {
                if (e.Value.Count() < 1)
                {
                    e.Results.Add(new ValidationResult() { Text = this.I18N("inventoryexpress.ledgeraccount.validation.name.invalid"), Type = TypesInputValidity.Error });
                }
                else if (ViewModel.Instance.LedgerAccounts.Where(x => x.Name.Equals(e.Value)).Count() > 0)
                {
                    e.Results.Add(new ValidationResult() { Text = this.I18N("inventoryexpress.ledgeraccount.validation.name.used"), Type = TypesInputValidity.Error });
                }
            };

            form.ProcessFormular += (s, e) =>
            {
                // Neues Sachkonto erstellen und speichern
                var ledgerAccount = new LedgerAccount()
                {
                    Name = form.LedgerAccountName.Value,
                    Description = form.Description.Value,
                    Tag = form.Tag.Value,
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                    Guid = Guid.NewGuid().ToString()
                };

                if (context.Request.GetParameter(form.Image.Name) is ParameterFile file)
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

            context.VisualTree.Content.Primary.Add(form);
        }
    }
}
