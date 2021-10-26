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
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var guid = context.Request.GetParameter("SupplierID")?.Value;
            var supplier = ViewModel.Instance.Suppliers.Where(x => x.Guid == guid).FirstOrDefault();
            var form = new ControlFormularSupplier(this);

            form.Edit = true;
            form.RedirectUri = context.Uri.Take(-1);
            form.BackUri = context.Uri.Take(-1);
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

            context.VisualTree.Content.Primary.Add(form);
        }
    }
}
