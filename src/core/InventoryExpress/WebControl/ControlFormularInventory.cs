﻿using InventoryExpress.Model;
using InventoryExpress.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebApiControl;
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
        public ControlApiFormularItemInputSelection Manufacturer { get; } = new ControlApiFormularItemInputSelection("manufacturer")
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
        public ControlApiFormularItemInputSelection Location { get; } = new ControlApiFormularItemInputSelection("location")
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
        public ControlApiFormularItemInputSelection Supplier { get; } = new ControlApiFormularItemInputSelection("supplier")
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
        public ControlApiFormularItemInputSelection LedgerAccount { get; } = new ControlApiFormularItemInputSelection("ledgeraccount")
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
        public ControlApiFormularItemInputSelection CostCenter { get; } = new ControlApiFormularItemInputSelection("costcenter")
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
        public ControlApiFormularItemInputSelection Condition { get; } = new ControlApiFormularItemInputSelection("condition")
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
        public ControlApiFormularItemInputSelection Parent { get; } = new ControlApiFormularItemInputSelection("parent")
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
        public ControlApiFormularItemInputSelection Template { get; } = new ControlApiFormularItemInputSelection("template")
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
        public ControlApiFormularItemInputSelection Tag { get; } = new ControlApiFormularItemInputSelection("tags")
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

            Manufacturer.RestUri = context.Uri.Root.Append("api/v1/manufacturers");
            Location.RestUri = context.Uri.Root.Append("api/v1/locations");
            Supplier.RestUri = context.Uri.Root.Append("api/v1/suppliers");
            LedgerAccount.RestUri = context.Uri.Root.Append("api/v1/ledgeraccounts");
            CostCenter.RestUri = context.Uri.Root.Append("api/v1/costcenters");
            Condition.RestUri = context.Uri.Root.Append("api/v1/conditions");
            Parent.RestUri = context.Uri.Root.Append("api/v1/inventories");
            Template.RestUri = context.Uri.Root.Append("api/v1/templates");
            Tag.RestUri = context.Uri.Root.Append("api/v1/tags");
            Template.OnChange = new PropertyOnChange($"$('#{ID}').submit();");
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

            //lock (ViewModel.Instance.Database)
            //{
            var guid = e.Context.Request.GetParameter("InventoryID")?.Value;
            var inventory = ViewModel.GetInventory(guid);
            //var inventory = ViewModel.GetInventoryByName(e.Value);

            //    if (inventory != null && !inventory.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase) && ViewModel.Instance.Inventories.Where(x => x.Name.Equals(e.Value)).Any())
            //    {
            //        e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.inventory.validation.name.used"));
            //    }
            //}
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
