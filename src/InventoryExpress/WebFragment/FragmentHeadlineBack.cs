﻿using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebFragment;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebFragment;
using WebExpress.WebAttribute;
using WebExpress.WebPage;

namespace InventoryExpress.WebFragment
{
    [WebExSection(Section.HeadlinePrologue)]
    [WebExModule(typeof(Module))]
    [WebExContext("attachment")]
    [WebExContext("inventoryedit")]
    [WebExContext("costcenteredit")]
    [WebExContext("ledgeraccountedit")]
    [WebExContext("locationedit")]
    [WebExContext("manufactureredit")]
    [WebExContext("supplieredit")]
    [WebExContext("templateedit")]
    [WebExContext("templateadd")]
    [WebExContext("mediaedit")]
    [WebExContext("journal")]
    public sealed class FragmentHeadlineBack : FragmentControlButtonLink
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FragmentHeadlineBack()
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
            Icon = new PropertyIcon(TypeIcon.ArrowLeft);
            Outline = true;
            BackgroundColor = new PropertyColorButton(TypeColorButton.Secondary);
            Text = "inventoryexpress:inventoryexpress.inventory.attachment.back";
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
            Uri = context.Uri.Take(-1);

            return base.Render(context);
        }
    }
}