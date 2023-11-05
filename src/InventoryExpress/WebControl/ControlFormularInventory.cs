using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using InventoryExpress.Parameter;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using WebExpress.WebApp.WebApiControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebUI.WebControl;

namespace InventoryExpress.WebControl
{
    public class ControlFormularInventory : ControlForm
    {
        /// <summary>
        /// Returns the name of the inventory item.
        /// </summary>
        public ControlFormItemInputTextBox InventoryName { get; } = new ControlFormItemInputTextBox()
        {
            Name = "name",
            Label = "inventoryexpress:inventoryexpress.inventory.name.label",
            Help = "inventoryexpress:inventoryexpress.inventory.name.description",
            Icon = new PropertyIcon(TypeIcon.Font),
            Format = TypesEditTextFormat.Default
        };

        /// <summary>
        /// Returns the manufacturer.
        /// </summary>
        public ControlApiFormularItemInputSelection Manufacturer { get; } = new ControlApiFormularItemInputSelection()
        {
            Name = "manufacturer",
            Label = "inventoryexpress:inventoryexpress.inventory.manufacturer.label",
            Help = "inventoryexpress:inventoryexpress.inventory.manufacturer.description",
            Icon = new PropertyIcon(TypeIcon.Industry),
            MultiSelect = false
        };

        /// <summary>
        /// Returns the location.
        /// </summary>
        public ControlApiFormularItemInputSelection Location { get; } = new ControlApiFormularItemInputSelection()
        {
            Name = "location",
            Label = "inventoryexpress:inventoryexpress.inventory.location.label",
            Help = "inventoryexpress:inventoryexpress.inventory.location.description",
            Icon = new PropertyIcon(TypeIcon.Map),
            MultiSelect = false
        };

        /// <summary>
        /// Returns the supplier.
        /// </summary>
        public ControlApiFormularItemInputSelection Supplier { get; } = new ControlApiFormularItemInputSelection()
        {
            Name = "supplier",
            Label = "inventoryexpress:inventoryexpress.inventory.supplier.label",
            Help = "inventoryexpress:inventoryexpress.inventory.supplier.description",
            Icon = new PropertyIcon(TypeIcon.Truck),
            MultiSelect = false
        };

        /// <summary>
        /// Returns the ledger account.
        /// </summary>
        public ControlApiFormularItemInputSelection LedgerAccount { get; } = new ControlApiFormularItemInputSelection()
        {
            Name = "ledgeraccount",
            Label = "inventoryexpress:inventoryexpress.inventory.ledgeraccount.label",
            Help = "inventoryexpress:inventoryexpress.inventory.ledgeraccount.description",
            Icon = new PropertyIcon(TypeIcon.At),
            MultiSelect = false
        };

        /// <summary>
        /// Returns the cost center.
        /// </summary>
        public ControlApiFormularItemInputSelection CostCenter { get; } = new ControlApiFormularItemInputSelection()
        {
            Name = "costcenter",
            Label = "inventoryexpress:inventoryexpress.inventory.costcenter.label",
            Help = "inventoryexpress:inventoryexpress.inventory.costcenter.description",
            Icon = new PropertyIcon(TypeIcon.ShoppingBag),
            MultiSelect = false
        };

        /// <summary>
        /// Returns the condition.
        /// </summary>
        public ControlApiFormularItemInputSelection Condition { get; } = new ControlApiFormularItemInputSelection()
        {
            Name = "condition",
            Label = "inventoryexpress:inventoryexpress.inventory.condition.label",
            Help = "inventoryexpress:inventoryexpress.inventory.condition.description",
            Icon = new PropertyIcon(TypeIcon.Star),
            MultiSelect = false
        };

        /// <summary>
        /// Returns the parent.
        /// </summary>
        public ControlApiFormularItemInputSelection Parent { get; } = new ControlApiFormularItemInputSelection()
        {
            Name = "parent",
            Label = "inventoryexpress:inventoryexpress.inventory.parent.label",
            Help = "inventoryexpress:inventoryexpress.inventory.parent.description",
            Icon = new PropertyIcon(TypeIcon.Link),
            MultiSelect = false
        };

