using System.Collections.Generic;
using System.Linq;
using WebExpress.WebCore.WebComponent;
using WebExpress.WebCore.WebHtml;
using WebExpress.WebCore.WebPage;
using WebExpress.WebUI.WebControl;
using WebExpress.WebUI.WebFragment;

namespace InventoryExpress.WebFragment
{
    public abstract class FragmentSidebarMedia : FragmentControlPanelTool
    {
        /// <summary>
        /// Retruns or sets an enumeration of the primary tools identified.
        /// </summary>
        private IEnumerable<FragmentCacheItem> PrimaryFragments { get; set; }

        /// <summary>
        /// Retruns or sets an enumeration of the secondary tools identified.
        /// </summary>
        private IEnumerable<FragmentCacheItem> SecondaryFragments { get; set; }

        /// <summary>
        /// Returns the picture.
        /// </summary>
        protected ControlImage Image { get; } = new ControlImage()
        {
            Width = 180,
        };

        /// <summary>
        /// Constructor
        /// </summary>
        public FragmentSidebarMedia()
        {
            Border = new PropertyBorder(false);
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
            Content.Add(Image);
            Tools.Icon = new PropertyIcon(TypeIcon.EllipsisHorizontal);
            Tools.BackgroundColor = new PropertyColorButton(TypeColorButton.Light);
            Tools.Size = TypeSizeButton.Small;
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="page">The page where the component is active.</param>
        public override void Initialization(IFragmentContext context, IPage page)
        {
            base.Initialization(context, page);

            var fragmentManager = ComponentManager.GetComponent<FragmentManager>();
            PrimaryFragments = fragmentManager.GetCacheableFragments<IControlDropdownItem>("mediatool.primary", page);
            SecondaryFragments = fragmentManager.GetCacheableFragments<IControlDropdownItem>("mediatool.secondary", page);

        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is represented.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var primaryControls = PrimaryFragments.SelectMany(x => x.CreateInstance<IControlDropdownItem>(context.Page, context.Request));
            var secondaryControls = SecondaryFragments.SelectMany(x => x.CreateInstance<IControlDropdownItem>(context.Page, context.Request));

            Tools.Items.Clear();
            Tools.Items.AddRange(primaryControls);
            if (primaryControls.Any() && secondaryControls.Any())
            {
                Tools.AddSeperator();
            }
            Tools.Items.AddRange(secondaryControls);

            return base.Render(context);
        }
    }
}
