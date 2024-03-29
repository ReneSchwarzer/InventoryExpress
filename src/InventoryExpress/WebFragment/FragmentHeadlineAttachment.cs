﻿using InventoryExpress.Model;
using InventoryExpress.Parameter;
using InventoryExpress.WebPage;
using System.Linq;
using WebExpress.WebHtml;
using WebExpress.Internationalization;
using WebExpress.WebUI.WebAttribute;
using WebExpress.WebUI.WebControl;
using WebExpress.WebUI.WebFragment;
using WebExpress.WebApp.WebFragment;
using WebExpress.WebAttribute;
using WebExpress.WebPage;

namespace InventoryExpress.WebFragment
{
    [Section(Section.HeadlinePreferences)]
    [Module<Module>]
    [Scope<PageInventoryDetails>]
    public sealed class FragmentHeadlineAttachment : FragmentControlLink
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FragmentHeadlineAttachment()
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
            Icon = new PropertyIcon(TypeIcon.PaperClip);
            Size = new PropertySizeText(TypeSizeText.Small);
            TextColor = new PropertyColorText(TypeColorText.Secondary);
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
            var guid = context.Request.GetParameter<ParameterInventoryId>()?.Value;
            var inventory = ViewModel.GetInventory(guid);

            if (inventory != null)
            {
                var count = ViewModel.GetInventoryAttachments(inventory).Count();

                Title = $"{InternationalizationManager.I18N(context.Culture, "inventoryexpress:inventoryexpress.inventory.attachment.function")} ({count})";
                Styles.Add(count == 0 ? "display: none;" : string.Empty);
                Uri = context.Uri.Append("attachments");
            }

            return base.Render(context);
        }
    }
}
