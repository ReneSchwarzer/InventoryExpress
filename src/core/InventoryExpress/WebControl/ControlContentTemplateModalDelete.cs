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
    /// Modal zum Löschen einer Vorlage. Wird von der Komponetne ControlMoreTemplateDelete aufgerufen
    /// </summary>
    [Section(Section.ContentSecondary)]
    [Application("InventoryExpress")]
    [Context("templateedit")]
    public sealed class ControlContentTemplateModalDelete : ControlModalFormConfirmDelete, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlContentTemplateModalDelete()
           : base("del_template")
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
                    var guid = context.Page.GetParamValue("TemplateID");
                    var template = ViewModel.Instance.Templates.Where(x => x.Guid == guid).FirstOrDefault();

                    if (template != null)
                    {
                        ViewModel.Instance.Templates.Remove(template);
                        ViewModel.Instance.SaveChanges();
                    }
                }
            };

            Header = context.Page.I18N("inventoryexpress.template.delete.label");
            Content = new ControlFormularItemStaticText() { Text = context.I18N("inventoryexpress.template.delete.description") };
            RedirectUri = context.Uri.Take(-1);

            return base.Render(context);
        }
    }
}
