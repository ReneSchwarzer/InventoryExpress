using InventoryExpress.Model;
using InventoryExpress.Model.Entity;
using InventoryExpress.WebControl;
using System;
using System.IO;
using System.Linq;
using WebExpress.Message;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
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
        /// Liefert das Formular
        /// </summary>
        private ControlFormularLedgerAccount Form { get; } = new ControlFormularLedgerAccount("ledgeraccount")
        {
            Edit = false
        };

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

            Form.InitializeFormular += InitializeFormular;
            Form.FillFormular += FillFormular;
            Form.LedgerAccountName.Validation += LedgerAccountNameValidation;
            Form.ProcessFormular += ProcessFormular;
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Formular verarbeitet werden soll.
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente/param>
        private void ProcessFormular(object sender, FormularEventArgs e)
        {
            // Neues Sachkonto erstellen und speichern
            var ledgerAccount = new LedgerAccount()
            {
                Name = Form.LedgerAccountName.Value,
                Description = Form.Description.Value,
                Tag = Form.Tag.Value,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Guid = Guid.NewGuid().ToString()
            };

            if (e.Context.Request.GetParameter(Form.Image.Name) is ParameterFile file)
            {
                if (ledgerAccount.Media == null)
                {
                    ledgerAccount.Media = new Media()
                    {
                        Name = file.Value,
                        Created = DateTime.Now,
                        Updated = DateTime.Now,
                        Guid = Guid.NewGuid().ToString()
                    };
                }
                else
                {
                    ledgerAccount.Media.Name = file.Value;
                    ledgerAccount.Media.Updated = DateTime.Now;
                }

                File.WriteAllBytes(Path.Combine(e.Context.Application.AssetPath, "media", ledgerAccount.Media.Guid), file.Data);
            }

            lock (ViewModel.Instance.Database)
            {
                ViewModel.Instance.LedgerAccounts.Add(ledgerAccount);
                ViewModel.Instance.SaveChanges();
            }
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Feld LedgerAccountName validiert werden soll.
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente/param>
        private void LedgerAccountNameValidation(object sender, ValidationEventArgs e)
        {
            lock (ViewModel.Instance.Database)
            {
                if (e.Value.Length < 1)
                {
                    e.Results.Add(new ValidationResult() { Text = "inventoryexpress:inventoryexpress.ledgeraccount.validation.name.invalid", Type = TypesInputValidity.Error });
                }
                else if (ViewModel.Instance.LedgerAccounts.Where(x => x.Name.Equals(e.Value)).Any())
                {
                    e.Results.Add(new ValidationResult() { Text = "inventoryexpress:inventoryexpress.ledgeraccount.validation.name.used", Type = TypesInputValidity.Error });
                }
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular initialisiert wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente</param>
        private void InitializeFormular(object sender, FormularEventArgs e)
        {
            Form.RedirectUri = e.Context.Uri.Take(-1);
            Form.BackUri = e.Context.Uri.Take(-1);
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular gefüllt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente</param>
        private void FillFormular(object sender, FormularEventArgs e)
        {
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            context.VisualTree.Content.Primary.Add(Form);
        }
    }
}