        /// <summary>
        /// Returns the template.
        /// </summary>
        public ControlApiFormularItemInputSelection Template { get; } = new ControlApiFormularItemInputSelection()
        {
            Name = "template",
            Label = "inventoryexpress:inventoryexpress.inventory.template.label",
            Help = "inventoryexpress:inventoryexpress.inventory.template.description",
            Icon = new PropertyIcon(TypeIcon.Clone),
            MultiSelect = false
        };

        /// <summary>
        /// Returns the attributes.
        /// </summary>
        public ControlFormItemGroupVertical Attributes { get; } = new ControlFormItemGroupVertical();

        /// <summary>
        /// Returns the cost value.
        /// </summary>
        public ControlFormItemInputTextBox CostValue { get; } = new ControlFormItemInputTextBox()
        {
            Name = "costvalue",
            Label = "inventoryexpress:inventoryexpress.inventory.costvalue.label",
            Help = "inventoryexpress:inventoryexpress.inventory.costvalue.description",
            Icon = new PropertyIcon(TypeIcon.EuroSign)
        };

        /// <summary>
        /// Returns the purchase date.
        /// </summary>
        public ControlFormItemInputDatepicker PurchaseDate { get; } = new ControlFormItemInputDatepicker()
        {
            Name = "purchasedate",
            Label = "inventoryexpress:inventoryexpress.inventory.purchasedate.label",
            Help = "inventoryexpress:inventoryexpress.inventory.purchasedate.description",
            Icon = new PropertyIcon(TypeIcon.CalendarPlus)
        };

        /// <summary>
        /// Returns the derecognition date.
        /// </summary>
        public ControlFormItemInputDatepicker DerecognitionDate { get; } = new ControlFormItemInputDatepicker()
        {
            Name = "derecognitiondate",
            Label = "inventoryexpress:inventoryexpress.inventory.derecognitiondate.label",
            Help = "inventoryexpress:inventoryexpress.inventory.derecognitiondate.description",
            Icon = new PropertyIcon(TypeIcon.CalendarMinus)
        };

        /// <summary>
        /// Returns the tags.
        /// </summary>
        public ControlApiFormularItemInputSelection Tag { get; } = new ControlApiFormularItemInputSelection()
        {
            Name = "tag",
            Label = "inventoryexpress:inventoryexpress.inventory.tags.label",
            Help = "inventoryexpress:inventoryexpress.inventory.tags.description",
            Icon = new PropertyIcon(TypeIcon.Tag),
            MultiSelect = true
        };

        /// <summary>
        /// Returns the description.
        /// </summary>
        public ControlFormItemInputTextBox Description { get; } = new ControlFormItemInputTextBox()
        {
            Name = "description",
            Label = "inventoryexpress:inventoryexpress.inventory.description.label",
            Help = "inventoryexpress:inventoryexpress.inventory.description.description",
            Format = TypesEditTextFormat.Wysiwyg,
            Icon = new PropertyIcon(TypeIcon.CommentAlt),
            Rows = 10,
            AutoInitialize = true
        };

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlFormularInventory(string id = null)
            : base(id ?? "inventory")
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Three, PropertySpacing.Space.None, PropertySpacing.Space.None);
            BackgroundColor = LayoutSchema.FormularBackground;
            Padding = new PropertySpacingPadding(PropertySpacing.Space.Two);
            Layout = TypeLayoutForm.Horizontal;

            Add(InventoryName);
            Add(Manufacturer);
            Add(Location);
            Add(Supplier);
            Add(LedgerAccount);
            Add(CostCenter);
            Add(Condition);
            Add(Parent);
            Add(Template);
            Add(Attributes);
            Add(CostValue);
            Add(PurchaseDate);
            Add(DerecognitionDate);
            Add(Tag);
            Add(Description);

