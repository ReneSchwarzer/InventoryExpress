using InventoryExpress.Model;
using InventoryExpress.Parameter;
using InventoryExpress.WebPage;
using InventoryExpress.WebPageSetting;
using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebControl;
using WebExpress.UI.WebFragment;
using WebExpress.WebAttribute;
using WebExpress.WebComponent;
using WebExpress.WebPage;

namespace InventoryExpress.WebFragment
{
    [WebExSection("mediatool.secondary")]
    [Module<Module>]
    [Scope<PageSettingTemplateEdit>]
    public sealed class FragmentMediaToolDeleteTemplate : FragmentMediaToolDelete
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FragmentMediaToolDeleteTemplate()
        {
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
            var disabled = template.Media?.Image == null;

            Active = disabled ? TypeActive.Disabled : TypeActive.None;
            TextColor = disabled ? new PropertyColorText(TypeColorText.Muted) : TextColor;

            Uri = ComponentManager.SitemapManager.GetUri<PageMediaDelete>(new ParameterMediaId(template.Media?.Id));
            Modal = new PropertyModal(TypeModal.Formular, TypeModalSize.Default)
            {
                RedirectUri = ComponentManager.SitemapManager.GetUri<PageSettingTemplateEdit>(guid)
            };

            return base.Render(context);
        }
    }
}
