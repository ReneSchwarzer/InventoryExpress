using InventoryExpress.Model;
using InventoryExpress.Model.Entity;
using InventoryExpress.WebControl;
using System;
using System.IO;
using System.Linq;
using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
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
        /// Liefert das Formular
        /// </summary>
        private ControlFormularLedgerAccount Form { get; } = new ControlFormularLedgerAccount("ledgeraccount")
        {
            Edit = true
        };

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
            lock (ViewModel.Instance.Database)
            {
                var guid = e.Context.Request.GetParameter("LedgerAccountID")?.Value;
                var ledgerAccount = ViewModel.Instance.LedgerAccounts.Where(x => x.Guid == guid).FirstOrDefault();

                // Sachkonto ändern und speichern
                ledgerAccount.Name = Form.LedgerAccountName.Value;
                ledgerAccount.Description = Form.Description.Value;
                ledgerAccount.Tag = Form.Tag.Value;
                ledgerAccount.Updated = DateTime.Now;

                ViewModel.Instance.SaveChanges();

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
                var guid = e.Context.Request.GetParameter("LedgerAccountID")?.Value;
                var ledgerAccount = ViewModel.Instance.LedgerAccounts.Where(x => x.Guid == guid).FirstOrDefault();

                if (e.Value.Count() < 1)
                {
                    e.Results.Add(new ValidationResult() { Text = this.I18N("inventoryexpress.ledgeraccount.validation.name.invalid"), Type = TypesInputValidity.Error });
                }
                else if (!ledgerAccount.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase) && ViewModel.Instance.Suppliers.Where(x => x.Name.Equals(e.Value)).Count() > 0)
                {
                    e.Results.Add(new ValidationResult() { Text = this.I18N("inventoryexpress.ledgeraccount.validation.name.used"), Type = TypesInputValidity.Error });
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
            lock (ViewModel.Instance.Database)
            {
                var guid = e.Context.Request.GetParameter("LedgerAccountID")?.Value;
                var ledgerAccount = ViewModel.Instance.LedgerAccounts.Where(x => x.Guid == guid).FirstOrDefault();

                Form.LedgerAccountName.Value = ledgerAccount?.Name;
                Form.Description.Value = ledgerAccount?.Description;
                Form.Tag.Value = ledgerAccount?.Tag;
            }
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            lock (ViewModel.Instance.Database)
            {
                var guid = context.Request.GetParameter("LedgerAccountID")?.Value;
                var ledgerAccount = ViewModel.Instance.LedgerAccounts.Where(x => x.Guid == guid).FirstOrDefault();

                context.Request.Uri.Display = ledgerAccount.Name;
                context.VisualTree.Content.Primary.Add(Form);
            }
        }
    }
}
