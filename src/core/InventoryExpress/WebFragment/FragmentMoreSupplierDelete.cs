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
    [WebExSection(Section.MoreSecondary)]
    [WebExModule(typeof(Module))]
    [WebExContext("supplieredit")]
    public sealed class FragmentMoreSupplierDelete : FragmentControlDropdownItemLink
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public FragmentMoreSupplierDelete()
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
            var guid = context.Request.GetParameter("SupplierID")?.Value;
            var supplier = ViewModel.GetSupplier(guid);
            var inUse = ViewModel.GetSupplierInUse(supplier);


            Active = inUse ? TypeActive.Disabled : TypeActive.None;
            TextColor = inUse ? new PropertyColorText(TypeColorText.Muted) : TextColor;

            Uri = context.Uri.Append("del");
            Modal = new PropertyModal(TypeModal.Formular, TypeModalSize.Default) { RedirectUri = context.ApplicationContext.ContextPath.Append("suppliers") };

            return base.Render(context);
        }
    }
}