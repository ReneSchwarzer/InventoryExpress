using InventoryExpress.Model;
using InventoryExpress.WebControl;
using System;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [ID("LocationEdit")]
    [Title("inventoryexpress:inventoryexpress.location.edit.label")]
    [SegmentGuid("LocationID", "inventoryexpress:inventoryexpress.location.edit.display")]
    [Path("/Location")]
    [Module("inventoryexpress")]
    [Context("general")]
    [Context("locationedit")]
    public sealed class PageLocationEdit : PageWebApp, IPageLocation
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageLocationEdit()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var guid = context.Request.GetParameter("LocationID")?.Value;
            var location = ViewModel.Instance.Locations.Where(x => x.Guid == guid).FirstOrDefault();
            var form = new ControlFormularLocation();

            form.Edit = true;
            form.RedirectUri = context.Uri.Take(-1);
            form.BackUri = context.Uri.Take(-1);
            form.LocationName.Value = location?.Name;
            form.Description.Value = location?.Description;
            form.Address.Value = location?.Address;
            form.Zip.Value = location?.Zip;
            form.Place.Value = location?.Place;
            form.Building.Value = location?.Building;
            form.Room.Value = location?.Room;
            form.Tag.Value = location?.Tag;

            form.LocationName.Validation += (s, e) =>
            {
                if (e.Value.Count() < 1)
                {
                    e.Results.Add(new ValidationResult() { Text = this.I18N("inventoryexpress.location.validation.name.invalid"), Type = TypesInputValidity.Error });
                }
                else if (!location.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase) && ViewModel.Instance.Suppliers.Where(x => x.Name.Equals(e.Value)).Count() > 0)
                {
                    e.Results.Add(new ValidationResult() { Text = this.I18N("inventoryexpress.location.validation.name.used"), Type = TypesInputValidity.Error });
                }
            };

            form.ProcessFormular += (s, e) =>
            {
                // Standort ändern und speichern
                location.Name = form.LocationName.Value;
                location.Description = form.Description.Value;
                location.Address = form.Address.Value;
                location.Zip = form.Zip.Value;
                location.Place = form.Place.Value;
                location.Building = form.Building.Value;
                location.Room = form.Room.Value;
                location.Tag = form.Tag.Value;
                location.Updated = DateTime.Now;

                ViewModel.Instance.SaveChanges();
            };

            context.VisualTree.Content.Primary.Add(form);
        }
    }
}
