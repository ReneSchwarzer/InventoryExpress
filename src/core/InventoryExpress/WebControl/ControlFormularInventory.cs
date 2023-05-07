using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebApiControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.Wql;

namespace InventoryExpress.WebControl
{
    public class ControlFormularInventory : ControlFormular
    {
        /// <summary>
        /// Liefert oder setzt den Namen des Inventargegenstandes
        /// </summary>
        public ControlFormularItemInputTextBox InventoryName { get; } = new ControlFormularItemInputTextBox()
        {
            Name = "name",
            Label = "inventoryexpress:inventoryexpress.inventory.name.label",
            Help = "inventoryexpress:inventoryexpress.inventory.name.description",
            Icon = new PropertyIcon(TypeIcon.Font),
            Format = TypesEditTextFormat.Default
        };

        /// <summary>
        /// Liefert oder setzt den Hersteller
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
        /// Liefert oder setzt den Standort
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
        /// Liefert oder setzt den Lieferanten
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
        /// Liefert oder setzt das Sachkonto
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
        /// Liefert oder setzt die Kostenstelle
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
        /// Liefert oder setzt den Zustand
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
        /// Liefert oder setzt die Zugehörigkeit
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
        /// Liefert oder setzt das Template
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
        /// Liefert oder setzt die Attribute
        /// </summary>
        public ControlFormularItemGroupVertical Attributes { get; } = new ControlFormularItemGroupVertical();

        /// <summary>
        /// Liefert oder setzt den Anschaffungswert
        /// </summary>
        public ControlFormularItemInputTextBox CostValue { get; } = new ControlFormularItemInputTextBox()
        {
            Name = "costvalue",
            Label = "inventoryexpress:inventoryexpress.inventory.costvalue.label",
            Help = "inventoryexpress:inventoryexpress.inventory.costvalue.description",
            Icon = new PropertyIcon(TypeIcon.EuroSign)
        };

        /// <summary>
        /// Liefert oder setzt das Anschaffungsdatum
        /// </summary>
        public ControlFormularItemInputDatepicker PurchaseDate { get; } = new ControlFormularItemInputDatepicker()
        {
            Name = "purchasedate",
            Label = "inventoryexpress:inventoryexpress.inventory.purchasedate.label",
            Help = "inventoryexpress:inventoryexpress.inventory.purchasedate.description",
            Icon = new PropertyIcon(TypeIcon.CalendarPlus)
        };

        /// <summary>
        /// Liefert oder setzt das Abgangsdatum
        /// </summary>
        public ControlFormularItemInputDatepicker DerecognitionDate { get; } = new ControlFormularItemInputDatepicker()
        {
            Name = "derecognitiondate",
            Label = "inventoryexpress:inventoryexpress.inventory.derecognitiondate.label",
            Help = "inventoryexpress:inventoryexpress.inventory.derecognitiondate.description",
            Icon = new PropertyIcon(TypeIcon.CalendarMinus)
        };

        /// <summary>
        /// Liefert oder setzt die Schlagwörter
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
        /// Liefert oder setzt die Beschreibung
        /// </summary>
        public ControlFormularItemInputTextBox Description { get; } = new ControlFormularItemInputTextBox()
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
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormularInventory(string id = null)
            : base(id ?? "inventory")
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Three, PropertySpacing.Space.None, PropertySpacing.Space.None);
            BackgroundColor = LayoutSchema.FormularBackground;
            Padding = new PropertySpacingPadding(PropertySpacing.Space.Two);
            Layout = TypeLayoutFormular.Horizontal;

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
        /// Initialisiert das Formular
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
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

