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
    [ID("ManufacturerAdd")]
    [Title("inventoryexpress.manufacturer.add.label")]
    [Segment("add", "inventoryexpress.manufacturer.add.label")]
    [Path("/Manufacturer")]
    [Module("InventoryExpress")]
    [Context("general")]
    public sealed class PageManufacturerAdd : PageTemplateWebApp, IPageManufacturer
    {
        /// <summary>
        /// Formular
        /// </summary>
        private ControlFormularManufacturer form;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageManufacturerAdd()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Initialization()
        {
            base.Initialization();

            form = new ControlFormularManufacturer(this)
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

            form.ManufacturerName.Validation += (s, e) =>
            {
                if (e.Value.Count() < 1)
                {
                    e.Results.Add(new ValidationResult() { Text = "Geben Sie einen gültigen Namen ein!", Type = TypesInputValidity.Error });
                }
                else if (ViewModel.Instance.Manufacturers.Where(x => x.Name.Equals(e.Value)).Count() > 0)
                {
                    e.Results.Add(new ValidationResult() { Text = "Der Hersteller wird bereits verwendet. Geben Sie einen anderen Namen an!", Type = TypesInputValidity.Error });
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
                    //Tag = form.Tag.Value,
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                    Guid = Guid.NewGuid().ToString()
                };

                if (GetParam(form.Image.Name) is ParameterFile file)
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
