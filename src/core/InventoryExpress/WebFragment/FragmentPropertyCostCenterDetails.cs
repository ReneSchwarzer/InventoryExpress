using InventoryExpress.Model;
using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebControl;
using WebExpress.UI.WebFragment;
using WebExpress.WebApp.WebFragment;
using WebExpress.WebAttribute;
using WebExpress.WebPage;

namespace InventoryExpress.WebFragment
{
    /// <summary>
    /// Anzeige der Metadaten einer Kostenstelle
    /// </summary>
    [WebExSection(Section.PropertyPrimary)]
    [WebExModule(typeof(Module))]
    [WebExContext("costcenteredit")]
    public sealed class FragmentPropertyCostCenterDetails : FragmentControlList
    {
        /// <summary>
        /// Das Erstellungsdatum
        /// </summary>
        private ControlAttribute CreationDateAttribute { get; } = new ControlAttribute()
        {
            TextColor = new PropertyColorText(TypeColorText.Secondary),
            Icon = new PropertyIcon(TypeIcon.CalendarPlus),
            Name = "inventoryexpress:inventoryexpress.costcenter.creationdate.label"
        };

        /// <summary>
        /// Das Datum der letzten Änderung
        /// </summary>
        private ControlAttribute UpdateDateAttribute { get; } = new ControlAttribute()
        {
            TextColor = new PropertyColorText(TypeColorText.Secondary),
            Icon = new PropertyIcon(TypeIcon.Save),
            Name = "inventoryexpress:inventoryexpress.costcenter.updatedate.label"
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public FragmentPropertyCostCenterDetails()
        {
            Layout = TypeLayoutList.Flush;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);

            Add(new ControlListItem(CreationDateAttribute));
            Add(new ControlListItem(UpdateDateAttribute));
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
            var guid = context.Request.GetParameter("CostCenterID")?.Value;
            var costCenter = ViewModel.GetCostCenter(guid);

            CreationDateAttribute.Value = costCenter?.Created.ToString(context.Culture.DateTimeFormat.ShortDatePattern);
            UpdateDateAttribute.Value = costCenter?.Updated.ToString
            (
                $"{context.Culture.DateTimeFormat.ShortDatePattern} {context.Culture.DateTimeFormat.ShortTimePattern}"
            );

            return base.Render(context);
        }
    }
}
