using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using InventoryExpress.WebControl;
using System;
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
    [ID("SupplierEdit")]
    [Title("inventoryexpress:inventoryexpress.supplier.edit.label")]
    [SegmentGuid("SupplierID", "inventoryexpress:inventoryexpress.supplier.edit.display")]
    [Path("/Supplier")]
    [Module("inventoryexpress")]
    [Context("general")]
    [Context("supplieredit")]
    public sealed class PageSupplierEdit : PageWebApp, IPageSupplier
    {
        /// <summary>
        /// Liefert das Formular
        /// </summary>
        private ControlFormularSupplier Form { get; } = new ControlFormularSupplier("supplier")
        {
            Edit = true
        };

        /// <summary>
        /// Liefert oder setzt den Lieferanten
        /// </summary>
        private WebItemEntitySupplier Supplier { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageSupplierEdit()
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
            Form.SupplierName.Value = Supplier?.Name;
            Form.Description.Value = Supplier?.Description;
            Form.Address.Value = Supplier?.Address;
            Form.Zip.Value = Supplier?.Zip;
            Form.Place.Value = Supplier?.Place;
            Form.Tag.Value = Supplier?.Tag;
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

            // Lieferant ändern und speichern
            Supplier.Name = Form.SupplierName.Value;
            Supplier.Description = Form.Description.Value;
            Supplier.Address = Form.Address.Value;
            Supplier.Zip = Form.Zip.Value;
            Supplier.Place = Form.Place.Value;
            Supplier.Tag = Form.Tag.Value;
            Supplier.Updated = DateTime.Now;
            Supplier.Media.Name = file?.Value;

            ViewModel.AddOrUpdateSupplier(Supplier);

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
                    InternationalizationManager.I18N(Culture, "inventoryexpress:inventoryexpress.supplier.notification.edit"),
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

            context.Request.Uri.Display = Supplier.Name;
            context.VisualTree.Content.Primary.Add(Form);
        }
    }
}
