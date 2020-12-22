using InventoryExpress.WebControl;
using InventoryExpress.Model;
using System;
using System.Linq;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebResource;
using WebExpress.Attribute;

namespace InventoryExpress.WebResource
{
    [ID("CostCenterAdd")]
    [Title("inventoryexpress.costcenters.add.label")]
    [Segment("add", "inventoryexpress.costcenters.add.label")]
    [Path("/CostCenter")]
    [Module("InventoryExpress")]
    [Context("general")]
    public sealed class PageCostCenterAdd : PageTemplateWebApp, IPageCostCenter
    {
        /// <summary>
        /// Formular
        /// </summary>
        private ControlFormularCostCenter form;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageCostCenterAdd()
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

            Content.Content.Add(form);

            form.CostCenterName.Validation += (s, e) =>
            {
                if (e.Value.Count() < 1)
                {
                    e.Results.Add(new ValidationResult() { Text = "Geben Sie einen gültigen Namen ein!", Type = TypesInputValidity.Error });
                }
                else if (ViewModel.Instance.CostCenters.Where(x => x.Name.Equals(e.Value)).Count() > 0)
                {
                    e.Results.Add(new ValidationResult() { Text = "Die Kostenstelle wird bereits verwendet. Geben Sie einen anderen Namen an!", Type = TypesInputValidity.Error });
                }
            };

            form.ProcessFormular += (s, e) =>
            {
                // Neues Herstellerobjekt erstellen und speichern
                var costcenter = new CostCenter()
                {
                    Name = form.CostCenterName.Value,
                    //Tag = form.Tag.Value,
                    Description = form.Description.Value,
                    Guid = Guid.NewGuid().ToString()
                };

                ViewModel.Instance.CostCenters.Add(costcenter);
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
