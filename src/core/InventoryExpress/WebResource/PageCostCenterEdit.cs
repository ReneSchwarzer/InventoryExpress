﻿using InventoryExpress.Model;
using InventoryExpress.WebControl;
using System;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebResource;

namespace InventoryExpress.WebResource
{
    [ID("CostCenterEdit")]
    [Title("inventoryexpress.costcenter.edit.label")]
    [SegmentGuid("CostCenterID", "inventoryexpress.costcenter.edit.display")]
    [Path("/CostCenter")]
    [Module("InventoryExpress")]
    [Context("general")]
    [Context("costcenteredit")]
    public sealed class PageCostCenterEdit : PageTemplateWebApp, IPageCostCenter
    {
        /// <summary>
        /// Formular
        /// </summary>
        private ControlFormularCostCenter form;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageCostCenterEdit()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Initialization()
        {
            base.Initialization();

            form = new ControlFormularCostCenter()
            {
                RedirectUrl = Uri.Take(-1),
                Edit = true
            };
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();

            var guid = GetParamValue("CostCenterID");
            var costcenter = ViewModel.Instance.CostCenters.Where(x => x.Guid == guid).FirstOrDefault();

            Content.Primary.Add(form);

            form.CostCenterName.Value = costcenter?.Name;
            form.Description.Value = costcenter?.Description;
            form.Tag.Value = "";

            form.CostCenterName.Validation += (s, e) =>
            {
                if (e.Value.Count() < 1)
                {
                    e.Results.Add(new ValidationResult() { Text = "Geben Sie einen gültigen Namen ein!", Type = TypesInputValidity.Error });
                }
                else if (!costcenter.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase) && ViewModel.Instance.Suppliers.Where(x => x.Name.Equals(e.Value)).Count() > 0)
                {
                    e.Results.Add(new ValidationResult() { Text = "Die Kostenstelle wird bereits verwendet. Geben Sie einen anderen Namen an!", Type = TypesInputValidity.Error });
                }
            };

            form.ProcessFormular += (s, e) =>
            {
                // Kostenstelle ändern und speichern
                costcenter.Description = form.Description.Value;
                costcenter.Updated = DateTime.Now;
                //costcenter.Tag = form.Tag.Value;

                ViewModel.Instance.SaveChanges();
            };
        }
    }
}
