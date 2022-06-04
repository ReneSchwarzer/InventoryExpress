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
    [ID("ManufacturerEdit")]
    [Title("inventoryexpress:inventoryexpress.manufacturer.edit.label")]
    [SegmentGuid("ManufacturerID", "inventoryexpress:inventoryexpress.manufacturer.edit.display", SegmentGuidAttribute.Format.Simple)]
    [Path("/Manufacturer")]
    [Module("inventoryexpress")]
    [Context("general")]
    [Context("manufactureredit")]
    public sealed class PageManufacturerEdit : PageWebApp, IPageManufacturer
    {
        /// <summary>
        /// Liefert das Formular
        /// </summary>
        private ControlFormularManufacturer Form { get; } = new ControlFormularManufacturer("manufacturer")
        {
            Edit = true
        };

        /// <summary>
        /// Liefert oder setzt den Hersteller
        /// </summary>
        private WebItemEntityManufacturer Manufacturer { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageManufacturerEdit()
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
            Form.ManufacturerName.Value = Manufacturer?.Name;
            Form.Description.Value = Manufacturer?.Description;
            Form.Address.Value = Manufacturer?.Address;
            Form.Zip.Value = Manufacturer?.Zip;
            Form.Place.Value = Manufacturer?.Place;
            Form.Tag.Value = Manufacturer?.Tag;
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

            // Herstellerobjekt ändern und speichern
            Manufacturer.Name = Form.ManufacturerName.Value;
            Manufacturer.Description = Form.Description.Value;
            Manufacturer.Address = Form.Address.Value;
            Manufacturer.Zip = Form.Zip.Value;
            Manufacturer.Place = Form.Place.Value;
            Manufacturer.Tag = Form.Tag.Value;
            Manufacturer.Updated = DateTime.Now;
            Manufacturer.Media.Name = file?.Value;

            ViewModel.AddOrUpdateManufacturer(Manufacturer);

            if (file != null)
            {
                ViewModel.AddOrUpdateMedia(Manufacturer.Media, file);
            }
            
            transaction.Commit();

            NotificationManager.CreateNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(Culture, "inventoryexpress:inventoryexpress.manufacturer.notification.edit"),
                    new ControlLink()
                    {
                        Text = Manufacturer.Name,
                        Uri = new UriRelative(ViewModel.GetManufacturerUri(Manufacturer.Id))
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: new UriRelative(Manufacturer.Image),
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

            var guid = context.Request.GetParameter("ManufacturerID")?.Value;
            Manufacturer = ViewModel.GetManufacturer(guid);

            context.Request.Uri.Display = Manufacturer.Name;
            context.VisualTree.Content.Primary.Add(Form);
        }
    }
}
