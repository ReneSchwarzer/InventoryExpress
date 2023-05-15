using InventoryExpress.Model;
using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebControl;
using WebExpress.UI.WebFragment;
using WebExpress.WebApp.WebFragment;
using WebExpress.WebAttribute;
using WebExpress.WebPage;

namespace InventoryExpress.WebFragment
{
    [WebExSection(Section.MoreSecondary)]
    [WebExModule(typeof(Module))]
    [WebExContext("templateedit")]
    public sealed class FragmentMoreTemplateDelete : FragmentControlDropdownItemLink
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public FragmentMoreTemplateDelete()
        {
            TextColor = new PropertyColorText(TypeColorText.Danger);
            Text = "inventoryexpress:inventoryexpress.delete.label";
            Icon = new PropertyIcon(TypeIcon.Trash);
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="page">Die Seite, indem die Komonente aktiv ist</param>
        public override void Initialization(IFragmentContext context, IPage page)
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
            var guid = context.Request.GetParameter("TemplateID")?.Value;
            var template = ViewModel.GetTemplate(guid);
            var inUse = ViewModel.GetTemplateInUse(template);

            Active = inUse ? TypeActive.Disabled : TypeActive.None;
            TextColor = inUse ? new PropertyColorText(TypeColorText.Muted) : TextColor;

            Uri = context.Uri.Append("del");
            Modal = new PropertyModal(TypeModal.Formular, TypeModalSize.Default) { RedirectUri = context.ApplicationContext.ContextPath.Append("setting/templates") };

            return base.Render(context);
        }
    }
}
