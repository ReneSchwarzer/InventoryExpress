using InventoryExpress.Model;
using System.Linq;
using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebComponent;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebComponent;
using WebExpress.WebApp.WebControl;
using WebExpress.WebAttribute;
using WebExpress.WebPage;
using static WebExpress.Internationalization.InternationalizationManager;

namespace InventoryExpress.WebComponent
{
    /// <summary>
    /// Modal zum Löschen eines Standortes. Wird von der Komponetne ControlMoreLocationDelete aufgerufen
    /// </summary>
    [Section(Section.ContentSecondary)]
    [Module("inventoryexpress")]
    [Context("locationedit")]
    public sealed class ComponentContentLocationModalDelete : ComponentControlModalFormConfirmDelete
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ComponentContentLocationModalDelete()
           : base("del_location")
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IComponentContext context)
        {
            base.Initialization(context);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Confirm += (s, e) =>
            {
                lock (ViewModel.Instance.Database)
                {
                    var guid = context.Request.GetParameter("LocationID")?.Value;
                    var location = ViewModel.Instance.Locations.Where(x => x.Guid == guid).FirstOrDefault();

                    if (location != null)
                    {
                        ViewModel.Instance.Locations.Remove(location);
                        ViewModel.Instance.SaveChanges();
                    }
                }
            };

            Header = I18N(context.Culture, "inventoryexpress:inventoryexpress.location.delete.label");
            Content = new ControlFormularItemStaticText() { Text = I18N(context.Culture, "inventoryexpress:inventoryexpress.location.delete.description") };
            RedirectUri = context.Uri.Take(-1);

            return base.Render(context);
        }
    }
}
