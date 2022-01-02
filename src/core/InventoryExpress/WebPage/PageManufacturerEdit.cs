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
    [ID("ManufacturerEdit")]
    [Title("inventoryexpress:inventoryexpress.manufacturer.edit.label")]
    [SegmentGuid("ManufacturerID", "inventoryexpress:inventoryexpress.manufacturer.edit.display", SegmentGuidAttribute.Format.Simple)]
    [Path("/Manufacturer")]
    [Module("inventoryexpress")]
    [Context("general")]
    [Context("manufactureredit")]
    public sealed class PageManufacturerEdit : PageWebApp, IPageManufacturer
    {
        /// <summary>
        /// Liefert das Formular
        /// </summary>
        private ControlFormularManufacturer Form { get; } = new ControlFormularManufacturer("manufacturer")
        {
            Edit = true
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageManufacturerEdit()
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
            Form.ManufacturerName.Validation += ManufacturerNameValidation;
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
                var guid = e.Context.Request.GetParameter("ManufacturerID")?.Value;
                var manufacturer = ViewModel.Instance.Manufacturers.Where(x => x.Guid == guid).FirstOrDefault();

                // Herstellerobjekt ändern und speichern
                manufacturer.Name = Form.ManufacturerName.Value;
                manufacturer.Description = Form.Description.Value;
                manufacturer.Address = Form.Address.Value;
                manufacturer.Zip = Form.Zip.Value;
                manufacturer.Place = Form.Place.Value;
                manufacturer.Tag = Form.Tag.Value;
                manufacturer.Updated = DateTime.Now;

                ViewModel.Instance.SaveChanges();

                if (e.Context.Request.GetParameter(Form.Image.Name) is ParameterFile file)
                {
                    if (manufacturer.Media == null)
                    {
                        manufacturer.Media = new Media()
                        {
                            Name = file.Value,
                            Created = DateTime.Now,
                            Updated = DateTime.Now,
                            Guid = Guid.NewGuid().ToString()
                        };
                    }
                    else
                    {
                        manufacturer.Media.Name = file.Value;
                        manufacturer.Media.Updated = DateTime.Now;
                    }

                    File.WriteAllBytes(Path.Combine(e.Context.Application.AssetPath, "media", manufacturer.Media.Guid), file.Data);
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
                e.Results.Add(new ValidationResult() { Text = "inventoryexpress:inventoryexpress.manufacturer.validation.zip.tolong", Type = TypesInputValidity.Error });
            }
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Feld ManufacturerName validiert werden soll.
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente/param>
        private void ManufacturerNameValidation(object sender, ValidationEventArgs e)
        {
            lock (ViewModel.Instance.Database)
            {
                var guid = e.Context.Request.GetParameter("ManufacturerID")?.Value;
                var manufacturer = ViewModel.Instance.Manufacturers.Where(x => x.Guid == guid).FirstOrDefault();

                if (e.Value.Count() < 1)
                {
                    e.Results.Add(new ValidationResult() { Text = "inventoryexpress:inventoryexpress.manufacturer.validation.name.invalid", Type = TypesInputValidity.Error });
                }
                else if (!manufacturer.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase) && ViewModel.Instance.Suppliers.Where(x => x.Name.Equals(e.Value)).Count() > 0)
                {
                    e.Results.Add(new ValidationResult() { Text = "inventoryexpress:inventoryexpress.manufacturer.validation.name.used", Type = TypesInputValidity.Error });
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
                var guid = e.Context.Request.GetParameter("ManufacturerID")?.Value;
                var manufacturer = ViewModel.Instance.Manufacturers.Where(x => x.Guid == guid).FirstOrDefault();

                Form.ManufacturerName.Value = manufacturer?.Name;
                Form.Description.Value = manufacturer?.Description;
                Form.Address.Value = manufacturer?.Address;
                Form.Zip.Value = manufacturer?.Zip;
                Form.Place.Value = manufacturer?.Place;
                Form.Tag.Value = manufacturer?.Tag;
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
                var guid = context.Request.GetParameter("ManufacturerID")?.Value;
                var manufacturer = ViewModel.Instance.Manufacturers.Where(x => x.Guid == guid).FirstOrDefault();

                context.Request.Uri.Display = manufacturer.Name;
            }

            context.VisualTree.Content.Primary.Add(Form);
        }
    }
}
