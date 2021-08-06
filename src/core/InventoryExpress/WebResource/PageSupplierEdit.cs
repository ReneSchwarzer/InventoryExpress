using InventoryExpress.WebControl;
using InventoryExpress.Model;
using System;
using System.Linq;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebResource;
using WebExpress.Attribute;
using WebExpress.Message;
using WebExpress.Internationalization;

namespace InventoryExpress.WebResource
{
    [ID("SupplierEdit")]
    [Title("inventoryexpress.supplier.edit.label")]
    [SegmentGuid("SupplierID", "inventoryexpress.supplier.edit.display")]
    [Path("/Supplier")]
    [Module("InventoryExpress")]
    [Context("general")]
    [Context("supplieredit")]
    public sealed class PageSupplierEdit : PageTemplateWebApp, IPageSupplier
    {
        /// <summary>
        /// Formular
        /// </summary>
        private ControlFormularSupplier form;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageSupplierEdit()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Initialization()
        {
            base.Initialization();

            form = new ControlFormularSupplier(this)
            {
                RedirectUri = Uri.Take(-1),
                Edit = true,
                BackUri = Uri.Take(-1),
            };
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();

            var guid = GetParamValue("SupplierID");
            var supplier = ViewModel.Instance.Suppliers.Where(x => x.Guid == guid).FirstOrDefault();

            Content.Primary.Add(form);

            form.SupplierName.Value = supplier?.Name;
            form.Description.Value = supplier?.Description;
            form.Address.Value = supplier?.Address;
            form.Zip.Value = supplier?.Zip;
            form.Place.Value = supplier?.Place;
            form.Tag.Value = supplier?.Tag;

            form.SupplierName.Validation += (s, e) =>
            {
                if (e.Value.Count() < 1)
                {
                    e.Results.Add(new ValidationResult() { Text = this.I18N("inventoryexpress.supplier.validation.name.invalid"), Type = TypesInputValidity.Error });
                }
                else if (!supplier.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase) && ViewModel.Instance.Suppliers.Where(x => x.Name.Equals(e.Value)).Count() > 0)
                {
                    e.Results.Add(new ValidationResult() { Text = this.I18N("inventoryexpress.supplier.validation.name.used"), Type = TypesInputValidity.Error });
                }
            };

            form.ProcessFormular += (s, e) =>
            {
                // Lieferant ändern und speichern
                supplier.Name = form.SupplierName.Value;
                supplier.Description = form.Description.Value;
                supplier.Address = form.Address.Value;
                supplier.Zip = form.Zip.Value;
                supplier.Place = form.Place.Value;
                supplier.Updated = DateTime.Now;
                supplier.Tag = form.Tag.Value;

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
