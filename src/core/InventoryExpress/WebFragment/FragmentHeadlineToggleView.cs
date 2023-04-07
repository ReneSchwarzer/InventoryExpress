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
    [Section(Section.HeadlineSecondary)]
    [Module("inventoryexpress")]
    [Context("inventories")]
    [Context("manufacturers")]
    public sealed class FragmentHeadlineToggleView : ControlFormularToggleView, IFragment
    {
        /// <summary>
        /// Liefert der Kontext
        /// </summary>
        public IFragmentContext Context { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public FragmentHeadlineToggleView()
            : base("toggle_view")
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="page">Die Seite, indem die Komonente aktiv ist</param>
        public void Initialization(IFragmentContext context, IPage page)
        {
            Context = context;

            ProcessFormular += OnProcessFormular;
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
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
