﻿using InventoryExpress.WebPage;
using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebFragment;
using WebExpress.WebApp.WebFragment;
using WebExpress.WebAttribute;
using WebExpress.WebPage;

namespace InventoryExpress.WebFragment
{
    [WebExSection(Section.Metadata)]
    [WebExModule<Module>]
    [WebExScope<PageInventoryDetails>]
    public sealed class FragmentHeadlineInventoryMetadata : FragmentControlText
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FragmentHeadlineInventoryMetadata()
        {
            //Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
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
            //lock (ViewModel.Instance.Database)
            //{
            //    var id = context.Request.GetParameter("InventoryId")?.Value;
            //    var inventory = ViewModel.Instance.Inventories.Where(x => x.Guid.Equals(id)).FirstOrDefault();

            //    Text = string.Format(I18N(context.Culture, "inventoryexpress:inventoryexpress.inventory.metadata.created"), inventory.Created.ToString("d", context.Culture));

            //    if (inventory.Created != inventory.Updated)
            //    {
            //        Text += " ";
            //        Text += string.Format(I18N(context.Culture, "inventoryexpress:inventoryexpress.inventory.metadata.lastchange"), inventory.Updated.ToString("d", context.Culture));
            //    }
            //}

            return base.Render(context);
        }
    }
}
