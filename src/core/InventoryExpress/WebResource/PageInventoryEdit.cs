using InventoryExpress.Model;
using InventoryExpress.WebControl;
using System;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebResource;

namespace InventoryExpress.WebResource
{
    [ID("InventoryEdit")]
    [Title("inventoryexpress.inventory.edit.label")]
    [Segment("edit", "inventoryexpress.inventory.edit.display")]
    [Path("/Details")]
    [Module("InventoryExpress")]
    [Context("general")]
    public sealed class PageInventoryEdit : PageTemplateWebApp, IPageInventory
    {
        /// <summary>
        /// Formular
        /// </summary>
        private ControlFormularInventory form;


        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageInventoryEdit()
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
                RedirectUrl = Uri.Take(-1)
            };

            Uri.Display = GetParamValue("InventoryID").Split('-').LastOrDefault();
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();

            var guid = GetParamValue("InventoryID");
            var inventory = ViewModel.Instance.Inventories.Where(x => x.Guid == guid).FirstOrDefault();

            Content.Primary.Add(form);

            form.InventoryName.Value = inventory?.Name;
            form.Description.Value = inventory?.Description;
            form.Manufacturer.Value = inventory?.Manufacturer?.Guid;
            form.Location.Value = inventory?.Location?.Guid;
            form.Supplier.Value = inventory?.Supplier?.Guid;
            form.LedgerAccount.Value = inventory?.LedgerAccount?.Guid;
            form.CostCenter.Value = inventory?.CostCenter?.Guid;
            form.Condition.Value = inventory?.Condition?.Guid;
            form.Parent.Value = inventory?.Parent;
            form.Template.Value = inventory?.Template?.Guid;

            form.InventoryName.Validation += (s, e) =>
            {
                if (e.Value.Count() < 1)
                {
                    e.Results.Add(new ValidationResult() { Text = "Geben Sie einen gültigen Namen ein!", Type = TypesInputValidity.Error });
                }
                else if (!inventory.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase) && ViewModel.Instance.Inventories.Where(x => x.Name.Equals(e.Value)).Count() > 0)
                {
                    e.Results.Add(new ValidationResult() { Text = "Der Name wird bereits verwendet. Geben Sie einen anderen Namen an!", Type = TypesInputValidity.Error });
                }
            };

            form.ProcessFormular += (s, e) =>
            {
                // Neues Herstellerobjekt erstellen und speichern
                inventory.Name = form.InventoryName.Value;
                inventory.Manufacturer = ViewModel.Instance.Manufacturers.Where(x => x.Guid == form.Manufacturer.Value).FirstOrDefault();
                inventory.Location = ViewModel.Instance.Locations.Where(x => x.Guid == form.Location.Value).FirstOrDefault();
                inventory.Supplier = ViewModel.Instance.Suppliers.Where(x => x.Guid == form.Supplier.Value).FirstOrDefault();
                inventory.LedgerAccount = ViewModel.Instance.LedgerAccounts.Where(x => x.Guid == form.LedgerAccount.Value).FirstOrDefault();
                inventory.CostCenter = ViewModel.Instance.CostCenters.Where(x => x.Guid == form.CostCenter.Value).FirstOrDefault();
                inventory.Condition = ViewModel.Instance.Conditions.Where(x => x.Guid == form.Condition.Value).FirstOrDefault();
                inventory.Parent = form.Parent.Value;
                inventory.Template = ViewModel.Instance.Templates.Where(x => x.Guid == form.Template.Value).FirstOrDefault();
                //Tag = form.Tag.Value,
                inventory.Description = form.Description.Value;

                ViewModel.Instance.SaveChanges();
            };
        }
    }
}
