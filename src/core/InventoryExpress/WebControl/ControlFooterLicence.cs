using System.Linq;
using WebExpress.Attribute;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.Plugin;
using WebExpress.UI.Attribute;
using WebExpress.UI.Component;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebApp.Components;

namespace InventoryExpress.WebControl
{
    [Section(Section.FooterPrimary)]
    [Application("InventoryExpress")]
    public sealed class ControlFooterLicence : ControlPanel, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlFooterLicence()
            : base()
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {

        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var webexpress = PluginManager.Plugins.Where(x => x.PluginID == "webexpress").FirstOrDefault();
            Classes.Add("text-center");

            Content.Add(new ControlLink()
            {
                Text = context.I18N("inventoryexpress.footer.licence.label"),
                Uri = new UriAbsolute(context.I18N("inventoryexpress.footer.licence.uri")),
                TextColor = new PropertyColorText(TypeColorText.Muted),
                Size = new PropertySizeText(TypeSizeText.Small)
            });

            return base.Render(context);
        }
    }
}
