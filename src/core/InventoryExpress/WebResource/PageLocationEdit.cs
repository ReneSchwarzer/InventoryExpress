using InventoryExpress.WebControl;
using InventoryExpress.Model;
using System;
using System.Linq;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebResource;
using WebExpress.Attribute;
using WebExpress.Message;
using WebExpress.Internationalization;

namespace InventoryExpress.WebResource
{
    [ID("LocationEdit")]
    [Title("inventoryexpress.location.edit.label")]
    [SegmentGuid("LocationID", "inventoryexpress.location.edit.display")]
    [Path("/Location")]
    [Module("InventoryExpress")]
    [Context("general")]
    [Context("locationedit")]
    public sealed class PageLocationEdit : PageTemplateWebApp, IPageLocation
    {
        /// <summary>
        /// Formular
        /// </summary>
        private ControlFormularLocation form;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageLocationEdit()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Initialization()
        {
            base.Initialization();

            form = new ControlFormularLocation()
            {
                RedirectUri = Uri.Take(-1),
                Edit = true,
                EnableCancelButton = true,
                BackUri = Uri.Take(-1),
            };
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();

            var guid = GetParamValue("LocationID");
            var location = ViewModel.Instance.Locations.Where(x => x.Guid == guid).FirstOrDefault();

            Content.Primary.Add(form);

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
        }
    }
}
