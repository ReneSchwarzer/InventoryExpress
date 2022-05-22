using InventoryExpress.WebControl;
using InventoryExpress.WebSession;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [ID("Manufacturer")]
    [Title("inventoryexpress:inventoryexpress.manufacturers.label")]
    [Segment("manufacturers", "inventoryexpress:inventoryexpress.manufacturers.label")]
    [Path("/")]
    [Module("inventoryexpress")]
    [Context("general")]
    [Context("manufacturers")]
    public sealed class PageManufacturers : PageWebApp, IPageManufacturer
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageManufacturers()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var property = context.Request.Session.GetOrCreateProperty<SessionPropertyToggleStatus>();

            if (property.ViewList)
            {
                // Listenansicht
                context.VisualTree.Content.Primary.Add(new ControlManufactorsList());
            }
            else
            {
                // Tabelarische Ansicht
                context.VisualTree.Content.Primary.Add(new ControlManufactorsTable());
            }
        }
    }
}
