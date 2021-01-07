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
    [Section(Section.PropertySecondary)]
    [Application("InventoryExpress")]
    [Context("inventoryedit")]
    public sealed class ControlPropertyInventoryDelete : ControlButtonLink, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlPropertyInventoryDelete()
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
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
                var form = new ControlFormular("del") { EnableSubmitAndNextButton = false, EnableCancelButton = false, RedirectUri = Uri };
                form.SubmitButton.Text = context.Page.I18N("inventoryexpress.delete.label");
                form.SubmitButton.Icon = new PropertyIcon(TypeIcon.TrashAlt);
                form.SubmitButton.Color = new PropertyColorButton(TypeColorButton.Danger);
                form.ProcessFormular += (s, e) =>
                {
                    if (inventory != null)
                    {
                        ViewModel.Instance.Inventories.Remove(inventory);
                        ViewModel.Instance.SaveChanges();

                        context.Page.Redirecting(context.Uri.Take(-1));
                    }
                };

                Text = context.Page.I18N("inventoryexpress.delete.label");
                Icon = new PropertyIcon(TypeIcon.Trash);
                BackgroundColor = new PropertyColorButton(TypeColorButton.Danger);
                Value = inventory?.Created.ToString(context.Page.Culture.DateTimeFormat.ShortDatePattern);

                Modal = new ControlModal
                (
                    "delete",
                    context.Page.I18N("inventoryexpress.inventory.delete.label"),
                    new ControlText()
                    {
                        Text = context.Page.I18N("inventoryexpress.inventory.delete.description")
                    },
                    form
                );

                return base.Render(context);
            }
        }
    }
}
