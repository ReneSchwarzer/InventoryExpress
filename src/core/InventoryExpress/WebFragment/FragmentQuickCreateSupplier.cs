﻿using InventoryExpress.WebPage;
using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebControl;
using WebExpress.UI.WebFragment;
using WebExpress.WebApp.WebFragment;
using WebExpress.WebAttribute;
using WebExpress.WebPage;
using WebExpress.WebUri;

namespace InventoryExpress.WebFragment
{
    [Section(Section.AppQuickcreateSecondary)]
    [WebExModule("inventoryexpress")]
    public sealed class FragmentQuickCreateSupplier : FragmentControlSplitButtonItemLink
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public FragmentQuickCreateSupplier()
            : base()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="page">Die Seite, indem die Komonente aktiv ist</param>
        public override void Initialization(IFragmentContext context, IPage page)
        {
            base.Initialization(context, page);

            Text = "inventoryexpress:inventoryexpress.supplier.label";
            Uri = UriResource.Combine(page.ResourceContext.ContextPath, "suppliers/add");
            Icon = new PropertyIcon(TypeIcon.Truck);
            Modal = new PropertyModal(TypeModal.Formular, TypeModalSize.Large);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Active = context.Page is IPageSupplier ? TypeActive.Active : TypeActive.None;

            return base.Render(context);
        }

    }
}
