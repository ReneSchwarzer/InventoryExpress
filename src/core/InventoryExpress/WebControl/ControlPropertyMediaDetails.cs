using InventoryExpress.Model;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.Attribute;
using WebExpress.UI.Component;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.Components;

namespace InventoryExpress.WebControl
{
    [Section(Section.PropertyPrimary)]
    [Application("InventoryExpress")]
    [Context("media")]
    public sealed class ControlPropertyMediaDetails : ControlList, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlPropertyMediaDetails()
        {
            Layout = TypeLayoutList.Flush;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);

        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var guid = context.Page.GetParamValue("MediaID");
            var media = ViewModel.Instance.Media.Where(x => x.Guid == guid).FirstOrDefault();

            Add(new ControlListItem(new ControlAttribute()
            {
                Name = context.Page.I18N("inventoryexpress.media.creationdate.label") + ":",
                Icon = new PropertyIcon(TypeIcon.CalendarPlus),
                Value = media?.Created.ToString(context.Culture.DateTimeFormat.ShortDatePattern),
                TextColor = new PropertyColorText(TypeColorText.Secondary)
            }));

            Add(new ControlListItem(new ControlAttribute()
            {
                Name = context.Page.I18N("inventoryexpress.media.updatedate.label") + ":",
                Icon = new PropertyIcon(TypeIcon.Save),
                Value = media?.Updated.ToString(context.Culture.DateTimeFormat.ShortDatePattern + " " + context.Culture.DateTimeFormat.ShortTimePattern),
                TextColor = new PropertyColorText(TypeColorText.Secondary)
            }));

            Add(new ControlListItem(new ControlAttribute()
            {
                Name = context.Page.I18N("inventoryexpress.media.size.label") + ":",
                Icon = new PropertyIcon(TypeIcon.Hdd),
                Value = string.Format(new FileSizeFormatProvider() { Culture = context.Culture }, "{0:fs}", media?.Data.Length),
                TextColor = new PropertyColorText(TypeColorText.Secondary)
            }));

            return base.Render(context);
        }
    }
}
