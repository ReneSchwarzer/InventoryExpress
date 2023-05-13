using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
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
    [WebExID("SettingAttributeEdit")]
    [WebExTitle("inventoryexpress:inventoryexpress.attribute.edit.label")]
    [WebExSegmentGuid("AttributeID", "inventoryexpress:inventoryexpress.attribute.edit.display")]
    [WebExContextPath("/setting")]
    [WebExParent("SettingAttribute")]
    [WebExSettingHide()]
    [WebExSettingContext("webexpress.webapp:setting.tab.general.label")]
    [WebExModule("inventoryexpress")]
    [WebExContext("general")]
    [WebExContext("attributeedit")]
    public sealed class PageSettingAttributeEdit : PageWebAppSetting, IPageAttribute
    {
        /// <summary>
        /// Liefert das Formular
        /// </summary>
        private ControlFormularAttribute Form { get; } = new ControlFormularAttribute("attribute")
        {
        };

        /// <summary>
        /// Liefert oder setzt das zu ändernde Attribut
        /// </summary>
        private WebItemEntityAttribute Attribute { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageSettingAttributeEdit()
        {
        }

        /// <summary>
        /// Initialisierung
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
        /// Wird aufgerufen, wenn das Formular initialisiert wird
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void InitializeFormular(object sender, FormularEventArgs e)
        {
            Form.RedirectUri = e.Context.Uri.Take(-1);
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular gefüllt werden soll
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void FillFormular(object sender, FormularEventArgs e)
        {
            Form.AttributeName.Value = Attribute?.Name;
            Form.Description.Value = Attribute?.Description;
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Formular verarbeitet werden soll.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">Die Eventargumente/param>
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

            var guid = context.Request.GetParameter("AttributeID")?.Value;
            Attribute = ViewModel.GetAttribute(guid);

            Uri.Display = Attribute.Name;
            context.VisualTree.Content.Primary.Add(Form);
        }
    }
}
