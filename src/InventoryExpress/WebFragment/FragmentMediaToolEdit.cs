using WebExpress.WebApp.WebControl;
using WebExpress.WebCore.WebHtml;
using WebExpress.WebCore.WebPage;
using WebExpress.WebCore.WebUri;
using WebExpress.WebUI.WebControl;
using WebExpress.WebUI.WebFragment;

namespace InventoryExpress.WebFragment
{
    public abstract class FragmentMediaToolEdit : FragmentControlDropdownItemLink
    {
        /// <summary>
        /// Returns the form for uploading an image.
        /// </summary>
        protected ControlModalFormularFileUpload Form { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public FragmentMediaToolEdit(string id)
        {
            Form = new ControlModalFormularFileUpload(id);
            TextColor = new PropertyColorText(TypeColorText.Default);
            Uri = new UriFragment();
            Text = "inventoryexpress:inventoryexpress.edit.label";
            Icon = new PropertyIcon(TypeIcon.Edit);
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="page">The page where the component is active.</param>
        public override void Initialization(IFragmentContext context, IPage page)
        {
            base.Initialization(context, page);

            Modal = new PropertyModal(TypeModal.Modal, Form);
            Form.Upload += OnUpload;
        }

        /// <summary>
        /// Fired when the upload event was triggered.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
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
