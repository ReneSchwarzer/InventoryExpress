using InventoryExpress.WebControl;
using InventoryExpress.Model;
using System;
using System.Linq;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebResource;
using WebExpress.Attribute;

namespace InventoryExpress.WebResource
{
    [ID("SupplierAdd")]
    [Title("inventoryexpress.supplier.add.label")]
    [Segment("add", "inventoryexpress.supplier.add.label")]
    [Path("/Supplier")]
    [Module("InventoryExpress")]
    [Context("general")]
    public sealed class PageSupplierAdd : PageTemplateWebApp, IPageSupplier
    {
        /// <summary>
        /// Formular
        /// </summary>
        private ControlFormularSupplier form;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageSupplierAdd()
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

            Content.Content.Add(form);

            form.SupplierName.Validation += (s, e) =>
            {
                if (e.Value.Count() < 1)
                {
                    e.Results.Add(new ValidationResult() { Text = "Geben Sie einen gültigen Namen ein!", Type = TypesInputValidity.Error });
                }
                else if (ViewModel.Instance.Suppliers.Where(x => x.Name.Equals(e.Value)).Count() > 0)
                {
                    e.Results.Add(new ValidationResult() { Text = "Der Lieferant wird bereits verwendet. Geben Sie einen anderen Namen an!", Type = TypesInputValidity.Error });
                }
            };

            form.ProcessFormular += (s, e) =>
            {
                // Neues Herstellerobjekt erstellen und speichern
                var supplier = new Supplier()
                {
                    Name = form.SupplierName.Value,
                    //Tag = form.Tag.Value,
                    Description = form.Description.Value,
                    Guid = Guid.NewGuid().ToString()
                };

                ViewModel.Instance.Suppliers.Add(supplier);
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
