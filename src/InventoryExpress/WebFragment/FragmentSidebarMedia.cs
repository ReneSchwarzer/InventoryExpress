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
        /// Constructor
        /// </summary>
        public FragmentSidebarMedia()
        {
            Content.Add(Image);
            Modal = new PropertyModal(TypeModal.Modal, Form);
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="page">The page where the component is active.</param>
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
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is represented.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            return base.Render(context);
        }
    }
}
