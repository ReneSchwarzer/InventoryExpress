using InventoryExpress.WebControl;
using InventoryExpress.Model;
using System;
using System.Linq;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebResource;
using WebExpress.Attribute;

namespace InventoryExpress.WebResource
{
    [ID("SupplierEdit")]
    [Title("inventoryexpress.supplier.edit.label")]
    [SegmentGuid("SupplierID", "inventoryexpress.supplier.edit.display")]
    [Path("/Supplier")]
    [Module("InventoryExpress")]
    [Context("general")]
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
                RedirectUrl = Uri.Take(-1)
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

            Content.Content.Add(form);

            form.SupplierName.Value = supplier?.Name;
            form.Description.Value = supplier?.Description;

            form.SupplierName.Validation += (s, e) =>
            {
                if (e.Value.Count() < 1)
                {
                    e.Results.Add(new ValidationResult() { Text = "Geben Sie einen gültigen Namen ein!", Type = TypesInputValidity.Error });
                }
                else if (!supplier.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase) && ViewModel.Instance.Suppliers.Where(x => x.Name.Equals(e.Value)).Count() > 0)
                {
                    e.Results.Add(new ValidationResult() { Text = "Der Lieferant wird bereits verwendet. Geben Sie einen anderen Namen an!", Type = TypesInputValidity.Error });
                }
            };

            form.ProcessFormular += (s, e) =>
            {
                // Herstellerobjekt ändern und speichern
                supplier.Name = form.SupplierName.Value;
                //Tag = form.Tag.Value;
                supplier.Description = form.Description.Value;

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