            // Attribute ermitteln
            if (template != null)
            {
                foreach (var attribute in template.Attributes)
                {
                    Attributes.Items.Add(new ControlFormularItemInputTextBox()
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
        /// Wird aufgerufen, wenn der Name überfürft werden soll
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">Das Eventargument</param>
        private void OnInventoryNameValidation(object sender, ValidationEventArgs e)
        {
            var guid = e.Context.Request.GetParameter("InventoryID")?.Value;
            var inventory = ViewModel.GetInventory(guid);

            if (e.Value == null || e.Value.Length < 1)
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.inventory.validation.name.invalid"));
            }
            else if
            (
                inventory == null &&
                ViewModel.GetInventories(new WqlStatement()).Where(x => x.Name.Equals(e.Value, StringComparison.OrdinalIgnoreCase)).Any()
            )
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.inventory.validation.name.used"));
            }
            else if
            (
                inventory != null &&
                !inventory.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase) &&
                ViewModel.GetInventories(new WqlStatement()).Where(x => x.Name.Equals(e.Value, StringComparison.OrdinalIgnoreCase)).Any()
            )
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.inventory.validation.name.used"));
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Anschaffungswert überfürft werden soll
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">Das Eventargument</param>
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
        /// Liefert die Attribute
        /// </summary>
        /// <returns>Die Attribute</returns>
        public IEnumerable<WebItemEntityInventoryAttribute> GetAttributes()
        {
            var attributes = Attributes.Items.Select(x => x as ControlFormularItemInputTextBox)
                .Select(x => new WebItemEntityInventoryAttribute(x.Tag as WebItemEntityInventoryAttribute)
                {
                    Value = x.Value
                });

            return attributes;
        }

        /// <summary>
        /// Setzt einen Wert in ein Attributfeld ein
        /// </summary>
        /// <param name="attributes">Das Attribute</param>
        public void SetAttributes(IEnumerable<WebItemEntityInventoryAttribute> attributes)
        {
            foreach (var attribute in attributes)
            {
                var field = Attributes.Items.Where(x => x.Name == "attribute_" + attribute.Id)
                .Select(x => x as ControlFormularItemInput)
                .FirstOrDefault();

                if (field == null)
                {
                    field = new ControlFormularItemInputTextBox()
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
        /// Übernahme der Werte aus dem Formular
        /// </summary>
        /// <param name="inventory">Der Inventargegenstand, welcher die Werte aufnimmt</param>
        /// <param name="culture">Die Kultur</param>
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
        /// Übernahme der Werte aus dem Inventargegenstand
        /// </summary>
        /// <param name="inventory">Der Inventargegenstand, aus denen die Werte stammen</param>
        /// <param name="culture">Die Kultur</param>
        public void Fill(WebItemEntityInventory inventory, CultureInfo culture)
        {
            InventoryName.Value = inventory?.Name;
            Manufacturer.Value = inventory.Manufacturer?.Id;
            Manufacturer.Options.Add(new ControlFormularItemInputSelectionItem() { ID = inventory.Manufacturer?.Id, Label = inventory.Manufacturer?.Name });
            Location.Value = inventory.Location?.Id;
            Location.Options.Add(new ControlFormularItemInputSelectionItem() { ID = inventory.Location?.Id, Label = inventory.Location?.Name });
            Supplier.Value = inventory.Supplier?.Id;
            Supplier.Options.Add(new ControlFormularItemInputSelectionItem() { ID = inventory.Supplier?.Id, Label = inventory.Supplier?.Name });
            LedgerAccount.Value = inventory.LedgerAccount?.Id;
            LedgerAccount.Options.Add(new ControlFormularItemInputSelectionItem() { ID = inventory.LedgerAccount?.Id, Label = inventory.LedgerAccount?.Name });
            CostCenter.Value = inventory.CostCenter?.Id;
            CostCenter.Options.Add(new ControlFormularItemInputSelectionItem() { ID = inventory.CostCenter?.Id, Label = inventory.CostCenter?.Name });
            Condition.Value = inventory.Condition?.Id;
            Condition.Options.Add(new ControlFormularItemInputSelectionItem() { ID = inventory.Condition?.Id, Label = inventory.Condition?.Name });
            Parent.Value = inventory.Parent?.Id;
            Parent.Options.Add(new ControlFormularItemInputSelectionItem() { ID = inventory.Parent?.Id, Label = inventory.Parent?.Name });
            Template.Value = inventory.Template?.Id;
            Template.Options.Add(new ControlFormularItemInputSelectionItem() { ID = inventory.Template?.Id, Label = inventory.Template?.Name });
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
