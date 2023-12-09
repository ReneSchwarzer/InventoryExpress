using InventoryExpress.Model;
using InventoryExpress.Parameter;
using InventoryExpress.WebPage;
using System.Linq;
using WebExpress.WebApp.WebFragment;
using WebExpress.WebCore.WebAttribute;
using WebExpress.WebCore.WebHtml;
using WebExpress.WebCore.WebPage;
using WebExpress.WebCore.WebUri;
using WebExpress.WebUI.WebAttribute;
using WebExpress.WebUI.WebControl;
using WebExpress.WebUI.WebFragment;

namespace InventoryExpress.WebFragment
{
    [Section(Section.ContentSecondary)]
    [Order(int.MinValue)]
    [Module<Module>]
    [Scope<PageInventoryDetails>]
    public sealed class FragmentContentInventoryTag : FragmentControlPanelFlexbox
    {
        /// <summary>
        /// Die Liste mit den Tag-Links
        /// </summary>
        private ControlLinkList TagList { get; } = new ControlLinkList()
        {
            TextColor = new PropertyColorText(TypeColorText.Secondary),
            Icon = new PropertyIcon(TypeIcon.Tag)
        };

        /// <summary>
        /// Constructor
        /// </summary>
        public FragmentContentInventoryTag()
        {
            Padding = new PropertySpacingPadding(PropertySpacing.Space.Null, PropertySpacing.Space.Five, PropertySpacing.Space.Two, PropertySpacing.Space.Two);
            Layout = TypeLayoutFlexbox.Default;
            Justify = TypeJustifiedFlexbox.End;
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="page">The page where the component is active.</param>
        public override void Initialization(IFragmentContext context, IPage page)
        {
            base.Initialization(context, page);

            Content.Add(TagList);
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is represented.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            TagList.Links.Clear();

            var guid = context.Request.GetParameter<ParameterInventoryId>()?.Value;
            var tags = ViewModel.GetInventoryTags(guid);

            TagList.Links.AddRange(tags.Select(x => new ControlLink() { Text = x.Label, Uri = new UriFragment() }));

            return base.Render(context);
        }
    }
}
