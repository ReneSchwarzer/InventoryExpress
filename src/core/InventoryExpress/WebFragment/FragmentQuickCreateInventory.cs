﻿using InventoryExpress.WebPage;
using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebControl;
using WebExpress.UI.WebFragment;
using WebExpress.WebApp.WebFragment;
using WebExpress.WebAttribute;
using WebExpress.WebPage;

namespace InventoryExpress.WebFragment
{
    [Section(Section.AppQuickcreatePreferences)]
    [WebExModule("inventoryexpress")]
    public sealed class FragmentQuickCreateInventory : FragmentControlSplitButtonItemLink
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public FragmentQuickCreateInventory()
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

            Text = "inventoryexpress:inventoryexpress.inventory.label";
            Uri = context.ModuleContext.ContextPath.Append("add");
            Icon = new PropertyIcon(TypeIcon.Plus);
            Modal = new PropertyModal(TypeModal.Formular, TypeModalSize.ExtraLarge);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Active = context.Page is IPageInventory ? TypeActive.Active : TypeActive.None;

            return base.Render(context);
        }

    }
}
