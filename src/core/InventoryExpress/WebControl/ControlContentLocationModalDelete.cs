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
    /// Modal zum Löschen eines Standortes. Wird von der Komponetne ControlMoreLocationDelete aufgerufen
    /// </summary>
    [Section(Section.ContentSecondary)]
    [Application("InventoryExpress")]
    [Context("locationedit")]
    public sealed class ControlContentLocationModalDelete : ControlModal, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlContentLocationModalDelete()
           : base("del_location_modal")
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
                var guid = context.Page.GetParamValue("LocationID");
                var location = ViewModel.Instance.Locations.Where(x => x.Guid == guid).FirstOrDefault();
                var form = new ControlFormular("del_form") { EnableSubmitAndNextButton = false, EnableCancelButton = false, RedirectUri = context.Page.Uri };

                form.SubmitButton.Text = context.Page.I18N("inventoryexpress.delete.label");
                form.SubmitButton.Icon = new PropertyIcon(TypeIcon.TrashAlt);
                form.SubmitButton.Color = new PropertyColorButton(TypeColorButton.Danger);
                form.ProcessFormular += (s, e) =>
                {
                    if (location != null)
                    {
                        ViewModel.Instance.Locations.Remove(location);
                        ViewModel.Instance.SaveChanges();

                        context.Page.Redirecting(context.Uri.Take(-1));
                    }
                };

                Header = context.Page.I18N("inventoryexpress.location.delete.label");
                Content.Add(new ControlText()
                {
                    Text = context.Page.I18N("inventoryexpress.location.delete.description")
                });
                Content.Add(form);

                return base.Render(context);
            }
        }
    }
}
