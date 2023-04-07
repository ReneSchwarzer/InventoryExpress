using InventoryExpress.WebPage;
using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebControl;
using WebExpress.UI.WebFragment;
using WebExpress.WebUri;
using WebExpress.WebApp.WebFragment;
using WebExpress.WebAttribute;
using WebExpress.WebPage;

namespace InventoryExpress.WebFragment
{
    [Section(Section.AppQuickcreateSecondary)]
    [Module("inventoryexpress")]
    public sealed class FragmentQuickCreateLedgerAccount : FragmentControlSplitButtonItemLink
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public FragmentQuickCreateLedgerAccount()
            : base()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="page">Die Seite, indem die Komonente aktiv ist</param>
        public override void Initialization(IFragmentContext context, IPage page)
        {
            base.Initialization(context, page);

            Text = "inventoryexpress:inventoryexpress.ledgeraccount.label";
            Uri = UriRelative.Combine(page.ContextPath, "ledgeraccounts/add");
            Icon = new PropertyIcon(TypeIcon.At);
            Modal = new PropertyModal(TypeModal.Formular, TypeModalSize.Large);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Active = context.Page is IPageLedgerAccount ? TypeActive.Active : TypeActive.None;

            return base.Render(context);
        }

    }
}
