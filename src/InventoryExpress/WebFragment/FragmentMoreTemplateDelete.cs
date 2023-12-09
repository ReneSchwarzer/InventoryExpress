using InventoryExpress.Model;
using InventoryExpress.Parameter;
using InventoryExpress.WebPageSetting;
using WebExpress.WebApp.WebFragment;
using WebExpress.WebCore.WebAttribute;
using WebExpress.WebCore.WebComponent;
using WebExpress.WebCore.WebHtml;
using WebExpress.WebCore.WebPage;
using WebExpress.WebUI.WebAttribute;
using WebExpress.WebUI.WebControl;
using WebExpress.WebUI.WebFragment;

namespace InventoryExpress.WebFragment
{
    [Section(Section.MoreSecondary)]
    [Module<Module>]
    [Scope<PageSettingTemplateEdit>]
    public sealed class FragmentMoreTemplateDelete : FragmentControlDropdownItemLink
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FragmentMoreTemplateDelete()
        {
            TextColor = new PropertyColorText(TypeColorText.Danger);
            Text = "inventoryexpress:inventoryexpress.delete.label";
            Icon = new PropertyIcon(TypeIcon.Trash);
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="page">The page where the component is active.</param>
        public override void Initialization(IFragmentContext context, IPage page)
        {
            base.Initialization(context, page);
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is represented.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var guid = context.Request.GetParameter<ParameterTemplateId>();
            var template = ViewModel.GetTemplate(guid?.Value);
            var inUse = ViewModel.GetTemplateInUse(template);

            Active = inUse ? TypeActive.Disabled : TypeActive.None;
            TextColor = inUse ? new PropertyColorText(TypeColorText.Muted) : TextColor;

            Uri = ComponentManager.SitemapManager.GetUri<PageSettingTemplateDelete>(guid);
            Modal = new PropertyModal(TypeModal.Formular, TypeModalSize.Default)
            {
                RedirectUri = ComponentManager.SitemapManager.GetUri<PageSettingTemplates>()
            };

            return base.Render(context);
        }
    }
}
