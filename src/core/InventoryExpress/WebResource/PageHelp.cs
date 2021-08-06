using System.Reflection;
using WebExpress.Attribute;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebApp.WebResource;

namespace InventoryExpress.WebResource
{
    [ID("Help")]
    [Title("inventoryexpress.help.label")]
    [Segment("help", "inventoryexpress.help.label")]
    [Path("/")]
    [Module("InventoryExpress")]
    [Context("general")]
    public sealed class PageHelp : PageTemplateWebApp, IPageHelp
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageHelp()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Initialization()
        {
            base.Initialization();
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();

            Content.Primary.Add(new ControlImage()
            {
                Uri = Uri.Root.Append("assets/img/inventoryexpress.svg"),
                Width = 200,
                Height = 200,
                HorizontalAlignment = TypeHorizontalAlignment.Right
            });

            var card = new ControlPanelCard();

            card.Add(new ControlText()
            {
                Text = this.I18N("app.name"),
                Format = TypeFormatText.H3
            });

            card.Add(new ControlText()
            {
                Text = this.I18N("app.description"),
                Format = TypeFormatText.Paragraph
            });

            card.Add(new ControlText()
            {
                Text = this.I18N("app.privacypolicy.label"),
                Format = TypeFormatText.H3
            });

            card.Add(new ControlText()
            {
                Text = this.I18N("app.privacypolicy.description"),
                Format = TypeFormatText.Paragraph
            });

            card.Add(new ControlText()
            {
                Text = this.I18N("app.disclaimer.label"),
                Format = TypeFormatText.H3
            });

            card.Add(new ControlText()
            {
                Text = this.I18N("app.disclaimer.description"),
                Format = TypeFormatText.Paragraph
            });

            card.Add(new ControlText()
            {
                Text = this.I18N("app.about"),
                Format = TypeFormatText.H3
            });

            card.Add(new ControlText()
            {
                Text = this.I18N("app.version.label"),
                TextColor = new PropertyColorText(TypeColorText.Primary)
            });

            card.Add(new ControlText()
            {
                Text = string.Format("{0}", Context.Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion),
                TextColor = new PropertyColorText(TypeColorText.Dark)
            });

            card.Add(new ControlText()
            {
                Text = this.I18N("app.contact.label"),
                TextColor = new PropertyColorText(TypeColorText.Primary)
            });

            card.Add(new ControlLink()
            {
                Text = string.Format("rene_schwarzer@hotmail.de"),
                Uri = new UriAbsolute()
                {
                    Scheme = UriScheme.Mailto,
                    Authority = new UriAuthority("rene_schwarzer@hotmail.de")
                },
                TextColor = new PropertyColorText(TypeColorText.Dark)
            });

            Content.Primary.Add(card);
        }
    }
}
