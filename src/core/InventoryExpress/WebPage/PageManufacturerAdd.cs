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
    [ID("ManufacturerAdd")]
    [Title("inventoryexpress:inventoryexpress.manufacturer.add.label")]
    [Segment("add", "inventoryexpress:inventoryexpress.manufacturer.add.label")]
    [Path("/Manufacturer")]
    [Module("inventoryexpress")]
    [Context("general")]
    [Context("manufactureradd")]
    public sealed class PageManufacturerAdd : PageWebApp, IPageManufacturer
    {
        /// <summary>
        /// Liefert das Formular
        /// </summary>
        private ControlFormularManufacturer Form { get; } = new ControlFormularManufacturer("manufacturer")
        {
            Edit = false
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageManufacturerAdd()
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
            // Neues Herstellerobjekt erstellen und speichern
            var manufacturer = new Manufacturer()
            {
                Name = Form.ManufacturerName.Value,
                Description = Form.Description.Value,
                Address = Form.Address.Value,
                Zip = Form.Zip.Value,
                Place = Form.Place.Value,
                Tag = Form.Tag.Value,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Guid = Guid.NewGuid().ToString()
            };

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

            lock (ViewModel.Instance.Database)
            {
                ViewModel.Instance.Manufacturers.Add(manufacturer);
                ViewModel.Instance.SaveChanges();
            }
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Feld Zip validiert werden soll.
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente/param>
        private void ZipValidation(object sender, ValidationEventArgs e)
        {
            if (e.Value != null && e.Value.Length >= 10)
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
                if (e.Value.Length < 1)
                {
                    e.Results.Add(new ValidationResult() { Text = "inventoryexpress:inventoryexpress.manufacturer.validation.name.invalid", Type = TypesInputValidity.Error });
                }
                else if (ViewModel.Instance.Manufacturers.Where(x => x.Name.Equals(e.Value)).Any())
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
