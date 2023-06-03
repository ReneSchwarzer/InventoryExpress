using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using InventoryExpress.Parameter;
using InventoryExpress.WebControl;
using System;
using WebExpress.Internationalization;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.WebSettingPage;
using WebExpress.WebAttribute;
using WebExpress.WebComponent;
using WebExpress.WebResource;

namespace InventoryExpress.WebPageSetting
{
    [WebExTitle("inventoryexpress:inventoryexpress.attribute.edit.label")]
    [WebExSegmentGuid<ParameterAttributeId>("inventoryexpress:inventoryexpress.attribute.edit.display")]
    [WebExContextPath("/edit")]
    [WebExParent<PageSettingAttributes>]
    [WebExSettingHide()]
    [WebExSettingContext("webexpress.webapp:setting.tab.general.label")]
    [WebExModule<Module>]
    public sealed class PageSettingAttributeEdit : PageWebAppSetting, IPageAttribute
    {
        /// <summary>
        /// Returns the form
        /// </summary>
        private ControlFormularAttribute Form { get; } = new ControlFormularAttribute("attribute")
        {
        };

        /// <summary>
        /// Liefert oder setzt das zu ändernde Attribut
        /// </summary>
        private WebItemEntityAttribute Attribute { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public PageSettingAttributeEdit()
        {
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            Form.InitializeFormular += InitializeFormular;
            Form.FillFormular += FillFormular;
            Form.ProcessFormular += ProcessFormular;
        }

        /// <summary>
        /// Invoked when the form is initialized.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void InitializeFormular(object sender, FormularEventArgs e)
        {
            Form.RedirectUri = e.Context.Uri.Take(-1);
        }

        /// <summary>
        /// Invoked when the form is about to be populated.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void FillFormular(object sender, FormularEventArgs e)
        {
            Form.AttributeName.Value = Attribute?.Name;
            Form.Description.Value = Attribute?.Description;
        }

        /// <summary>
        /// Fired when the form is about to be processed.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument./param>
        private void ProcessFormular(object sender, FormularEventArgs e)
        {
            // Attribut ändern und speichern
            Attribute.Name = Form.AttributeName.Value;
            Attribute.Description = Form.Description.Value;
            Attribute.Updated = DateTime.Now;

            using (var transaction = ViewModel.BeginTransaction())
            {
                ViewModel.AddOrUpdateAttribute(Attribute);

                transaction.Commit();
            }

            ComponentManager.GetComponent<NotificationManager>()?.AddNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(Culture, "inventoryexpress:inventoryexpress.attribute.notification.edit"),
                    new ControlLink()
                    {
                        Text = Attribute.Name,
                        Uri = ViewModel.GetAttributeUri(Attribute.Id)
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: Attribute.Image,
                durability: 10000
            );
        }

        /// <summary>
        /// Processing of the resource.
        /// </summary>
        /// <param name="context">The context for rendering the page.</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var guid = context.Request.GetParameter("AttributeId")?.Value;
            Attribute = ViewModel.GetAttribute(guid);

            Uri.Display = Attribute.Name;
            context.VisualTree.Content.Primary.Add(Form);
        }
    }
}
