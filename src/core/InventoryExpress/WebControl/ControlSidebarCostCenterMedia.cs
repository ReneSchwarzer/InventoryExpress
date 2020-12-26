﻿using InventoryExpress.Model;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Html;
using WebExpress.UI.Attribute;
using WebExpress.UI.Component;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.Components;

namespace InventoryExpress.WebControl
{
    [Section(Section.SidebarPreferences)]
    [Application("InventoryExpress")]
    [Context("costcenteredit")]
    public sealed class ControlSidebarCostCenterMedia : ControlLink, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlSidebarCostCenterMedia()
        {
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var guid = context.Page.GetParamValue("CostCenterID");
            var costCenter = ViewModel.Instance.CostCenters.Where(x => x.Guid == guid).FirstOrDefault();
            var media = ViewModel.Instance.Media.Where(x => x.ID == (costCenter != null ? costCenter.MediaID : null)).FirstOrDefault();
            var image = media != null ? context.Uri.Root.Append("media").Append(media.Guid) : null;

            Uri = context.Uri.Append("media");
            
            Content.Add(new ControlImage()
            {
                Uri = image == null ? context.Uri.Root.Append("/assets/img/inventoryexpress.svg") : image,
                Width = 180,
                Margin = new PropertySpacingMargin(PropertySpacing.Space.Two)
            });

            return base.Render(context);
        }
    }
}
