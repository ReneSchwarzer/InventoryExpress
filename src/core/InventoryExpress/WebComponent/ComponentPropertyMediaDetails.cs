using InventoryExpress.Model;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.Attribute;
using WebExpress.UI.WebComponent;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebComponent;
using WebExpress.WebPage;

namespace InventoryExpress.WebComponent
{
    [Section(Section.PropertyPrimary)]
    [Module("inventoryexpress")]
    [Context("media")]
    public sealed class ComponentPropertyMediaDetails : ControlList, IComponent
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
        public ComponentPropertyMediaDetails()
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
        /// <param name="context">Der Kontext</param>
        public void Initialization(IComponentContext context)
        {
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var guid = context.Request.GetParameter("MediaID")?.Value;

            lock (ViewModel.Instance.Database)
            {
                var media = ViewModel.Instance.Media.Where(x => x.Guid == guid).FirstOrDefault();

                CreationDateAttribute.Value = media?.Created.ToString(context.Culture.DateTimeFormat.ShortDatePattern);
                UpdateDateAttribute.Value = media?.Updated.ToString
                (
                    $"{ context.Culture.DateTimeFormat.ShortDatePattern } { context.Culture.DateTimeFormat.ShortTimePattern }"
                );

                SizeAttribute.Value = string.Format(new FileSizeFormatProvider() { Culture = context.Culture }, "{0:fs}", media?.Data.Length);
            }

            return base.Render(context);
        }
    }
}