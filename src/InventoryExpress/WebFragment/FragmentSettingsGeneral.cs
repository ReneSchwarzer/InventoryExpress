using InventoryExpress.WebPage;
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
    [Section(Section.AppSettingsSecondary)]
    [Module<Module>]
    public sealed class FragmentSettingsGeneral : FragmentControlDropdownItemLink
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FragmentSettingsGeneral()
            : base()
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

            Text = "inventoryexpress:inventoryexpress.setting.label";
            Uri = ComponentManager.SitemapManager.GetUri<PageSettingGeneral>();
            Icon = new PropertyIcon(TypeIcon.Cog);
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is represented.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Active = context.Page is IPageSetting ? TypeActive.Active : TypeActive.None;

            return base.Render(context);
        }

    }
}
