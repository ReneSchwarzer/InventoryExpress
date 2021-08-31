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
    /// Modal zum Löschen eines Standortes. Wird von der Komponetne ControlMoreLocationDelete aufgerufen
    /// </summary>
    [Section(Section.ContentSecondary)]
    [Application("InventoryExpress")]
    [Context("locationedit")]
    public sealed class ControlContentLocationModalDelete : ControlModalFormConfirmDelete, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlContentLocationModalDelete()
           : base("del_location")
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
                    var guid = context.Page.GetParamValue("LocationID");
                    var location = ViewModel.Instance.Locations.Where(x => x.Guid == guid).FirstOrDefault();

                    if (location != null)
                    {
                        ViewModel.Instance.Locations.Remove(location);
                        ViewModel.Instance.SaveChanges();
                    }
                }
            };

            Header = context.Page.I18N("inventoryexpress.location.delete.label");
            Content = new ControlFormularItemStaticText() { Text = context.I18N("inventoryexpress.location.delete.description") };
            RedirectUri = context.Uri.Take(-1);

            return base.Render(context);
        }
    }
}
