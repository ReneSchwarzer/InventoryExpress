using InventoryExpress.Model;
using InventoryExpress.WebControl;
using System;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebPage;
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
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var guid = context.Request.GetParameter("ManufacturerID")?.Value;
            var manufacturer = ViewModel.Instance.Manufacturers.Where(x => x.Guid == guid).FirstOrDefault();
            var form = new ControlFormularManufacturer(this);

            form.Edit = true;
            form.RedirectUri = context.Uri.Take(-1);
            form.BackUri = context.Uri.Take(-1);
            form.ManufacturerName.Value = manufacturer?.Name;
            form.Description.Value = manufacturer?.Description;
            form.Address.Value = manufacturer?.Address;
            form.Zip.Value = manufacturer?.Zip;
            form.Place.Value = manufacturer?.Place;
            form.Tag.Value = manufacturer?.Tag;

            form.ManufacturerName.Validation += (s, e) =>
            {
                if (e.Value.Count() < 1)
                {
                    e.Results.Add(new ValidationResult() { Text = this.I18N("inventoryexpress.manufacturer.validation.name.invalid"), Type = TypesInputValidity.Error });
                }
                else if (!manufacturer.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase) && ViewModel.Instance.Suppliers.Where(x => x.Name.Equals(e.Value)).Count() > 0)
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
                // Herstellerobjekt ändern und speichern
                manufacturer.Name = form.ManufacturerName.Value;
                manufacturer.Description = form.Description.Value;
                manufacturer.Address = form.Address.Value;
                manufacturer.Zip = form.Zip.Value;
                manufacturer.Place = form.Place.Value;
                manufacturer.Tag = form.Tag.Value;
                manufacturer.Updated = DateTime.Now;

                ViewModel.Instance.SaveChanges();
            };

            context.VisualTree.Content.Primary.Add(form);
        }
    }
}
