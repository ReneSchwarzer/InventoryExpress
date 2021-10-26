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
    [ID("SupplierAdd")]
    [Title("inventoryexpress:inventoryexpress.supplier.add.label")]
    [Segment("add", "inventoryexpress:inventoryexpress.supplier.add.label")]
    [Path("/Supplier")]
    [Module("inventoryexpress")]
    [Context("general")]
    public sealed class PageSupplierAdd : PageWebApp, IPageSupplier
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageSupplierAdd()
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

            var form = new ControlFormularSupplier(this);
            form.EnableCancelButton = true;
            form.RedirectUri = context.Request.Uri.Take(-1);
            form.BackUri = context.Request.Uri.Take(-1);

            form.SupplierName.Validation += (s, e) =>
            {
                if (e.Value.Count() < 1)
                {
                    e.Results.Add(new ValidationResult() { Text = this.I18N("inventoryexpress.supplier.validation.name.invalid"), Type = TypesInputValidity.Error });
                }
                else if (ViewModel.Instance.Suppliers.Where(x => x.Name.Equals(e.Value)).Count() > 0)
                {
                    e.Results.Add(new ValidationResult() { Text = this.I18N("inventoryexpress.supplier.validation.name.used"), Type = TypesInputValidity.Error });
                }
            };

            form.ProcessFormular += (s, e) =>
            {
                // Neuen Liferanten erstellen und speichern
                var supplier = new Supplier()
                {
                    Name = form.SupplierName.Value,
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
                    if (supplier.Media == null)
                    {
                        supplier.Media = new Media()
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
                        supplier.Media.Name = file.Value;
                        supplier.Media.Data = file.Data;
                        supplier.Media.Updated = DateTime.Now;
                    }
                }

                ViewModel.Instance.Suppliers.Add(supplier);
                ViewModel.Instance.SaveChanges();
            };

            context.VisualTree.Content.Primary.Add(form);
        }
    }
}
