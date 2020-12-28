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
    [Context("supplieredit")]
    public sealed class ControlPropertySupplierDelete : ControlButtonLink, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlPropertySupplierDelete()
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
            var guid = context.Page.GetParamValue("SupplierID");
            var supplier = ViewModel.Instance.Suppliers.Where(x => x.Guid == guid).FirstOrDefault();
            var form = new ControlFormular("del") { EnableSubmitAndNextButton = false, EnableCancelButton = false, RedirectUri = Uri };
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

            Text = context.Page.I18N("inventoryexpress.delete.label");
            Icon = new PropertyIcon(TypeIcon.Trash);
            BackgroundColor = new PropertyColorButton(TypeColorButton.Danger);
            Value = supplier?.Created.ToString(context.Page.Culture.DateTimeFormat.ShortDatePattern);

            Modal = new ControlModal
            (
                "delete",
                context.Page.I18N("inventoryexpress.supplier.delete.label"),
                new ControlText()
                {
                    Text = context.Page.I18N("inventoryexpress.supplier.delete.description")
                },
                form
            );

            return base.Render(context);
        }
    }
}
