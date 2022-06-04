using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using InventoryExpress.WebControl;
using System;
using System.IO;
using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebApp.WebAttribute;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.WebSettingPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebPageSetting
{
    [ID("SettingAttributeEdit")]
    [Title("inventoryexpress:inventoryexpress.attribute.edit.label")]
    [SegmentGuid("AttributeID", "inventoryexpress:inventoryexpress.attribute.edit.display")]
    [Path("/Setting/SettingAttribute/edit")]
    [SettingHide()]
    [SettingContext("webexpress.webapp:setting.tab.general.label")]
    [Module("inventoryexpress")]
    [Context("general")]
    [Context("attributeedit")]
    public sealed class PageSettingAttributeEdit : PageWebAppSetting, IPageAttribute
    {
        /// <summary>
        /// Liefert das Formular
        /// </summary>
        private ControlFormularAttribute Form { get; } = new ControlFormularAttribute("attribute")
        {
            Edit = true
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
        /// <param name="context">Der Kontext</param>
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
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente</param>
        private void InitializeFormular(object sender, FormularEventArgs e)
        {
            Form.RedirectUri = e.Context.Uri.Take(-1);
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular gefüllt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente</param>
        private void FillFormular(object sender, FormularEventArgs e)
        {
            Form.AttributeName.Value = Attribute?.Name;
            Form.Description.Value = Attribute?.Description;
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Formular verarbeitet werden soll.
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente/param>
        private void ProcessFormular(object sender, FormularEventArgs e)
        {
            var file = e.Context.Request.GetParameter(Form.Image.Name) as ParameterFile;
            using var transaction = ViewModel.BeginTransaction();

            // Attribut ändern und speichern
            Attribute.Name = Form.AttributeName.Value;
            Attribute.Description = Form.Description.Value;
            Attribute.Updated = DateTime.Now;
            Attribute.Media.Name = file?.Value;

            ViewModel.AddOrUpdateAttribute(Attribute);

            if (file != null)
            {
                ViewModel.AddOrUpdateMedia(Attribute.Media, file);
            }

            transaction.Commit();

            NotificationManager.CreateNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(Culture, "inventoryexpress:inventoryexpress.attribute.notification.edit"),
                    new ControlLink()
                    {
                        Text = Attribute.Name,
                        Uri = new UriRelative(ViewModel.GetAttributeUri(Attribute.Id))
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: new UriRelative(Attribute.Image),
                durability: 10000
            );
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var guid = context.Request.GetParameter("AttributeID")?.Value;
            Attribute = ViewModel.GetAttribute(guid);

            context.Request.Uri.Display = Attribute.Name;
            context.VisualTree.Content.Primary.Add(Form);
        }
    }
}
