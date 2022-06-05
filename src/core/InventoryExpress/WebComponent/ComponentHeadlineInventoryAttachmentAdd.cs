using InventoryExpress.Model;
using InventoryExpress.Model.Entity;
using InventoryExpress.Model.WebItems;
using System;
using System.IO;
using System.Linq;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebComponent;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebApp.WebComponent;
using WebExpress.WebApp.WebControl;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebAttribute;
using WebExpress.WebPage;

namespace InventoryExpress.WebComponent
{
    [Section(Section.HeadlineSecondary)]
    [Module("inventoryexpress")]
    [Context("attachment")]
    public sealed class ComponentHeadlineInventoryAttachmentAdd : ComponentControlButtonLink
    {
        /// <summary>
        /// Formular zum Upload von Anhängen
        /// </summary>
        private ControlModalFormularFileUpload Form { get; } = new ControlModalFormularFileUpload("A21A40B5-29CC-4CA7-A235-79D181F1B77C")
        {
            Header = "inventoryexpress:inventoryexpress.media.file.add.label"
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ComponentHeadlineInventoryAttachmentAdd()
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
            Text = "inventoryexpress:inventoryexpress.media.file.add.label";
            Icon = new PropertyIcon(TypeIcon.Plus);
            BackgroundColor = new PropertyColorButton(TypeColorButton.Primary);
            Modal = new PropertyModal(TypeModal.Modal, Form);
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        /// <param name="page">Die Seite, indem die Komonente aktiv ist</param>
        public override void Initialization(IComponentContext context, IPage page)
        {
            base.Initialization(context, page);
            Form.Upload += OnUpload;
            Form.RedirectUri = page.Uri;
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Upload-Ereignis ausgelöst wurde
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnUpload(object sender, FormularUploadEventArgs e)
        {
            var file = e.Context.Request.GetParameter(Form.File.Name) as ParameterFile;
            var guid = e.Context.Request.GetParameter("InventoryID")?.Value;
            var inventory = ViewModel.GetInventory(guid);
            using var transaction = ViewModel.BeginTransaction();

            if (file != null)
            {
                ViewModel.AddOrUpdateInventoryAttachment(inventory, file);
            }

            transaction.Commit();

            NotificationManager.CreateNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(e.Context.Culture, "inventoryexpress:inventoryexpress.inventory.attachment.notification.label"),
                    new ControlLink()
                    {
                        Text = inventory.Name,
                        Uri = new UriRelative(ViewModel.GetInventoryUri(inventory.Id))
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: new UriRelative(ViewModel.GetMediaUri(inventory.Media.Id)),
                durability: 10000
            );
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            //var guid = context.Request.GetParameter("InventoryID")?.Value;
            //var inventory = ViewModel.GetInventory(guid);

            //Value = inventory?.Created.ToString(context.Page.Culture.DateTimeFormat.ShortDatePattern);

            return base.Render(context);
        }
    }
}
