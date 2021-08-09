﻿using InventoryExpress.Model;
using InventoryExpress.WebControl;
using System;
using System.Collections.Generic;
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

            var guid = Guid.NewGuid().ToString();

            form.RedirectUri = Uri.Root.Append(guid).Append("edit");

            Content.Primary.Add(form);

            form.InitializeFormular += (s, e) =>
            {
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
            };

            form.ProcessFormular += (s, e) =>
            {
                lock (ViewModel.Instance.Database)
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
                        Parent = ViewModel.Instance.Inventories.Where(x => x.Guid == form.Parent.Value).FirstOrDefault(),
                        Template = ViewModel.Instance.Templates.Where(x => x.Guid == form.Template.Value).FirstOrDefault(),
                        CostValue = !string.IsNullOrWhiteSpace(form.CostValue.Value) ? Convert.ToDecimal(form.CostValue.Value, Culture) : 0,
                        PurchaseDate = !string.IsNullOrWhiteSpace(form.PurchaseDate.Value) ? Convert.ToDateTime(form.PurchaseDate.Value, Culture) : null,
                        DerecognitionDate = !string.IsNullOrWhiteSpace(form.DerecognitionDate.Value) ? Convert.ToDateTime(form.DerecognitionDate.Value, Culture) : null,
                        Tag = form.Tag.Value,
                        Description = form.Description.Value,
                        Guid = guid
                    };

                    ViewModel.Instance.Inventories.Add(inventory);
                    ViewModel.Instance.SaveChanges();
                }
            };
        }
    }
}
