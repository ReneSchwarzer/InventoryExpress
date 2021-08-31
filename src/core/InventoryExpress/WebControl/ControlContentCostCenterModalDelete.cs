using InventoryExpress.Model;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.Attribute;
using WebExpress.UI.Component;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.Components;
using WebExpress.WebApp.WebControl;

namespace InventoryExpress.WebControl
{
    /// <summary>
    /// Modal zum Löschen einer Kostenstelle. Wird von der Komponetne ControlMoreCostCenterDelete aufgerufen
    /// </summary>
    [Section(Section.ContentSecondary)]
    [Application("InventoryExpress")]
    [Context("costcenteredit")]
    public sealed class ControlContentCostCenterModalDelete : ControlModalFormConfirmDelete, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlContentCostCenterModalDelete()
           : base("del_costcenter")
        {
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Confirm += (s, e) =>
            {
                lock (ViewModel.Instance.Database)
                {
                    var guid = context.Page.GetParamValue("CostCenterID");
                    var costCenter = ViewModel.Instance.CostCenters.Where(x => x.Guid == guid).FirstOrDefault();

                    if (costCenter != null)
                    {
                        ViewModel.Instance.CostCenters.Remove(costCenter);
                        ViewModel.Instance.SaveChanges();
                    }
                }
            };

            Header = context.Page.I18N("inventoryexpress.costcenter.delete.label");
            Content = new ControlFormularItemStaticText() { Text = context.I18N("inventoryexpress.costcenter.delete.description") };
            RedirectUri = context.Uri.Take(-1);

            return base.Render(context);
        }
    }
}
