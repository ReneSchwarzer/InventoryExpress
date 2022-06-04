﻿using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using InventoryExpress.WebControl;
using System.IO;
using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [ID("SupplierMedia")]
    [Title("inventoryexpress:inventoryexpress.supplier.media.label")]
    [Segment("media", "inventoryexpress:inventoryexpress.supplier.media.display")]
    [Path("/Supplier/SupplierEdit")]
    [Module("inventoryexpress")]
    [Context("general")]
    [Context("media")]
    [Context("mediaedit")]
    [Context("supplieredit")]
    public sealed class PageSupplierMedia : PageWebApp, IPageSupplier
    {
        /// <summary>
        /// Formular
        /// </summary>
        private ControlFormularMedia Form { get; } = new ControlFormularMedia("media");

        /// <summary>
        /// Liefert oder setzt den Lieferanten
        /// </summary>
        private WebItemEntitySupplier Supplier { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageSupplierMedia()
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
            Form.Tag.Value = Supplier.Media?.Tag;
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

            if (file != null)
            {
                ViewModel.AddOrUpdateMedia(Supplier.Media, file);
            }

            transaction.Commit();

            NotificationManager.CreateNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(Culture, "inventoryexpress:inventoryexpress.media.notification.edit"),
                    new ControlLink()
                    {
                        Text = Supplier.Name,
                        Uri = new UriRelative(ViewModel.GetSupplierUri(Supplier.Id))
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: new UriRelative(Supplier.Image),
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

            var guid = context.Request.GetParameter("SupplierID")?.Value;
            Supplier = ViewModel.GetSupplier(guid);

            context.VisualTree.Content.Preferences.Add(new ControlImage()
            {
                Uri = Supplier.Media != null ? new UriRelative(Supplier.Media?.Uri) : context.Uri.Root.Append("/assets/img/inventoryexpress.svg"),
                Width = 400,
                Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.Three)
            });

            context.VisualTree.Content.Primary.Add(Form);
        }
    }
}
