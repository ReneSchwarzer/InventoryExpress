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
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var form = new ControlFormularManufacturer(this);
            form.EnableCancelButton = true;
            form.RedirectUri = context.Request.Uri.Take(-1);
            form.BackUri = context.Request.Uri.Take(-1);

            form.ManufacturerName.Validation += (s, e) =>
            {
                if (e.Value.Count() < 1)
                {
                    e.Results.Add(new ValidationResult() { Text = this.I18N("inventoryexpress.manufacturer.validation.name.invalid"), Type = TypesInputValidity.Error });
                }
                else if (ViewModel.Instance.Manufacturers.Where(x => x.Name.Equals(e.Value)).Count() > 0)
                {
                    e.Results.Add(new ValidationResult() { Text = this.I18N("inventoryexpress.manufacturer.validation.name.used"), Type = TypesInputValidity.Error });
                }
            };

            form.Zip.Validation += (s, e) =>
            {
                if (e.Value != null && e.Value.Count() >= 10)
                {
                    e.Results.Add(new ValidationResult() { Text = this.I18N("inventoryexpress.manufacturer.validation.zip.tolong"), Type = TypesInputValidity.Error });
                }
            };

            form.ProcessFormular += (s, e) =>
            {
                // Neues Herstellerobjekt erstellen und speichern
                var manufacturer = new Manufacturer()
                {
                    Name = form.ManufacturerName.Value,
                    Description = form.Description.Value,
                    Address = form.Address.Value,
                    Zip = form.Zip.Value,
                    Place = form.Place.Value,
                    Tag = form.Tag.Value,
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                    Guid = Guid.NewGuid().ToString()
                };

                if (context.Request.GetParameter(form.Image.Name) is ParameterFile file)
                {
                    if (manufacturer.Media == null)
                    {
                        manufacturer.Media = new Media()
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
                        manufacturer.Media.Name = file.Value;
                        manufacturer.Media.Data = file.Data;
                        manufacturer.Media.Updated = DateTime.Now;
                    }
                }

                ViewModel.Instance.Manufacturers.Add(manufacturer);
                ViewModel.Instance.SaveChanges();
            };

            context.VisualTree.Content.Primary.Add(form);
        }
    }
}
