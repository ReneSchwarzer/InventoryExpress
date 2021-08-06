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
    /// Modal zum Löschen eines Lieferanten. Wird von der Komponetne ControlMoreSupplierDelete aufgerufen
    /// </summary>
    [Section(Section.ContentSecondary)]
    [Application("InventoryExpress")]
    [Context("supplieredit")]
    public sealed class ControlContentSupplierModalDelete : ControlModal, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlContentSupplierModalDelete()
           : base("del_supplier_modal")
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
                var guid = context.Page.GetParamValue("SupplierID");
                var supplier = ViewModel.Instance.Suppliers.Where(x => x.Guid == guid).FirstOrDefault();
                var form = new ControlFormular("del_form") { EnableSubmitAndNextButton = false, EnableCancelButton = false, RedirectUri = context.Page.Uri };

                form.SubmitButton.Text = context.Page.I18N("inventoryexpress.delete.label");
                form.SubmitButton.Icon = new PropertyIcon(TypeIcon.TrashAlt);
                form.SubmitButton.Color = new PropertyColorButton(TypeColorButton.Danger);
                form.ProcessFormular += (s, e) =>
                {
                    if (supplier != null)
                    {
                        ViewModel.Instance.Suppliers.Remove(supplier);
                        ViewModel.Instance.SaveChanges();

                        context.Page.Redirecting(context.Uri.Take(-1));
                    }
                };

                Header = context.Page.I18N("inventoryexpress.supplier.delete.label");
                Content.Add(new ControlText()
                {
                    Text = context.Page.I18N("inventoryexpress.supplier.delete.description")
                });
                Content.Add(form);

                return base.Render(context);
            }
        }
    }
}
