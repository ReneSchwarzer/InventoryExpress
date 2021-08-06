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
    [Context("ledgeraccountedit")]
    public sealed class ControlSidebarLedgerAccountMedia : ControlLink, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlSidebarLedgerAccountMedia()
        {
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            lock (ViewModel.Instance.Database)
            {
                var guid = context.Page.GetParamValue("LedgerAccountID");
                var ledgerAccount = ViewModel.Instance.LedgerAccounts.Where(x => x.Guid == guid).FirstOrDefault();
                var media = ViewModel.Instance.Media.Where(x => x.Id == (ledgerAccount != null ? ledgerAccount.MediaId : null)).FirstOrDefault();
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
}
