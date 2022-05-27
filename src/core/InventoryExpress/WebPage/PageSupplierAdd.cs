using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using InventoryExpress.WebControl;
using System.IO;
using System.Linq;
using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [ID("SupplierAdd")]
    [Title("inventoryexpress:inventoryexpress.supplier.add.label")]
    [Segment("add", "inventoryexpress:inventoryexpress.supplier.add.label")]
    [Path("/Supplier")]
    [Module("inventoryexpress")]
    [Context("general")]
    public sealed class PageSupplierAdd : PageWebApp, IPageSupplier
    {
        /// <summary>
        /// Liefert das Formular
        /// </summary>
        private ControlFormularSupplier Form { get; } = new ControlFormularSupplier("supplier")
        {
            Edit = false
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageSupplierAdd()
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
            Form.ProcessFormular += ProcessFormular;
            Form.RedirectUri = context.Application.ContextPath.Append("suppliers");
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular initialisiert wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente</param>
        private void InitializeFormular(object sender, FormularEventArgs e)
        {
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
        /// Wird ausgelöst, wenn das Formular verarbeitet werden soll.
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente/param>
        private void ProcessFormular(object sender, FormularEventArgs e)
        {
            var file = e.Context.Request.GetParameter(Form.Image.Name) as ParameterFile;

            // Neuen Liferanten erstellen und speichern
            var supplier = new WebItemEntitySupplier()
            {
                Name = Form.SupplierName.Value,
                Description = Form.Description.Value,
                Address = Form.Address.Value,
                Zip = Form.Zip.Value,
                Place = Form.Place.Value,
                Tag = Form.Tag.Value,
                Media = new WebItemEntityMedia()
                {
                    Name = file?.Value
                }
            };

            ViewModel.AddOrUpdateSupplier(supplier);
            ViewModel.Instance.SaveChanges();

            if (file != null)
            {
                ViewModel.AddOrUpdateMedia(supplier.Media, file?.Data);
                ViewModel.Instance.SaveChanges();
            }

            NotificationManager.CreateNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(Culture, "inventoryexpress:inventoryexpress.supplier.notification.add"),
                    new ControlLink()
                    {
                        Text = supplier.Name,
                        Uri = new UriRelative(ViewModel.GetSupplierUri(supplier.ID))
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: new UriRelative(supplier.Image),
                durability: 10000
            );

            Form.RedirectUri = Form.RedirectUri.Append(supplier.ID);
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Feld Zip validiert werden soll.
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente/param>
        private void ZipValidation(object sender, ValidationEventArgs e)
        {
            if (e.Value != null && e.Value.Count() >= 10)
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.supplier.validation.zip.tolong"));
            }
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Feld SupplierName validiert werden soll.
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente/param>
        private void SupplierNameValidation(object sender, ValidationEventArgs e)
        {
            lock (ViewModel.Instance.Database)
            {
                if (e.Value.Length < 1)
                {
                    e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.supplier.validation.name.invalid"));
                }
                else if (ViewModel.Instance.Suppliers.Where(x => x.Name.Equals(e.Value)).Any())
                {
                    e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.supplier.validation.name.used"));
                }
            }
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
