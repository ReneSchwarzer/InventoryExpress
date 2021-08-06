using InventoryExpress.Model;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.Attribute;
using WebExpress.UI.Component;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.Components;

namespace InventoryExpress.WebControl
{
    /// <summary>
    /// Modal zum löschen eines Inventargegenstandes. Wird von der Komponetne ControlMoreInventoryDelete aufgerufen
    /// </summary>
    [Section(Section.ContentSecondary)]
    [Application("InventoryExpress")]
    [Context("inventorydetails")]
    public sealed class ControlContentInventoryModalDelete : ControlModal, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlContentInventoryModalDelete()
           : base("del_inventory_modal")
        {
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            lock (ViewModel.Instance.Database)
            {
                var guid = context.Page.GetParamValue("InventoryID");
                var inventory = ViewModel.Instance.Inventories.Where(x => x.Guid == guid).FirstOrDefault();
                var form = new ControlFormular("del_form") { EnableSubmitAndNextButton = false, EnableCancelButton = false, RedirectUri = context.Page.Uri };

                form.SubmitButton.Text = context.Page.I18N("inventoryexpress.delete.label");
                form.SubmitButton.Icon = new PropertyIcon(TypeIcon.TrashAlt);
                form.SubmitButton.Color = new PropertyColorButton(TypeColorButton.Danger);
                form.ProcessFormular += (s, e) =>
                {
                    if (inventory != null)
                    {
                        var media = from a in ViewModel.Instance.InventoryAttachment
                                    join m in ViewModel.Instance.Media
                                    on a.MediaId equals m.Id
                                    where a.InventoryId == inventory.Id
                                    select m;

                        ViewModel.Instance.Media.RemoveRange(media);
                        ViewModel.Instance.Inventories.Remove(inventory);
                        ViewModel.Instance.SaveChanges();

                        context.Page.Redirecting(context.Uri.Take(-1));
                    }
                };

                Header = context.Page.I18N("inventoryexpress.inventory.delete.label");
                Content.Add(new ControlText()
                {
                    Text = context.Page.I18N("inventoryexpress.inventory.delete.description")
                });
                Content.Add(form);

                return base.Render(context);
            }
        }
    }
}
