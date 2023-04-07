using WebExpress.Html;
using WebExpress.UI.WebFragment;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebControl;
using WebExpress.WebPage;

namespace InventoryExpress.WebFragment
{
    public abstract class FragmentSidebarMedia : FragmentControlLink
    {
        /// <summary>
        /// Das Bild
        /// </summary>
        protected ControlImage Image { get; } = new ControlImage()
        {
            Width = 180,
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two)
        };

        /// <summary>
        /// Das Formular zum Upload eines Bildes
        /// </summary>
        protected ControlModalFormularFileUpload Form { get; } = new ControlModalFormularFileUpload("BCD434C5-655C-483A-AE9A-A12B9891C7B1")
        {
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public FragmentSidebarMedia()
        {
            Content.Add(Image);
            Modal = new PropertyModal(TypeModal.Modal, Form);
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="page">Die Seite, indem die Komonente aktiv ist</param>
        public override void Initialization(IFragmentContext context, IPage page)
        {
            base.Initialization(context, page);
            Form.Upload += OnUpload;
            Form.RedirectUri = page.Uri;
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Upload-Ereignis ausgelöst wurde
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">Das Eventargument</param>
        protected abstract void OnUpload(object sender, FormularUploadEventArgs e);

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            return base.Render(context);
        }
    }
}
