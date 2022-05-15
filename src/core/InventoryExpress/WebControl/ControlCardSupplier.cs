﻿using InventoryExpress.Model.WebItems;
using System.Linq;
using WebExpress.Html;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebPage;

namespace InventoryExpress.WebControl
{
    public class ControlCardSupplier : ControlPanelCard
    {
        /// <summary>
        /// Liefert oder setzt den Lieferanten
        /// </summary>
        public WebItemEntitySupplier Supplier { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlCardSupplier(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
            BackgroundColor = new PropertyColorBackground(TypeColorBackground.Light);
            Styles.Add("width: fit-content;");
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
                Image = new UriRelative(Supplier.Image),
                ImageWidth = 100,
                Title = new ControlLink()
                {
                    Text = Supplier.Name,
                    Uri = new UriRelative(Supplier.Uri),
                    TextColor = new PropertyColorText(TypeColorText.Dark)
                }
            };

            media.Content.Add(new ControlText()
            {
                Text = Supplier.Description,
                Format = TypeFormatText.Paragraph
            });

            Content.Add(media);

            return base.Render(context);
        }
    }
}
