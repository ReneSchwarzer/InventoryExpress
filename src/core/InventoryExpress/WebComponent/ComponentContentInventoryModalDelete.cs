using InventoryExpress.Model;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Html;
using WebExpress.UI.Attribute;
using WebExpress.UI.WebComponent;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebComponent;
using WebExpress.WebApp.WebControl;
using WebExpress.WebPage;
using static WebExpress.Internationalization.InternationalizationManager;

namespace InventoryExpress.WebComponent
{
    /// <summary>
    /// Modal zum Löschen eines Inventargegenstandes. Wird von der Komponetne ControlMoreInventoryDelete aufgerufen
    /// </summary>
    [Section(Section.ContentSecondary)]
    [Module("inventoryexpress")]
    [Context("inventorydetails")]
    public sealed class ComponentContentInventoryModalDelete : ControlModalFormConfirmDelete, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ComponentContentInventoryModalDelete()
           : base("del_inventory")
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public void Initialization(IComponentContext context)
        {
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
                    var guid = context.Request.GetParameter("InventoryID")?.Value;
                    var inventory = ViewModel.Instance.Inventories.Where(x => x.Guid == guid).FirstOrDefault();

                    var media = from a in ViewModel.Instance.InventoryAttachment
                                join m in ViewModel.Instance.Media
                                on a.MediaId equals m.Id
                                where a.InventoryId == inventory.Id
                                select m;

                    ViewModel.Instance.Media.RemoveRange(media);
                    ViewModel.Instance.Inventories.Remove(inventory);
                    ViewModel.Instance.SaveChanges();
                }
            };

            Header = I18N(context.Culture, "inventoryexpress:inventoryexpress.inventory.delete.label");
            Content = new ControlFormularItemStaticText() { Text = I18N(context.Culture, "inventoryexpress:inventoryexpress.inventory.delete.description") };
            RedirectUri = context.Uri.Take(-1);

            return base.Render(context);
        }
    }
}
