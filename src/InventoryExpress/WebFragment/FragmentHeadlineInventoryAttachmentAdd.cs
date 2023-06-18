using InventoryExpress.Model;
using InventoryExpress.Parameter;
using InventoryExpress.WebPage;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebControl;
using WebExpress.UI.WebFragment;
using WebExpress.WebApp.WebControl;
using WebExpress.WebApp.WebFragment;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebAttribute;
using WebExpress.WebComponent;
using WebExpress.WebMessage;
using WebExpress.WebPage;

namespace InventoryExpress.WebFragment
{
    [WebExSection(Section.HeadlineSecondary)]
    [Module<Module>]
    [Scope<PageInventoryAttachments>]
    public sealed class FragmentHeadlineInventoryAttachmentAdd : FragmentControlButtonLink
    {
        /// <summary>
        /// Formular zum Upload von Anhängen
        /// </summary>
        private ControlModalFormularFileUpload Form { get; } = new ControlModalFormularFileUpload("A21A40B5-29CC-4CA7-A235-79D181F1B77C")
        {
            Header = "inventoryexpress:inventoryexpress.media.file.add.label"
        };

        /// <summary>
        /// Constructor
        /// </summary>
        public FragmentHeadlineInventoryAttachmentAdd()
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
            Text = "inventoryexpress:inventoryexpress.media.file.add.label";
            Icon = new PropertyIcon(TypeIcon.Plus);
            BackgroundColor = new PropertyColorButton(TypeColorButton.Primary);
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
        }

        /// <summary>
        /// Fired when the upload event was triggered.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void OnUpload(object sender, FormularUploadEventArgs e)
        {
            var file = e.Context.Request.GetParameter(Form.File.Name) as ParameterFile;
            var guid = e.Context.Request.GetParameter<ParameterInventoryId>()?.Value;
            var inventory = ViewModel.GetInventory(guid);

            if (file != null)
            {
                using var transaction = ViewModel.BeginTransaction();

                ViewModel.AddOrUpdateInventoryAttachment(inventory, file);

                transaction.Commit();
            }

            ComponentManager.GetComponent<NotificationManager>()?.AddNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(e.Context.Culture, "inventoryexpress:inventoryexpress.inventory.attachment.notification.label"),
                    new ControlLink()
                    {
                        Text = inventory.Name,
                        Uri = ViewModel.GetInventoryUri(inventory.Id)
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: ViewModel.GetMediaUri(inventory.Media.Id),
                durability: 10000
            );
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is represented.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var guid = context.Request.GetParameter<ParameterInventoryId>();
            Form.RedirectUri = ComponentManager.SitemapManager.GetUri<PageInventoryAttachments>(guid);

            //var inventory = ViewModel.GetInventory(Guid);

            //Value = inventory?.Created.ToString(context.Page.Culture.DateTimeFormat.ShortDatePattern);

            return base.Render(context);
        }
    }
}
