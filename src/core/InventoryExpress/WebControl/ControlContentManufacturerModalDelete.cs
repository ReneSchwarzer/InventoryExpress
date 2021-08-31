using InventoryExpress.Model;
using Microsoft.EntityFrameworkCore;
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
    /// Modal zum Löschen eines Herstellers. Wird von der Komponetne ControlMoreManufacturerDelete aufgerufen
    /// </summary>
    [Section(Section.ContentSecondary)]
    [Application("InventoryExpress")]
    [Context("manufactureredit")]
    public sealed class ControlContentManufacturerModalDelete : ControlModalFormConfirmDelete, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlContentManufacturerModalDelete()
           : base("del_manufacturer")
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
                    var guid = context.Page.GetParamValue("ManufacturerID");
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

            Header = context.Page.I18N("inventoryexpress.manufacturer.delete.label");
            Content = new ControlFormularItemStaticText() { Text = context.I18N("inventoryexpress.manufacturer.delete.description") };
            RedirectUri = context.Uri.Take(-1);

            return base.Render(context);
        }
    }
}
