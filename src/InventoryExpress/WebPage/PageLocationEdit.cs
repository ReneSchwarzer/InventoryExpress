using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using InventoryExpress.Parameter;
using InventoryExpress.WebControl;
using System;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebComponent;
using WebExpress.WebResource;
using WebExpress.WebScope;
using WebExpress.WebUri;

namespace InventoryExpress.WebPage
{
    [WebExTitle("inventoryexpress:inventoryexpress.location.edit.label")]
    [WebExSegmentGuid<ParameterLocationId>("inventoryexpress:inventoryexpress.location.edit.display")]
    [WebExContextPath("/")]
    [WebExParent<PageLocations>]
    [WebExModule<Module>]
    public sealed class PageLocationEdit : PageWebApp, IPageLocation, IScope
    {
        /// <summary>
        /// Returns the form
        /// </summary>
        private ControlFormularLocation Form { get; } = new ControlFormularLocation("location")
        {
        };

        /// <summary>
        /// Liefert oder setzt den Standort
        /// </summary>
        private WebItemEntityLocation Location { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public PageLocationEdit()
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
        /// Fired when the form is about to be processed.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument./param>
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

            ComponentManager.GetComponent<NotificationManager>()?.AddNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(Culture, "inventoryexpress:inventoryexpress.location.notification.edit"),
                    new ControlLink()
                    {
                        Text = Location.Name,
                        Uri = new UriResource(ViewModel.GetLocationUri(Location.Id))
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: new UriResource(Location.Image),
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

            var guid = context.Request.GetParameter<ParameterLocationId>()?.Value;
            Location = ViewModel.GetLocation(guid);

            context.Uri.Display = Location.Name;
            context.VisualTree.Content.Primary.Add(Form);
        }
    }
}
