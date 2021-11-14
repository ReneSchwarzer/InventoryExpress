using InventoryExpress.Session;
using InventoryExpress.WebControl;
using WebExpress.Attribute;
using WebExpress.Html;
using WebExpress.Session;
using WebExpress.UI.Attribute;
using WebExpress.UI.WebComponent;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebComponent;
using WebExpress.WebPage;

namespace InventoryExpress.WebComponent
{
    [Section(Section.HeadlineSecondary)]
    [Module("inventoryexpress")]
    [Context("inventories")]
    public sealed class ComponentHeadlineToggleView : ControlFormularToggleView, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ComponentHeadlineToggleView()
            :base("toggle_view")
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public void Initialization(IComponentContext context)
        {
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
        /// Verarbeitung des Formulars
        /// </summary>
        /// <param name="sender">Der Sender</param>
        /// <param name="e">Die Eventargumente</param>
        private void OnProcessFormular(object sender, FormularEventArgs e)
        {
            var property = e.Context.Request.Session.GetOrCreateProperty<SessionPropertyToggleStatus>();
            property.ViewList = !property.ViewList;

            // Seite neu laden
            e.Context.Page.Redirecting(e.Context.Uri);
        }
    }
}
