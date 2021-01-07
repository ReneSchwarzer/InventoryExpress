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
        private ControlFormularInventory Form;

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

            Form = new ControlFormularInventory("inventory")
            {
                RedirectUri = Uri.Take(-1),
                Edit = true,
                EnableCancelButton = true,
                BackUri = Uri.Take(-1)
            };

            Form.InitializeFormular += (s, e) =>
            {
                Form.InventoryName.Validation += (s, e) =>
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

                Form.CostValue.Validation += (s, e) =>
                {
                    try
                    {
                        if (Convert.ToDecimal(Form.CostValue.Value, Culture) < 0)
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

            Form.FillFormular += (s, e) =>
            {
                Form.InventoryName.Value = inventory?.Name;
                Form.Manufacturer.Value = ViewModel.Instance.Manufacturers.Where(x => x.Id == inventory.ManufacturerId).FirstOrDefault()?.Guid;
                Form.Location.Value = ViewModel.Instance.Locations.Where(x => x.Id == inventory.LocationId).FirstOrDefault()?.Guid;
                Form.Supplier.Value = ViewModel.Instance.Suppliers.Where(x => x.Id == inventory.SupplierId).FirstOrDefault()?.Guid;
                Form.LedgerAccount.Value = ViewModel.Instance.LedgerAccounts.Where(x => x.Id == inventory.LedgerAccountId).FirstOrDefault()?.Guid;
                Form.CostCenter.Value = ViewModel.Instance.CostCenters.Where(x => x.Id == inventory.CostCenterId).FirstOrDefault()?.Guid;
                Form.Condition.Value = ViewModel.Instance.Conditions.Where(x => x.Id == inventory.ConditionId).FirstOrDefault()?.Guid;
                Form.Parent.Value = ViewModel.Instance.Inventories.Where(x => x.Id == inventory.ParentId).FirstOrDefault()?.Guid;
                Form.Template.Value = ViewModel.Instance.Templates.Where(x => x.Id == inventory.TemplateId)?.FirstOrDefault()?.Guid;
                Form.CostValue.Value = inventory.CostValue.ToString(Culture);
                Form.PurchaseDate.Value = inventory.PurchaseDate.HasValue ? inventory.PurchaseDate.Value.ToString(Culture.DateTimeFormat.ShortDatePattern) : null;
                Form.DerecognitionDate.Value = inventory.DerecognitionDate.HasValue ? inventory.DerecognitionDate.Value.ToString(Culture.DateTimeFormat.ShortDatePattern) : null;
                Form.Tag.Value = inventory?.Tag;
                Form.Description.Value = inventory?.Description;
            };

            Form.ProcessFormular += (s, e) =>
            {
                // Neues Herstellerobjekt erstellen und speichern
                inventory.Name = Form.InventoryName.Value;
                inventory.Manufacturer = ViewModel.Instance.Manufacturers.Where(x => x.Guid == Form.Manufacturer.Value).FirstOrDefault();
                inventory.Location = ViewModel.Instance.Locations.Where(x => x.Guid == Form.Location.Value).FirstOrDefault();
                inventory.Supplier = ViewModel.Instance.Suppliers.Where(x => x.Guid == Form.Supplier.Value).FirstOrDefault();
                inventory.LedgerAccount = ViewModel.Instance.LedgerAccounts.Where(x => x.Guid == Form.LedgerAccount.Value).FirstOrDefault();
                inventory.CostCenter = ViewModel.Instance.CostCenters.Where(x => x.Guid == Form.CostCenter.Value).FirstOrDefault();
                inventory.Condition = ViewModel.Instance.Conditions.Where(x => x.Guid == Form.Condition.Value).FirstOrDefault();
                inventory.Parent = ViewModel.Instance.Inventories.Where(x => x.Guid == Form.Parent.Value).FirstOrDefault();
                inventory.Template = ViewModel.Instance.Templates.Where(x => x.Guid == Form.Template.Value).FirstOrDefault();
                inventory.CostValue = !string.IsNullOrWhiteSpace(Form.CostValue.Value) ? Convert.ToDecimal(Form.CostValue.Value, Culture) : 0;
                inventory.PurchaseDate = !string.IsNullOrWhiteSpace(Form.PurchaseDate.Value) ? Convert.ToDateTime(Form.PurchaseDate.Value, Culture) : null;
                inventory.DerecognitionDate = !string.IsNullOrWhiteSpace(Form.DerecognitionDate.Value) ? Convert.ToDateTime(Form.DerecognitionDate.Value, Culture) : null;
                inventory.Tag = Form.Tag.Value;
                inventory.Description = Form.Description.Value;

                ViewModel.Instance.SaveChanges();
            };

            Content.Primary.Add(Form);
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
