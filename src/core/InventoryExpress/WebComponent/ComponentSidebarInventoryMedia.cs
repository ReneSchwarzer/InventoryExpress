using InventoryExpress.Model;
using System;
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
    [Section(Section.SidebarHeader)]
    [Module("inventoryexpress")]
    [Context("inventorydetails")]
    [Context("attachment")]
    [Context("journal")]
    [Context("inventoryedit")]
    public sealed class ComponentSidebarInventoryMedia : ComponentControlLink
    {
        /// <summary>
        /// Das Bild
        /// </summary>
        private ControlImage Image { get; } = new ControlImage()
        {
            Width = 180,
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two)
        };

        /// <summary>
        /// Das Formular zum Upload eines Bildes
        /// </summary>
        private ControlModalFormularFileUpload Form { get; } = new ControlModalFormularFileUpload("BCD434C5-655C-483A-AE9A-A12B9891C7B1")
        {
            Header = "inventoryexpress:inventoryexpress.inventory.media.label"
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ComponentSidebarInventoryMedia()
        {
            Content.Add(Image);
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
                ViewModel.AddOrUpdateMedia(inventory, file);
            }

            transaction.Commit();

            NotificationManager.CreateNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(e.Context.Culture, "inventoryexpress:inventoryexpress.media.notification.edit"),
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
            var guid = context.Request.GetParameter("InventoryID")?.Value;
            var inventory = ViewModel.GetInventory(guid);

            Uri = context.Uri.Root.Append(guid).Append("media");
            Image.Uri = new UriRelative(inventory?.Media?.Uri);

            return base.Render(context);
        }
    }
}
