using InventoryExpress.Model;
using InventoryExpress.WebControl;
using System;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebResource;

namespace InventoryExpress.WebResource
{
    [ID("InventoryAdd")]
    [Title("inventoryexpress.inventory.add.label")]
    [Segment("add", "inventoryexpress.inventory.add.label")]
    [Path("/")]
    [Module("InventoryExpress")]
    [Context("general")]
    public sealed class PageInventoryAdd : PageTemplateWebApp, IPageInventory
    {
        /// <summary>
        /// Formular
        /// </summary>
        private ControlFormularInventory form;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageInventoryAdd()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Initialization()
        {
            base.Initialization();

            form = new ControlFormularInventory()
            {
                RedirectUri = Uri.Take(-1),
                EnableCancelButton = true,
                BackUri = Uri.Take(-1),
            };
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();

            Content.Primary.Add(form);

            form.InventoryName.Validation += (s, e) =>
            {
                if (e.Value.Count() < 1)
                {
                    e.Results.Add(new ValidationResult() { Text = "Geben Sie einen gültigen Namen ein!", Type = TypesInputValidity.Error });
                }
                else if (ViewModel.Instance.Inventories.Where(x => x.Name.Equals(e.Value)).Count() > 0)
                {
                    e.Results.Add(new ValidationResult() { Text = "Der Name wird bereits verwendet. Geben Sie einen anderen Namen an!", Type = TypesInputValidity.Error });
                }
            };

            form.ProcessFormular += (s, e) =>
            {
                // Neues Herstellerobjekt erstellen und speichern
                var inventory = new Inventory()
                {
                    Name = form.InventoryName.Value,
                    Manufacturer = ViewModel.Instance.Manufacturers.Where(x => x.Guid == form.Manufacturer.Value).FirstOrDefault(),
                    Location = ViewModel.Instance.Locations.Where(x => x.Guid == form.Location.Value).FirstOrDefault(),
                    Supplier = ViewModel.Instance.Suppliers.Where(x => x.Guid == form.Supplier.Value).FirstOrDefault(),
                    LedgerAccount = ViewModel.Instance.LedgerAccounts.Where(x => x.Guid == form.LedgerAccount.Value).FirstOrDefault(),
                    CostCenter = ViewModel.Instance.CostCenters.Where(x => x.Guid == form.CostCenter.Value).FirstOrDefault(),
                    Condition = ViewModel.Instance.Conditions.Where(x => x.Guid == form.Condition.Value).FirstOrDefault(),
                    //Parent = form.Parent.Value,
                    Template = ViewModel.Instance.Templates.Where(x => x.Guid == form.Template.Value).FirstOrDefault(),
                    Tag = form.Tag.Value,
                    Description = form.InventoryName.Value,
                    Guid = Guid.NewGuid().ToString()
                };

                ViewModel.Instance.Inventories.Add(inventory);
                ViewModel.Instance.SaveChanges();
            };
        }
    }
}
