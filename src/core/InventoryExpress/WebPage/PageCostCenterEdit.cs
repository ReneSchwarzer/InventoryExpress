using InventoryExpress.Model;
using InventoryExpress.WebControl;
using System;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [ID("CostCenterEdit")]
    [Title("inventoryexpress:inventoryexpress.costcenter.edit.label")]
    [SegmentGuid("CostCenterID", "inventoryexpress:inventoryexpress.costcenter.edit.display")]
    [Path("/CostCenter")]
    [Module("inventoryexpress")]
    [Context("general")]
    [Context("costcenteredit")]
    public sealed class PageCostCenterEdit : PageWebApp, IPageCostCenter
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
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            form = new ControlFormularCostCenter()
            {
                Edit = true
            };
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var visualTree = context.VisualTree;

            var guid = context.Request.GetParameter("CostCenterID")?.Value;
            var costcenter = ViewModel.Instance.CostCenters.Where(x => x.Guid == guid).FirstOrDefault();

            visualTree.Content.Primary.Add(form);

            form.RedirectUri = context.Uri.Take(-1);
            form.BackUri = context.Uri.Take(-1);
            form.CostCenterName.Value = costcenter?.Name;
            form.Description.Value = costcenter?.Description;
            form.Tag.Value = costcenter?.Tag;

            form.CostCenterName.Validation += (s, e) =>
            {
                if (e.Value.Count() < 1)
                {
                    e.Results.Add(new ValidationResult() { Text = this.I18N("inventoryexpress.costcenter.validation.name.invalid"), Type = TypesInputValidity.Error });
                }
                else if (!costcenter.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase) && ViewModel.Instance.Suppliers.Where(x => x.Name.Equals(e.Value)).Count() > 0)
                {
                    e.Results.Add(new ValidationResult() { Text = this.I18N("inventoryexpress.costcenter.validation.name.used"), Type = TypesInputValidity.Error });
                }
            };

            form.ProcessFormular += (s, e) =>
            {
                // Kostenstelle ändern und speichern
                costcenter.Description = form.Description.Value;
                costcenter.Updated = DateTime.Now;
                costcenter.Tag = form.Tag.Value;

                ViewModel.Instance.SaveChanges();
            };
        }
    }
}
