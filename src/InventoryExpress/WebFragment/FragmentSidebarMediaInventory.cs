﻿using InventoryExpress.Model;
using InventoryExpress.Parameter;
using InventoryExpress.WebPage;
using WebExpress.WebHtml;
using WebExpress.WebUI.WebAttribute;
using WebExpress.WebUI.WebFragment;
using WebExpress.WebApp.WebFragment;
using WebExpress.WebAttribute;
using WebExpress.WebPage;

namespace InventoryExpress.WebFragment
{
    [Section(Section.SidebarHeader)]
    [Module<Module>]
    [Scope<PageInventoryDetails>]
    [Scope<PageInventoryEdit>]
    public sealed class FragmentSidebarMediaInventory : FragmentSidebarMedia
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FragmentSidebarMediaInventory()
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
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is represented.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var guid = context.Request.GetParameter<ParameterInventoryId>();
            var inventory = ViewModel.GetInventory(guid?.Value);

            Image.Uri = inventory.Image;

            return base.Render(context);
        }
    }
}
