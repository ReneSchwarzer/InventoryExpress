using System.Reflection;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.WebScope;
using WebExpress.WebCore.Internationalization;
using WebExpress.WebCore.WebAttribute;
using WebExpress.WebCore.WebResource;
using WebExpress.WebUI.WebControl;

namespace InventoryExpress.WebPage
{
    [Title("inventoryexpress:inventoryexpress.help.label")]
    [Segment("help", "inventoryexpress:inventoryexpress.help.label")]
    [ContextPath("/")]
    [Module<Module>]
    [Scope<ScopeGeneral>]
    public sealed class PageHelp : PageWebApp, IPageHelp
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PageHelp()
        {
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);
        }

        /// <summary>
        /// Processing of the resource.
        /// </summary>
        /// <param name="context">The context for rendering the page.</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var visualTree = context.VisualTree;

            visualTree.Content.Primary.Add(new ControlImage()
            {
                Uri = ResourceContext.ContextPath.Append("assets/img/inventoryexpress.svg"),
                Width = 200,
                Height = 200,
                HorizontalAlignment = TypeHorizontalAlignment.Right
            });

            var card = new ControlPanelCard();

            card.Add(new ControlText()
            {
                Text = this.I18N("inventoryexpress:app.name"),
                Format = TypeFormatText.H3
            });

            card.Add(new ControlText()
            {
                Text = this.I18N("inventoryexpress:app.description"),
                Format = TypeFormatText.Paragraph
            });

            card.Add(new ControlText()
            {
                Text = this.I18N("inventoryexpress:app.privacypolicy.label"),
                Format = TypeFormatText.H3
            });

            card.Add(new ControlText()
            {
                Text = this.I18N("inventoryexpress:app.privacypolicy.description"),
                Format = TypeFormatText.Paragraph
            });

            card.Add(new ControlText()
            {
                Text = this.I18N("inventoryexpress:app.disclaimer.label"),
                Format = TypeFormatText.H3
            });

            card.Add(new ControlText()
            {
                Text = this.I18N("inventoryexpress:app.disclaimer.description"),
                Format = TypeFormatText.Paragraph
            });

            card.Add(new ControlText()
            {
                Text = this.I18N("inventoryexpress:app.about"),
                Format = TypeFormatText.H3
            });

            card.Add(new ControlText()
            {
                Text = this.I18N("inventoryexpress:app.version.label"),
                TextColor = new PropertyColorText(TypeColorText.Primary)
            });

            card.Add(new ControlText()
            {
                Text = string.Format("{0}", ResourceContext.PluginContext.Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion),
                TextColor = new PropertyColorText(TypeColorText.Dark)
            });

            card.Add(new ControlText()
            {
                Text = this.I18N("inventoryexpress:app.contact.label"),
                TextColor = new PropertyColorText(TypeColorText.Primary)
            });

            card.Add(new ControlLink()
            {
                Text = string.Format("rene_schwarzer@hotmail.de"),
                Uri = "mailto:rene_schwarzer@hotmail.de",
                TextColor = new PropertyColorText(TypeColorText.Dark)
            });

            visualTree.Content.Primary.Add(card);
        }
    }
}
