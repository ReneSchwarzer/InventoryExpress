using InventoryExpress.Model;
using System.Linq;
using WebExpress.WebAttribute;
using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebComponent;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebComponent;
using WebExpress.WebApp.WebControl;
using WebExpress.WebPage;
using static WebExpress.Internationalization.InternationalizationManager;

namespace InventoryExpress.WebComponent
{
    /// <summary>
    /// Modal zum Löschen einer Vorlage. Wird von der Komponetne ControlMoreTemplateDelete aufgerufen
    /// </summary>
    [Section(Section.ContentSecondary)]
    [Module("inventoryexpress")]
    [Context("templateedit")]
    public sealed class ComponentContentTemplateModalDelete : ComponentControlModalFormConfirmDelete
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ComponentContentTemplateModalDelete()
           : base("del_template")
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        /// <param name="page">Die Seite, indem die Komonente aktiv ist</param>
        public override void Initialization(IComponentContext context, IPage page)
        {
            base.Initialization(context, page);
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
                    var guid = context.Request.GetParameter("TemplateID")?.Value;
                    var template = ViewModel.Instance.Templates.Where(x => x.Guid == guid).FirstOrDefault();

                    if (template != null)
                    {
                        ViewModel.Instance.Templates.Remove(template);
                        ViewModel.Instance.SaveChanges();
                    }
                }
            };

            Header = I18N(context.Culture, "inventoryexpress:inventoryexpress.template.delete.label");
            Content = new ControlFormularItemStaticText() { Text = I18N(context.Culture, "inventoryexpress:inventoryexpress.template.delete.description") };
            RedirectUri = context.Uri.Take(-1);

            return base.Render(context);
        }
    }
}
