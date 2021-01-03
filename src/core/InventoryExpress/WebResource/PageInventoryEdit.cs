using InventoryExpress.Model;
using InventoryExpress.WebControl;
using System;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Internationalization;
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
    [Context("inventoryedit")]
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

            var guid = GetParamValue("InventoryID");
            var inventory = ViewModel.Instance.Inventories.Where(x => x.Guid == guid).FirstOrDefault();

            form = new ControlFormularInventory("inventory")
            {
                RedirectUri = Uri.Take(-1),
                Edit = true,
                EnableCancelButton = true,
                BackUri = Uri.Take(-1)
            };

            form.InitializeFormular += (s, e) =>
            {
                form.InventoryName.Validation += (s, e) =>
                {
                    if (e.Value.Count() < 1)
                    {
                        e.Results.Add(new ValidationResult() { Text = this.I18N("inventoryexpress.inventory.validation.name.invalid"), Type = TypesInputValidity.Error });
                    }
                    else if (!inventory.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase) && ViewModel.Instance.Inventories.Where(x => x.Name.Equals(e.Value)).Count() > 0)
                    {
                        e.Results.Add(new ValidationResult() { Text = this.I18N("inventoryexpress.inventory.validation.name.used"), Type = TypesInputValidity.Error });
                    }
                };

                form.CostValue.Validation += (s, e) =>
                {
                    try
                    {
                        if (Convert.ToDecimal(form.CostValue.Value, Culture) < 0)
                        {
                            e.Results.Add(new ValidationResult() { Text = this.I18N("inventoryexpress.inventory.validation.costvalue.negativ"), Type = TypesInputValidity.Error });
                        };
                    }
                    catch
                    {
                        e.Results.Add(new ValidationResult() { Text = this.I18N("inventoryexpress.inventory.validation.costvalue.invalid"), Type = TypesInputValidity.Error });
                    }
                };
            };

            form.FillFormular += (s, e) =>
            {
                form.InventoryName.Value = inventory?.Name;
                form.Manufacturer.Value = ViewModel.Instance.Manufacturers.Where(x => x.Id == inventory.ManufacturerId).FirstOrDefault()?.Guid;
                form.Location.Value = ViewModel.Instance.Locations.Where(x => x.Id == inventory.LocationId).FirstOrDefault()?.Guid;
                form.Supplier.Value = ViewModel.Instance.Suppliers.Where(x => x.Id == inventory.SupplierId).FirstOrDefault()?.Guid;
                form.LedgerAccount.Value = ViewModel.Instance.LedgerAccounts.Where(x => x.Id == inventory.LedgerAccountId).FirstOrDefault()?.Guid;
                form.CostCenter.Value = ViewModel.Instance.CostCenters.Where(x => x.Id == inventory.CostCenterId).FirstOrDefault()?.Guid;
                form.Condition.Value = ViewModel.Instance.Conditions.Where(x => x.Id == inventory.ConditionId).FirstOrDefault()?.Guid;
                form.Parent.Value = ViewModel.Instance.Inventories.Where(x => x.Id == inventory.ParentId).FirstOrDefault()?.Guid;
                form.Template.Value = ViewModel.Instance.Templates.Where(x => x.Id == inventory.TemplateId)?.FirstOrDefault()?.Guid;
                form.CostValue.Value = inventory.CostValue.ToString(Culture);
                form.PurchaseDate.Value = inventory.PurchaseDate.HasValue ? inventory.PurchaseDate.Value.ToString(Culture.DateTimeFormat.ShortDatePattern) : null;
                form.DerecognitionDate.Value = inventory.DerecognitionDate.HasValue ? inventory.DerecognitionDate.Value.ToString(Culture.DateTimeFormat.ShortDatePattern) : null;
                form.Tag.Value = inventory?.Tag;
                form.Description.Value = inventory?.Description;
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
                inventory.Parent = ViewModel.Instance.Inventories.Where(x => x.Guid == form.Parent.Value).FirstOrDefault();
                inventory.Template = ViewModel.Instance.Templates.Where(x => x.Guid == form.Template.Value).FirstOrDefault();
                inventory.CostValue = !string.IsNullOrWhiteSpace(form.CostValue.Value) ? Convert.ToDecimal(form.CostValue.Value, Culture) : 0;
                inventory.PurchaseDate = !string.IsNullOrWhiteSpace(form.PurchaseDate.Value) ? Convert.ToDateTime(form.PurchaseDate.Value, Culture) : null;
                inventory.DerecognitionDate = !string.IsNullOrWhiteSpace(form.DerecognitionDate.Value) ? Convert.ToDateTime(form.DerecognitionDate.Value, Culture) : null;
                inventory.Tag = form.Tag.Value;
                inventory.Description = form.Description.Value;

                ViewModel.Instance.SaveChanges();
            };

            Content.Primary.Add(form);
            Uri.Display = GetParamValue("InventoryID").Split('-').LastOrDefault();
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();
        }
    }
}
