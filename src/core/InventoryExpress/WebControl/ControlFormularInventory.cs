using InventoryExpress.Model;
using InventoryExpress.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebControlRest;
using WebExpress.WebApp.WebPage;

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
        public ControlFormularItemInputSelectionRest Manufacturer { get; } = new ControlFormularItemInputSelectionRest("manufacturer")
        {
            Name = "manufacturer",
            Label = "inventoryexpress:inventoryexpress.inventory.manufacturers.label",
            Help = "inventoryexpress:inventoryexpress.inventory.manufacturer.description",
            Icon = new PropertyIcon(TypeIcon.Industry),
            MultiSelect = false
        };

        /// <summary>
        /// Liefert oder setzt den Standort
        /// </summary>
        public ControlFormularItemInputSelectionRest Location { get; } = new ControlFormularItemInputSelectionRest("location")
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
        public ControlFormularItemInputSelectionRest Supplier { get; } = new ControlFormularItemInputSelectionRest("supplier")
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
        public ControlFormularItemInputSelectionRest LedgerAccount { get; } = new ControlFormularItemInputSelectionRest("ledgeraccount")
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
        public ControlFormularItemInputSelectionRest CostCenter { get; } = new ControlFormularItemInputSelectionRest("costcenter")
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
        public ControlFormularItemInputSelectionRest Condition { get; } = new ControlFormularItemInputSelectionRest("condition")
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
        public ControlFormularItemInputSelectionRest Parent { get; } = new ControlFormularItemInputSelectionRest("parent")
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
        public ControlFormularItemInputSelectionRest Template { get; } = new ControlFormularItemInputSelectionRest("template")
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
        public ControlFormularItemInputDatepicker PurchaseDate { get; } = new ControlFormularItemInputDatepicker("purchasedate")
        {
            Name = "purchasedate",
            Label = "inventoryexpress:inventoryexpress.inventory.purchasedate.label",
            Help = "inventoryexpress:inventoryexpress.inventory.purchasedate.description",
            Icon = new PropertyIcon(TypeIcon.CalendarPlus)
        };

        /// <summary>
        /// Liefert oder setzt das Abgangsdatum
        /// </summary>
        public ControlFormularItemInputDatepicker DerecognitionDate { get; } = new ControlFormularItemInputDatepicker("derecognitiondate")
        {
            Name = "derecognitiondate",
            Label = "inventoryexpress:inventoryexpress.inventory.derecognitiondate.label",
            Help = "inventoryexpress:inventoryexpress.inventory.derecognitiondate.description",
            Icon = new PropertyIcon(TypeIcon.CalendarMinus)
        };

        /// <summary>
        /// Liefert oder setzt die Schlagwörter
        /// </summary>
        public ControlFormularItemInputSelectionRest Tag { get; } = new ControlFormularItemInputSelectionRest("tags")
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
        public ControlFormularItemInputTextBox Description { get; } = new ControlFormularItemInputTextBox("note")
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
        /// Bestimmt, ob das Formular zum Bearbeiten oder zum Neuanlegen verwendet werden soll.
        /// </summary>
        public bool Edit { get; set; } = false;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormularInventory(string id = null)
            : base(id ?? "inventory")
        {
            EnableCancelButton = false;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Three, PropertySpacing.Space.None, PropertySpacing.Space.None);
            BackgroundColor = LayoutSchema.FormularBackground;
            Border = new PropertyBorder(true);
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
            
            Manufacturer.RestUri = context.Uri.Root.Append("api/v1/manufacturers");
            Location.RestUri = context.Uri.Root.Append("api/v1/locations");
            Supplier.RestUri = context.Uri.Root.Append("api/v1/suppliers");
            LedgerAccount.RestUri = context.Uri.Root.Append("api/v1/ledgeraccounts");
            CostCenter.RestUri = context.Uri.Root.Append("api/v1/costcenters");
            Condition.RestUri = context.Uri.Root.Append("api/v1/conditions");
            Parent.RestUri = context.Uri.Root.Append("api/v1/inventories");
            Template.RestUri = context.Uri.Root.Append("api/v1/templates");
            Tag.RestUri = context.Uri.Root.Append("api/v1/tags");
            Template.OnChange = new PropertyOnChange($"$('#{ ID }').submit();");

            var guid = context.Request.GetParameter("InventoryID")?.Value;

            if (Edit)
            {
                lock (ViewModel.Instance.Database)
                {
                    
                    var inventory = ViewModel.Instance.Inventories.Where(x => x.Guid == guid).FirstOrDefault();
                    var attributesForm = new List<ControlFormularItemInputTextBox>();
                    var attributes = new List<InventoryAttribute>();

                    var templateGUID = context.Request.HasParameter(Template?.Name) ?
                        context.Request.GetParameter(Template?.Name)?.Value :
                        ViewModel.Instance.Templates.Where(x => x.Id == inventory.TemplateId).FirstOrDefault()?.Guid;

                    SetAttributes(inventory, templateGUID);
                }
            }
            else
            {
                var templateGUID = context.Request.GetParameter(Template?.Name)?.Value;
                SetAttributes(templateGUID);
            }
        }

        /// <summary>
        /// Übernahme der Attribute
        /// </summary>
        /// <param name="templateGUID">Die GUID der Attributvorlage</param>
        protected void SetAttributes(string templateGUID)
        {
            if (string.IsNullOrWhiteSpace(templateGUID))
            {
                return;
            }

            var attributesForm = new List<ControlFormularItemInputTextBox>();

            lock (ViewModel.Instance.Database)
            {
                var template = ViewModel.Instance.Templates.Where(x => x.Guid == templateGUID).FirstOrDefault();
                var attributes = from x in ViewModel.Instance.TemplateAttributes.Where(x => x.TemplateId == template.Id) 
                                 join y in ViewModel.Instance.Attributes on x.AttributeId equals y.Id
                                 select y;

                foreach (var attribute in attributes)
                {
                    attributesForm.Add(new ControlFormularItemInputTextBox()
                    {
                        Name = "attribute_" + attribute.Guid,
                        Label = $"{ attribute.Name }:",
                        Help = attribute.Description,
                        Tag = attribute
                    });
                }
            }

            attributesForm.ForEach(x => Attributes?.Items.Add(x));
        }

        /// <summary>
        /// Übernahme der Attribute
        /// </summary>
        /// <param name="inventory">Der Inventargegenstand</param>
        /// <param name="templateGUID">Die GUID der Attributvorlage</param>
        protected void SetAttributes(Inventory inventory, string templateGUID)
        {
            lock (ViewModel.Instance.Database)
            {
                var attributesForm = new List<ControlFormularItemInputTextBox>();
                var attributes = new List<InventoryAttribute>();
                               
                var template = ViewModel.Instance.Templates.Where(x => x.Guid == templateGUID).FirstOrDefault();

                // nur gefüllte Attribute übernehmen
                attributes.AddRange(ViewModel.Instance.InventoryAttributes.Where(x => x.InventoryId == inventory.Id && !string.IsNullOrWhiteSpace(x.Value)));

                // Template-Attribute übernehmen
                if (template != null)
                {
                    foreach (var ta in ViewModel.Instance.TemplateAttributes.Where(x => x.TemplateId == template.Id))
                    {
                        var att = ViewModel.Instance.Attributes.Where(x => x.Id == ta.AttributeId).FirstOrDefault();

                        if (attributes.Find
                            (
                                f =>
                                f.Attribute != null && f.Attribute.Name.Equals
                                (
                                    att?.Name,
                                    StringComparison.OrdinalIgnoreCase
                                )
                            ) == null)
                        {
                            attributes.Add(new InventoryAttribute()
                            {
                                AttributeId = ta.AttributeId,
                                InventoryId = inventory.Id,
                                Attribute = att,
                                Inventory = inventory,
                                Created = DateTime.Now
                            });
                        }
                    }
                }

                foreach (var attribute in attributes)
                {
                    attributesForm.Add(new ControlFormularItemInputTextBox()
                    {
                        Name = "attribute_" + attribute.Attribute.Guid,
                        Label = $"{ attribute.Attribute.Name }:",
                        Help = attribute.Attribute.Description,
                        Tag = attribute
                    });
                }

                attributesForm.ForEach(x => Attributes?.Items.Add(x));
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Name überfürft werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnInventoryNameValidation(object sender, ValidationEventArgs e)
        {
            if (e.Value.Length < 1)
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.inventory.validation.name.invalid"));
            }

            lock (ViewModel.Instance.Database)
            {
                var guid = e.Context.Request.GetParameter("InventoryID")?.Value;
                var inventory = ViewModel.Instance.Inventories.Where(x => x.Guid == guid).FirstOrDefault();

                if (inventory != null && !inventory.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase) && ViewModel.Instance.Inventories.Where(x => x.Name.Equals(e.Value)).Any())
                {
                    e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.inventory.validation.name.used"));
                }
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Anschaffungswert überfürft werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
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
    }
}
