using InventoryExpress.Model;
using Microsoft.EntityFrameworkCore;
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
    /// Modal zum Löschen eines Herstellers. Wird von der Komponetne ControlMoreManufacturerDelete aufgerufen
    /// </summary>
    [Section(Section.ContentSecondary)]
    [Module("inventoryexpress")]
    [Context("manufactureredit")]
    public sealed class ComponentContentManufacturerModalDelete : ComponentControlModalFormConfirmDelete
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ComponentContentManufacturerModalDelete()
           : base("del_manufacturer")
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        /// <param name="page">Die Seite, indem die Komonente aktiv ist</param>
        public override void Initialization(IComponentContext context, IPage page)
        {
            base.Initialization(context, page);
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
                    var guid = context.Request.GetParameter("ManufacturerID")?.Value;
                    var manufactur = ViewModel.Instance.Manufacturers.Where(x => x.Guid == guid).FirstOrDefault();

                    if (manufactur != null)
                    {
                        try
                        {
                            ViewModel.Instance.Manufacturers.Remove(manufactur);
                            ViewModel.Instance.SaveChanges();
                        }
                        catch (DbUpdateException /*ex*/)
                        {
                            //context.Page.AddMessage(context.Page.I18N("inventoryexpress.manufacturer.delete.error", MessageType.Error));
                        }
                    }
                }
            };

            Header = I18N(context.Culture, "inventoryexpress:inventoryexpress.manufacturer.delete.label");
            Content = new ControlFormularItemStaticText() { Text = I18N(context.Culture, "inventoryexpress:inventoryexpress.manufacturer.delete.description") };
            RedirectUri = context.Uri.Take(-1);

            return base.Render(context);
        }
    }
}
