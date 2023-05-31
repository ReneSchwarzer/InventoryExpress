using InventoryExpress.WebControl;
using InventoryExpress.WebSession;
using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebFragment;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebFragment;
using WebExpress.WebAttribute;
using WebExpress.WebPage;

namespace InventoryExpress.WebFragment
{
    [WebExSection(Section.HeadlineSecondary)]
    [WebExModule<Module>]
    [WebExContext("inventories")]
    [WebExContext("manufacturers")]
    public sealed class FragmentHeadlineToggleView : ControlFormularToggleView, IFragment
    {
        /// <summary>
        /// Liefert der Kontext
        /// </summary>
        public IFragmentContext FragmentContext { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public FragmentHeadlineToggleView()
            : base("toggle_view")
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="page">The page where the component is active.</param>
        public void Initialization(IFragmentContext context, IPage page)
        {
            FragmentContext = context;

            ProcessFormular += OnProcessFormular;
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

        /// <summary>
        /// Processing of the resource. des Formulars
        /// </summary>
        /// <param name="sender">Der Sender</param>
        /// <param name="e">The event argument.</param>
        private void OnProcessFormular(object sender, FormularEventArgs e)
        {
            var property = e.Context.Request.Session.GetOrCreateProperty<SessionPropertyToggleStatus>();
            property.ViewList = !property.ViewList;

            // Seite neu laden
            e.Context.Page.Redirecting(e.Context.Uri);
        }
    }
}
