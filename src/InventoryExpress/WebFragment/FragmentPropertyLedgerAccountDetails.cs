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
    [WebExSection(Section.PropertyPrimary)]
    [WebExModule(typeof(Module))]
    [WebExContext("ledgeraccountedit")]
    public sealed class FragmentPropertyLedgerAccountDetails : FragmentControlList
    {
        /// <summary>
        /// Das Erstellungsdatum
        /// </summary>
        private ControlAttribute CreationDateAttribute { get; } = new ControlAttribute()
        {
            TextColor = new PropertyColorText(TypeColorText.Secondary),
            Icon = new PropertyIcon(TypeIcon.CalendarPlus),
            Name = "inventoryexpress:inventoryexpress.ledgeraccount.creationdate.label"
        };

        /// <summary>
        /// Das Datum der letzten Änderung
        /// </summary>
        private ControlAttribute UpdateDateAttribute { get; } = new ControlAttribute()
        {
            TextColor = new PropertyColorText(TypeColorText.Secondary),
            Icon = new PropertyIcon(TypeIcon.Save),
            Name = "inventoryexpress:inventoryexpress.ledgeraccount.updatedate.label"
        };

        /// <summary>
        /// Constructor
        /// </summary>
        public FragmentPropertyLedgerAccountDetails()
        {
            Layout = TypeLayoutList.Flush;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);

            Add(new ControlListItem(CreationDateAttribute));
            Add(new ControlListItem(UpdateDateAttribute));
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="page">The page where the component is active.</param>
        public override void Initialization(IFragmentContext context, IPage page)
        {
            base.Initialization(context, page);
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is represented.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var Guid = context.Request.GetParameter("LedgerAccountId")?.Value;
            var ledgerAccount = ViewModel.GetLedgerAccount(Guid);

            CreationDateAttribute.Value = ledgerAccount?.Created.ToString(context.Culture.DateTimeFormat.ShortDatePattern);
            UpdateDateAttribute.Value = ledgerAccount?.Updated.ToString
            (
                $"{context.Culture.DateTimeFormat.ShortDatePattern} {context.Culture.DateTimeFormat.ShortTimePattern}"
            );

            return base.Render(context);
        }
    }
}
