﻿using InventoryExpress.Model;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebControl;
using WebExpress.UI.WebFragment;
using WebExpress.WebApp.WebFragment;
using WebExpress.WebAttribute;
using WebExpress.WebPage;

namespace InventoryExpress.WebFragment
{
    [Section(Section.PropertyPrimary)]
    [WebExModule("inventoryexpress")]
    [WebExContext("media")]
    public sealed class FragmentPropertyMediaDetails : FragmentControlList
    {
        /// <summary>
        /// Das Erstellungsdatum
        /// </summary>
        private ControlAttribute CreationDateAttribute { get; } = new ControlAttribute()
        {
            TextColor = new PropertyColorText(TypeColorText.Secondary),
            Icon = new PropertyIcon(TypeIcon.CalendarPlus),
            Name = "inventoryexpress:inventoryexpress.media.creationdate.label"
        };

        /// <summary>
        /// Das Datum der letzten Änderung
        /// </summary>
        private ControlAttribute UpdateDateAttribute { get; } = new ControlAttribute()
        {
            TextColor = new PropertyColorText(TypeColorText.Secondary),
            Icon = new PropertyIcon(TypeIcon.Save),
            Name = "inventoryexpress:inventoryexpress.media.updatedate.label"
        };

        /// <summary>
        /// Das Datum der letzten Änderung
        /// </summary>
        private ControlAttribute SizeAttribute { get; } = new ControlAttribute()
        {
            TextColor = new PropertyColorText(TypeColorText.Secondary),
            Icon = new PropertyIcon(TypeIcon.Hdd),
            Name = "inventoryexpress:inventoryexpress.media.size.label"
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public FragmentPropertyMediaDetails()
        {
            Layout = TypeLayoutList.Flush;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);

            Add(new ControlListItem(CreationDateAttribute));
            Add(new ControlListItem(UpdateDateAttribute));
            Add(new ControlListItem(SizeAttribute));
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
            var guid = context.Request.GetParameter("MediaID")?.Value;
            var media = ViewModel.GetMedia(guid);

            CreationDateAttribute.Value = media?.Created.ToString(context.Culture.DateTimeFormat.ShortDatePattern);
            UpdateDateAttribute.Value = media?.Updated.ToString
            (
                $"{context.Culture.DateTimeFormat.ShortDatePattern} {context.Culture.DateTimeFormat.ShortTimePattern}"
            );

            SizeAttribute.Value = string.Format(new FileSizeFormatProvider() { Culture = context.Culture }, "{0:fs}", media?.Size);

            return base.Render(context);
        }
    }
}