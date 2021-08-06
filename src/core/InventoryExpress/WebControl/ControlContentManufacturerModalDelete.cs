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

namespace InventoryExpress.WebControl
{
    /// <summary>
    /// Modal zum Löschen eines Herstellers. Wird von der Komponetne ControlMoreManufacturerDelete aufgerufen
    /// </summary>
    [Section(Section.ContentSecondary)]
    [Application("InventoryExpress")]
    [Context("manufactureredit")]
    public sealed class ControlContentManufacturerModalDelete : ControlModal, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlContentManufacturerModalDelete()
           : base("del_manufacturer_modal")
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
                var guid = context.Page.GetParamValue("ManufacturerID");
                var manufactur = ViewModel.Instance.Manufacturers.Where(x => x.Guid == guid).FirstOrDefault();
                var form = new ControlFormular("del_form") { EnableSubmitAndNextButton = false, EnableCancelButton = false, RedirectUri = context.Page.Uri };

                form.SubmitButton.Text = context.Page.I18N("inventoryexpress.delete.label");
                form.SubmitButton.Icon = new PropertyIcon(TypeIcon.TrashAlt);
                form.SubmitButton.Color = new PropertyColorButton(TypeColorButton.Danger);
                form.ProcessFormular += (s, e) =>
                {
                    if (manufactur != null)
                    {
                        try
                        {
                            ViewModel.Instance.Manufacturers.Remove(manufactur);
                            ViewModel.Instance.SaveChanges();

                            context.Page.Redirecting(context.Uri.Take(-1));
                        }
                        catch (DbUpdateException /*ex*/)
                        {
                            //context.Page.AddMessage(context.Page.I18N("inventoryexpress.manufacturer.delete.error", MessageType.Error));
                        }
                    }
                };

                Header = context.Page.I18N("inventoryexpress.manufacturer.delete.label");
                Content.Add(new ControlText()
                {
                    Text = context.Page.I18N("inventoryexpress.manufacturer.delete.description")
                });
                Content.Add(form);

                return base.Render(context);
            }
        }
    }
}
