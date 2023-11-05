﻿using InventoryExpress.WebPage;
using WebExpress.WebHtml;
using WebExpress.WebUI.WebAttribute;
using WebExpress.WebUI.WebControl;
using WebExpress.WebUI.WebFragment;
using WebExpress.WebApp.WebFragment;
using WebExpress.WebAttribute;
using WebExpress.WebComponent;
using WebExpress.WebPage;

namespace InventoryExpress.WebFragment
{
    [Section(Section.AppNavigationPrimary)]
    [Module<Module>]
    [Cache]
    public sealed class FragmentAppNavigationSupplier : FragmentControlNavigationItemLink
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FragmentAppNavigationSupplier()
            : base()
        {
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="page">The page where the component is active.</param>
        public override void Initialization(IFragmentContext context, IPage page)
        {
            base.Initialization(context, page);

            Text = "inventoryexpress:inventoryexpress.suppliers.label";
            Uri = ComponentManager.SitemapManager.GetUri<PageSuppliers>();
            Icon = new PropertyIcon(TypeIcon.Truck);
            Active = page is IPageSupplier ? TypeActive.Active : TypeActive.None;
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is represented.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            return base.Render(context);
        }
    }
}
