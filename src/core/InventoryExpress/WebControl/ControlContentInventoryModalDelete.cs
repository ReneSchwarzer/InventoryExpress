using InventoryExpress.Model;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.Attribute;
using WebExpress.UI.Component;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.Components;
using WebExpress.WebApp.WebControl;

namespace InventoryExpress.WebControl
{
    /// <summary>
    /// Modal zum Löschen eines Inventargegenstandes. Wird von der Komponetne ControlMoreInventoryDelete aufgerufen
    /// </summary>
    [Section(Section.ContentSecondary)]
    [Application("InventoryExpress")]
    [Context("inventorydetails")]
    public sealed class ControlContentInventoryModalDelete : ControlModalFormConfirmDelete, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlContentInventoryModalDelete()
           : base("del_inventory")
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
                    var guid = context.Page.GetParamValue("InventoryID");
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

            Header = context.Page.I18N("inventoryexpress.inventory.delete.label");
            Content = new ControlFormularItemStaticText() { Text = context.I18N("inventoryexpress.inventory.delete.description") };
            RedirectUri = context.Uri.Take(-1);

            return base.Render(context);
        }
    }
}
