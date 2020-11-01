﻿using InventoryExpress.Model;
using WebExpress.Html;
using WebExpress.Pages;
using WebExpress.UI.Controls;

namespace InventoryExpress.Controls
{
    public class ControlCardManufactor : ControlPanelCard
    {
        /// <summary>
        /// Liefert oder setzt den Hersteller
        /// </summary>
        public Manufacturer Manufactur { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlCardManufactor(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Two);
            BackgroundColor = new PropertyColorBackground(TypeColorBackground.Light);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var media = new ControlPanelMedia()
            {
                //Image = new UriRelative(string.IsNullOrWhiteSpace(Manufactur.Image) ? "/Assets/img/Logo.png" : "/data/" + Manufactur.Image),
                ImageWidth = 100,
                ImageHeight = 100,
                Title = new ControlLink()
                {
                    Text = Manufactur.Name,
                    Uri = context.Page.Uri.Append(Manufactur.ID.ToString()),
                    TextColor = new PropertyColorText(TypeColorText.Dark)
                }
            };

            media.Content.Add(new ControlText()
            {
                Text = Manufactur.Discription,
                Format = TypeFormatText.Paragraph
            });

            Content.Add(media);

            return base.Render(context);
        }
    }
}
