﻿using InventoryExpress.Model;
using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebControl;
using WebExpress.UI.WebFragment;
using WebExpress.WebUri;
using WebExpress.WebApp.WebFragment;
using WebExpress.WebAttribute;
using WebExpress.WebPage;

namespace InventoryExpress.WebFragment
{
    [Section(Section.MoreSecondary)]
    [WebExModule("inventoryexpress")]
    [WebExContext("media")]
    public sealed class FragmentMoreMediaDelete : FragmentControlDropdownItemLink
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public FragmentMoreMediaDelete()
        {
            TextColor = new PropertyColorText(TypeColorText.Danger);
            Uri = new UriFragment();
            Text = "inventoryexpress:inventoryexpress.delete.label";
            Icon = new PropertyIcon(TypeIcon.Trash);
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="page">Die Seite, indem die Komonente aktiv ist</param>
        public override void Initialization(IFragmentContext context, IPage page)
        {
            base.Initialization(context, page);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var guid = context.Request.GetParameter("MediaID")?.Value;
            var media = ViewModel.GetMedia(guid);
            var inUse = ViewModel.GetMediaInUse(media);

            Active = inUse ? TypeActive.Disabled : TypeActive.None;
            TextColor = inUse ? new PropertyColorText(TypeColorText.Muted) : TextColor;

            Uri = context.Uri.Append("del");
            Modal = new PropertyModal(TypeModal.Formular, TypeModalSize.Default) { RedirectUri = context.Uri.Take(-1) };

            return base.Render(context);
        }
    }
}
