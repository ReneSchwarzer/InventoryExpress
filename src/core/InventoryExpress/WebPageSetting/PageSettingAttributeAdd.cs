﻿using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using InventoryExpress.WebControl;
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
    [WebExID("SettingAttributeAdd")]
    [WebExTitle("inventoryexpress:inventoryexpress.attribute.add.label")]
    [WebExSegment("add", "inventoryexpress:inventoryexpress.attribute.add.label")]
    [WebExContextPath("/setting")]
    [WebExParent("SettingAttribute")]
    [WebExSettingHide()]
    [WebExSettingContext("webexpress.webapp:setting.tab.general.label")]
    [WebExModule("inventoryexpress")]
    [WebExContext("general")]
    [WebExContext("attributeadd")]
    public sealed class PageSettingAttributeAdd : PageWebAppSetting, IPageAttribute
    {
        /// <summary>
        /// Liefert das Formular
        /// </summary>
        private ControlFormularAttribute Form { get; } = new ControlFormularAttribute("attribute")
        {
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageSettingAttributeAdd()
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
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Formular verarbeitet werden soll.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">Die Eventargumente/param>
        private void ProcessFormular(object sender, FormularEventArgs e)
        {
            // Neues Attribut erstellen und speichern
            var attribute = new WebItemEntityAttribute()
            {
                Name = Form.AttributeName.Value,
                Description = Form.Description.Value
            };

            using (var transaction = ViewModel.BeginTransaction())
            {
                ViewModel.AddOrUpdateAttribute(attribute);

                transaction.Commit();
            }

            ComponentManager.GetComponent<NotificationManager>()?.AddNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(Culture, "inventoryexpress:inventoryexpress.attribute.notification.add"),
                    new ControlLink()
                    {
                        Text = attribute.Name,
                        Uri = ViewModel.GetAttributeUri(attribute.Id)
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: attribute.Image,
                durability: 10000
            );

            //Form.RedirectUri = Form.RedirectUri.Append(attribute.Id);
        }

        /// <summary>
        /// Processing of the resource.
        /// </summary>
        /// <param name="context">The context for rendering the page.</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            context.VisualTree.Content.Primary.Add(Form);
        }
    }
}
