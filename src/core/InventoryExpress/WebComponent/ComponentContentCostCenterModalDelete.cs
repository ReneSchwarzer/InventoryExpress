﻿using InventoryExpress.Model;
using System.Linq;
using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebComponent;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebComponent;
using WebExpress.WebAttribute;
using WebExpress.WebPage;
using static WebExpress.Internationalization.InternationalizationManager;

namespace InventoryExpress.WebComponent
{
    /// <summary>
    /// Modal zum Löschen einer Kostenstelle. Wird von der Komponetne ControlMoreCostCenterDelete aufgerufen
    /// </summary>
    [Section(Section.ContentSecondary)]
    [Module("inventoryexpress")]
    [Context("costcenteredit")]
    public sealed class ComponentContentCostCenterModalDelete : ComponentControlModalFormConfirmDelete
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ComponentContentCostCenterModalDelete()
           : base("del_costcenter")
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IComponentContext context)
        {
            base.Initialization(context);
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
                    var guid = context.Request.GetParameter("CostCenterID")?.Value;
                    var costCenter = ViewModel.Instance.CostCenters.Where(x => x.Guid == guid).FirstOrDefault();

                    if (costCenter != null)
                    {
                        ViewModel.Instance.CostCenters.Remove(costCenter);
                        ViewModel.Instance.SaveChanges();
                    }
                }
            };

            Header = I18N(context.Culture, "inventoryexpress:inventoryexpress.costcenter.delete.label");
            Content = new ControlFormularItemStaticText() { Text = I18N(context.Culture, "inventoryexpress:inventoryexpress.costcenter.delete.description") };
            RedirectUri = context.Uri.Take(-1);

            return base.Render(context);
        }
    }
}
