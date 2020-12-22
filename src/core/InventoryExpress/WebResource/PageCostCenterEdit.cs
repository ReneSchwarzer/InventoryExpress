using InventoryExpress.WebControl;
using InventoryExpress.Model;
using System;
using System.Linq;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebResource;
using WebExpress.Attribute;

namespace InventoryExpress.WebResource
{
    [ID("CostCenterEdit")]
    [Title("inventoryexpress.costcenters.edit.label")]
    [SegmentGuid("CostCenterID", "inventoryexpress.costcenters.edit.display")]
    [Path("/CostCenter")]
    [Module("InventoryExpress")]
    [Context("general")]
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
                RedirectUrl = Uri.Take(-1)
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

            Content.Content.Add(form);

            form.CostCenterName.Value = costcenter?.Name;
            form.Description.Value = costcenter?.Description;

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
                // Herstellerobjekt ändern und speichern
                costcenter.Name = form.CostCenterName.Value;
                //Tag = form.Tag.Value;
                costcenter.Description = form.Description.Value;

                ViewModel.Instance.SaveChanges();
            };
        }

        /// <summary>
        /// In String konvertieren
        /// </summary>
        /// <returns>Das Objekt als String</returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
