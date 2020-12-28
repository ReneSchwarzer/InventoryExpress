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
    [Context("media")]
    public sealed class ControlPropertyMediaDelete : ControlButtonLink, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlPropertyMediaDelete()
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
            var guid = context.Page.GetParamValue("MediaID");
            var media = ViewModel.Instance.Media.Where(x => x.Guid == guid).FirstOrDefault();
            var form = new ControlFormular("del") { EnableSubmitAndNextButton = false, EnableCancelButton = false, RedirectUri = Uri };
            form.SubmitButton.Text = context.Page.I18N("inventoryexpress.delete.label");
            form.SubmitButton.Icon = new PropertyIcon(TypeIcon.TrashAlt);
            form.SubmitButton.Color = new PropertyColorButton(TypeColorButton.Danger);
            form.ProcessFormular += (s, e) =>
            {
                if (media != null)
                {
                    ViewModel.Instance.Media.Remove(media);
                    ViewModel.Instance.SaveChanges();

                    context.Page.Redirecting(context.Uri);
                }
            };

            Text = context.Page.I18N("inventoryexpress.delete.label");
            Icon = new PropertyIcon(TypeIcon.Trash);
            BackgroundColor = new PropertyColorButton(TypeColorButton.Danger);
            Value = media?.Created.ToString(context.Page.Culture.DateTimeFormat.ShortDatePattern);
            Active = media != null ? TypeActive.None : TypeActive.Disabled;

            Modal = new ControlModal
            (
                "delete",
                context.Page.I18N("inventoryexpress.media.delete.label"),
                new ControlText()
                {
                    Text = context.Page.I18N("inventoryexpress.media.delete.description")
                },
                form
            );

            return base.Render(context);
        }
    }
}
