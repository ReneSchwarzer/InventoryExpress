using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using InventoryExpress.WebControl;
using System;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebUri;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [Id("LocationEdit")]
    [Title("inventoryexpress:inventoryexpress.location.edit.label")]
    [SegmentGuid("LocationID", "inventoryexpress:inventoryexpress.location.edit.display")]
    [ContextPath("/Location")]
    [Module("inventoryexpress")]
    [Context("general")]
    [Context("locationedit")]
    public sealed class PageLocationEdit : PageWebApp, IPageLocation
    {
        /// <summary>
        /// Liefert das Formular
        /// </summary>
        private ControlFormularLocation Form { get; } = new ControlFormularLocation("location")
        {
        };

        /// <summary>
        /// Liefert oder setzt den Standort
        /// </summary>
        private WebItemEntityLocation Location { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageLocationEdit()
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
            Form.LocationName.Value = Location?.Name;
            Form.Description.Value = Location?.Description;
            Form.Address.Value = Location?.Address;
            Form.Zip.Value = Location?.Zip;
            Form.Place.Value = Location?.Place;
            Form.Building.Value = Location?.Building;
            Form.Room.Value = Location?.Room;
            Form.Tag.Value = Location?.Tag;
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Formular verarbeitet werden soll.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">Die Eventargumente/param>
        private void ProcessFormular(object sender, FormularEventArgs e)
        {
            // Standort ändern und speichern
            Location.Name = Form.LocationName.Value;
            Location.Description = Form.Description.Value;
            Location.Address = Form.Address.Value;
            Location.Zip = Form.Zip.Value;
            Location.Place = Form.Place.Value;
            Location.Building = Form.Building.Value;
            Location.Room = Form.Room.Value;
            Location.Tag = Form.Tag.Value;
            Location.Updated = DateTime.Now;

            using (var transaction = ViewModel.BeginTransaction())
            {
                ViewModel.AddOrUpdateLocation(Location);

                transaction.Commit();
            }

            NotificationManager.CreateNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(Culture, "inventoryexpress:inventoryexpress.location.notification.edit"),
                    new ControlLink()
                    {
                        Text = Location.Name,
                        Uri = new UriRelative(ViewModel.GetLocationUri(Location.Id))
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: new UriRelative(Location.Image),
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

            var guid = context.Request.GetParameter("LocationID")?.Value;
            Location = ViewModel.GetLocation(guid);

            context.Request.Uri.Display = Location.Name;
            context.VisualTree.Content.Primary.Add(Form);
        }
    }
}
