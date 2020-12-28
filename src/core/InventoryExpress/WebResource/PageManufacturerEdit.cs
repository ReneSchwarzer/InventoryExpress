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
    [ID("ManufacturerEdit")]
    [Title("inventoryexpress.manufacturer.edit.label")]
    [SegmentGuid("ManufacturerID", "inventoryexpress.manufacturer.edit.display", SegmentGuidAttribute.Format.Simple)]
    [Path("/Manufacturer")]
    [Module("InventoryExpress")]
    [Context("general")]
    [Context("manufactureredit")]
    public sealed class PageManufacturerEdit : PageTemplateWebApp, IPageManufacturer
    {
        /// <summary>
        /// Formular
        /// </summary>
        private ControlFormularManufacturer form;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageManufacturerEdit()
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
                RedirectUrl = Uri.Take(-1),
                Edit = true
            };
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();

            var guid = GetParamValue("ManufacturerID");
            var manufacturer = ViewModel.Instance.Manufacturers.Where(x => x.Guid == guid).FirstOrDefault();

            Content.Primary.Add(form);

            form.ManufacturerName.Value = manufacturer?.Name;
            form.Description.Value = manufacturer?.Description;
            form.Address.Value = manufacturer?.Address;
            form.Zip.Value = manufacturer?.Zip;
            form.Place.Value = manufacturer?.Place;
            form.Tag.Value = "";

            form.ManufacturerName.Validation += (s, e) =>
            {
                if (e.Value.Count() < 1)
                {
                    e.Results.Add(new ValidationResult() { Text = "Geben Sie einen gültigen Namen ein!", Type = TypesInputValidity.Error });
                }
                else if (!manufacturer.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase) && ViewModel.Instance.Suppliers.Where(x => x.Name.Equals(e.Value)).Count() > 0)
                {
                    e.Results.Add(new ValidationResult() { Text = "Der Hersteller wird bereits verwendet. Geben Sie einen anderen Namen an!", Type = TypesInputValidity.Error });
                }
            };

            form.Zip.Validation += (s, e) =>
            {
                if (e.Value.Count() < 1)
                {
                    e.Results.Add(new ValidationResult() { Text = "Geben Sie einen gültigen Namen ein!", Type = TypesInputValidity.Error });
                }
                else if (!manufacturer.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase) && ViewModel.Instance.Suppliers.Where(x => x.Name.Equals(e.Value)).Count() > 0)
                {
                    e.Results.Add(new ValidationResult() { Text = "Der Hersteller wird bereits verwendet. Geben Sie einen anderen Namen an!", Type = TypesInputValidity.Error });
                }
            };

            form.ProcessFormular += (s, e) =>
            {
                // Herstellerobjekt ändern und speichern
                manufacturer.Name = form.ManufacturerName.Value;
                manufacturer.Description = form.Description.Value;
                manufacturer.Address = form.Address.Value;
                manufacturer.Zip = form.Zip.Value;
                manufacturer.Place = form.Place.Value;
                manufacturer.Updated = DateTime.Now;
                //manufacturer.Tag = form.Tag.Value;

                ViewModel.Instance.SaveChanges();
            };
        }
    }
}
