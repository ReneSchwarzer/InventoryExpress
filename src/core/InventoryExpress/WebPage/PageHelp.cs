﻿using System.Reflection;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [ID("Help")]
    [Title("inventoryexpress:inventoryexpress.help.label")]
    [Segment("help", "inventoryexpress:inventoryexpress.help.label")]
    [Path("/")]
    [Module("inventoryexpress")]
    [Context("general")]
    public sealed class PageHelp : PageWebApp, IPageHelp
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
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var visualTree = context.VisualTree;

            visualTree.Content.Primary.Add(new ControlImage()
            {
                Uri = context.Uri.Root.Append("assets/img/inventoryexpress.svg"),
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
                Text = string.Format("{0}", Context.Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion),
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
                Uri = new UriAbsolute()
                {
                    Scheme = UriScheme.Mailto,
                    Authority = new UriAuthority("rene_schwarzer@hotmail.de")
                },
                TextColor = new PropertyColorText(TypeColorText.Dark)
            });

            visualTree.Content.Primary.Add(card);
        }
    }
}
