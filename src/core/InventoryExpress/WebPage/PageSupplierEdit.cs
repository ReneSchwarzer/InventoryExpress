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
    [ID("SupplierEdit")]
    [Title("inventoryexpress:inventoryexpress.supplier.edit.label")]
    [SegmentGuid("SupplierID", "inventoryexpress:inventoryexpress.supplier.edit.display")]
    [Path("/Supplier")]
    [Module("inventoryexpress")]
    [Context("general")]
    [Context("supplieredit")]
    public sealed class PageSupplierEdit : PageWebApp, IPageSupplier
    {
        /// <summary>
        /// Liefert das Formular
        /// </summary>
        private ControlFormularSupplier Form { get; } = new ControlFormularSupplier("supplier")
        {
            Edit = true
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageSupplierEdit()
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
            Form.SupplierName.Validation += SupplierNameValidation;
            Form.Zip.Validation += ZipValidation;
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
                var guid = e.Context.Request.GetParameter("SupplierID")?.Value;
                var supplier = ViewModel.Instance.Suppliers.Where(x => x.Guid == guid).FirstOrDefault();

                // Lieferant ändern und speichern
                supplier.Name = Form.SupplierName.Value;
                supplier.Description = Form.Description.Value;
                supplier.Address = Form.Address.Value;
                supplier.Zip = Form.Zip.Value;
                supplier.Place = Form.Place.Value;
                supplier.Updated = DateTime.Now;
                supplier.Tag = Form.Tag.Value;

                ViewModel.Instance.SaveChanges();

                if (e.Context.Request.GetParameter(Form.Image.Name) is ParameterFile file)
                {
                    if (supplier.Media == null)
                    {
                        supplier.Media = new Media()
                        {
                            Name = file.Value,
                            Created = DateTime.Now,
                            Updated = DateTime.Now,
                            Guid = Guid.NewGuid().ToString()
                        };
                    }
                    else
                    {
                        supplier.Media.Name = file.Value;
                        supplier.Media.Updated = DateTime.Now;
                    }

                    File.WriteAllBytes(Path.Combine(e.Context.Application.AssetPath, "media", supplier.Media.Guid), file.Data);
                }
            }
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
                e.Results.Add(new ValidationResult() { Text = "inventoryexpress:inventoryexpress.supplier.validation.zip.tolong", Type = TypesInputValidity.Error });
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
                var guid = e.Context.Request.GetParameter("SupplierID")?.Value;
                var supplier = ViewModel.Instance.Suppliers.Where(x => x.Guid == guid).FirstOrDefault();

                if (e.Value.Count() < 1)
                {
                    e.Results.Add(new ValidationResult() { Text = "inventoryexpress:inventoryexpress.supplier.validation.name.invalid", Type = TypesInputValidity.Error });
                }
                else if (!supplier.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase) && ViewModel.Instance.Suppliers.Where(x => x.Name.Equals(e.Value)).Count() > 0)
                {
                    e.Results.Add(new ValidationResult() { Text = "inventoryexpress:inventoryexpress.supplier.validation.name.used", Type = TypesInputValidity.Error });
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
                var guid = e.Context.Request.GetParameter("SupplierID")?.Value;
                var supplier = ViewModel.Instance.Suppliers.Where(x => x.Guid == guid).FirstOrDefault();

                Form.SupplierName.Value = supplier?.Name;
                Form.Description.Value = supplier?.Description;
                Form.Address.Value = supplier?.Address;
                Form.Zip.Value = supplier?.Zip;
                Form.Place.Value = supplier?.Place;
                Form.Tag.Value = supplier?.Tag;
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
                var guid = context.Request.GetParameter("SupplierID")?.Value;
                var supplier = ViewModel.Instance.Suppliers.Where(x => x.Guid == guid).FirstOrDefault();

                context.Request.Uri.Display = supplier.Name;
                context.VisualTree.Content.Primary.Add(Form);
            }
        }
    }
}