            InventoryName.Validation += OnInventoryNameValidation;
            CostValue.Validation += OnCostValueValidation;
        }

        /// <summary>
        /// Initializes the form.
        /// </summary>
        /// <param name="context">The context in which the control is represented.</param>
        public override void Initialize(RenderContextFormular context)
        {
            base.Initialize(context);

            Attributes.Items.Clear();

            Manufacturer.RestUri = context.Uri.ModuleRoot.Append("api/v1/manufacturers");
            Location.RestUri = context.Uri.ModuleRoot.Append("api/v1/locations");
            Supplier.RestUri = context.Uri.ModuleRoot.Append("api/v1/suppliers");
            LedgerAccount.RestUri = context.Uri.ModuleRoot.Append("api/v1/ledgeraccounts");
            CostCenter.RestUri = context.Uri.ModuleRoot.Append("api/v1/costcenters");
            Condition.RestUri = context.Uri.ModuleRoot.Append("api/v1/conditions");
            Parent.RestUri = context.Uri.ModuleRoot.Append("api/v1/inventories");
            Template.RestUri = context.Uri.ModuleRoot.Append("api/v1/templates");
            Tag.RestUri = context.Uri.ModuleRoot.Append("api/v1/tags");
            Template.OnChange = new PropertyOnChange($"$('#{Id}').submit();");

            var guid = context.Request.GetParameter(Template.Name)?.Value;
            var template = ViewModel.GetTemplate(guid);

            // finding attributes
            if (template != null)
            {
                foreach (var attribute in template.Attributes)
                {
                    Attributes.Items.Add(new ControlFormItemInputTextBox()
                    {
                        Name = "attribute_" + attribute.Id,
                        Label = $"{attribute.Name}:",
                        Help = attribute.Description,
                        Tag = new WebItemEntityInventoryAttribute(attribute)
                    });
                }
            }
        }

        /// <summary>
        /// Invoked when the name is to be verified.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void OnInventoryNameValidation(object sender, ValidationEventArgs e)
        {
            var guid = e.Context.Request.GetParameter<ParameterInventoryId>()?.Value;
            var inventory = ViewModel.GetInventory(guid);

            if (e.Value == null || e.Value.Length < 1)
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.inventory.validation.name.invalid"));
            }
            else if
            (
                inventory == null &&
                ViewModel.GetInventories().Where(x => x.Name.Equals(e.Value, StringComparison.OrdinalIgnoreCase)).Any()
            )
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.inventory.validation.name.used"));
            }
            else if
            (
                inventory != null &&
                !inventory.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase) &&
                ViewModel.GetInventories().Where(x => x.Name.Equals(e.Value, StringComparison.OrdinalIgnoreCase)).Any()
            )
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.inventory.validation.name.used"));
            }
        }

        /// <summary>
        /// Invoked when the purchase value is to be checked.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void OnCostValueValidation(object sender, ValidationEventArgs e)
        {
            try
            {
                if (Convert.ToDecimal(CostValue.Value, e.Context.Culture) < 0)
                {
                    e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.inventory.validation.costvalue.negativ"));
                };
            }
            catch
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.inventory.validation.costvalue.invalid"));
            }
        }

        /// <summary>
        /// Returns the attributes.
        /// </summary>
        /// <returns>The attributes.</returns>
        public IEnumerable<WebItemEntityInventoryAttribute> GetAttributes()
        {
            var attributes = Attributes.Items.Select(x => x as ControlFormItemInputTextBox)
                .Select(x => new WebItemEntityInventoryAttribute(x.Tag as WebItemEntityInventoryAttribute)
                {
                    Value = x.Value
                });

            return attributes;
        }

        /// <summary>
        /// Inserts a value into an attribute field.
        /// </summary>
        /// <param name="attributes">The attribute.</param>
        public void SetAttributes(IEnumerable<WebItemEntityInventoryAttribute> attributes)
        {
            foreach (var attribute in attributes)
            {
                var field = Attributes.Items.Where(x => x.Name == "attribute_" + attribute.Id)
                .Select(x => x as ControlFormItemInput)
                .FirstOrDefault();

                if (field == null)
                {
                    field = new ControlFormItemInputTextBox()
                    {
                        Name = "attribute_" + attribute.Id,
                        Label = $"{attribute.Name}:",
                        Help = attribute.Description,
                        Tag = attribute
                    };

                    Attributes.Items.Add(field);
                }

                field.Value = attribute.Value;
            }
        }

        /// <summary>
        /// Transfer of values from the form.
        /// </summary>
        /// <param name="inventory">The inventory item, which takes the values.</param>
        /// <param name="culture">The culture.</param>
        public void Apply(WebItemEntityInventory inventory, CultureInfo culture)
        {
            inventory.Name = InventoryName.Value;
            inventory.Manufacturer = ViewModel.GetManufacturer(Manufacturer.Value);
            inventory.Location = ViewModel.GetLocation(Location.Value);
            inventory.Supplier = ViewModel.GetSupplier(Supplier.Value);
            inventory.LedgerAccount = ViewModel.GetLedgerAccount(LedgerAccount.Value);
            inventory.CostCenter = ViewModel.GetCostCenter(CostCenter.Value);
            inventory.Condition = ViewModel.GetCondition(Condition.Value);
            inventory.Parent = ViewModel.GetInventory(Parent.Value);
            inventory.Template = ViewModel.GetTemplate(Template.Value);
            inventory.CostValue = !string.IsNullOrWhiteSpace(CostValue.Value) ? Convert.ToDecimal(CostValue.Value, culture) : 0;
            inventory.PurchaseDate = !string.IsNullOrWhiteSpace(PurchaseDate.Value) ? Convert.ToDateTime(PurchaseDate.Value, culture) : null;
            inventory.DerecognitionDate = !string.IsNullOrWhiteSpace(DerecognitionDate.Value) ? Convert.ToDateTime(DerecognitionDate.Value, culture) : null;
            inventory.Tag = Tag.Value;
            inventory.Description = Description.Value;
            inventory.Attributes = GetAttributes();
        }

        /// <summary>
        /// Transfer of values from the inventory item.
        /// </summary>
        /// <param name="inventory">The inventory item from which the values originate.</param>
        /// <param name="culture">The culture.</param>
        public void Fill(WebItemEntityInventory inventory, CultureInfo culture)
        {
            InventoryName.Value = inventory?.Name;
            Manufacturer.Value = inventory.Manufacturer?.Guid;
            Manufacturer.Options.Add(new ControlFormItemInputSelectionItem() { Id = inventory.Manufacturer?.Guid, Label = inventory.Manufacturer?.Name });
            Location.Value = inventory.Location?.Guid;
            Location.Options.Add(new ControlFormItemInputSelectionItem() { Id = inventory.Location?.Guid, Label = inventory.Location?.Name });
            Supplier.Value = inventory.Supplier?.Guid;
            Supplier.Options.Add(new ControlFormItemInputSelectionItem() { Id = inventory.Supplier?.Guid, Label = inventory.Supplier?.Name });
            LedgerAccount.Value = inventory.LedgerAccount?.Guid;
            LedgerAccount.Options.Add(new ControlFormItemInputSelectionItem() { Id = inventory.LedgerAccount?.Guid, Label = inventory.LedgerAccount?.Name });
            CostCenter.Value = inventory.CostCenter?.Guid;
            CostCenter.Options.Add(new ControlFormItemInputSelectionItem() { Id = inventory.CostCenter?.Guid, Label = inventory.CostCenter?.Name });
            Condition.Value = inventory.Condition?.Guid;
            Condition.Options.Add(new ControlFormItemInputSelectionItem() { Id = inventory.Condition?.Guid, Label = inventory.Condition?.Name });
            Parent.Value = inventory.Parent?.Guid;
            Parent.Options.Add(new ControlFormItemInputSelectionItem() { Id = inventory.Parent?.Guid, Label = inventory.Parent?.Name });
            Template.Value = inventory.Template?.Guid;
            Template.Options.Add(new ControlFormItemInputSelectionItem() { Id = inventory.Template?.Guid, Label = inventory.Template?.Name });
            CostValue.Value = inventory.CostValue.ToString(culture);
            PurchaseDate.Value = inventory.PurchaseDate.HasValue ? inventory.PurchaseDate.Value.ToString(culture.DateTimeFormat.ShortDatePattern) : null;
            DerecognitionDate.Value = inventory.DerecognitionDate.HasValue ? inventory.DerecognitionDate.Value.ToString(culture.DateTimeFormat.ShortDatePattern) : null;
            Tag.Value = inventory.Tag;
            Description.Value = inventory?.Description;

            Attributes.Items.Clear();
            SetAttributes(inventory.Attributes);
        }
    }
}
