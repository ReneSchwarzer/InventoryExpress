using InventoryExpress.WebControl;
using InventoryExpress.Model;
using System;
using System.Linq;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebResource;
using WebExpress.Attribute;

namespace InventoryExpress.WebResource
{
    [ID("LocationEdit")]
    [Title("inventoryexpress.location.edit.label")]
    [SegmentGuid("LocationID", "inventoryexpress.location.edit.display")]
    [Path("/Location")]
    [Module("InventoryExpress")]
    [Context("general")]
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
                RedirectUrl = Uri.Take(-1)
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

            Content.Content.Add(form);

            form.LocationName.Value = location?.Name;
            form.Description.Value = location?.Description;

            form.LocationName.Validation += (s, e) =>
            {
                if (e.Value.Count() < 1)
                {
                    e.Results.Add(new ValidationResult() { Text = "Geben Sie einen gültigen Namen ein!", Type = TypesInputValidity.Error });
                }
                else if (!location.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase) && ViewModel.Instance.Suppliers.Where(x => x.Name.Equals(e.Value)).Count() > 0)
                {
                    e.Results.Add(new ValidationResult() { Text = "Der Standort wird bereits verwendet. Geben Sie einen anderen Namen an!", Type = TypesInputValidity.Error });
                }
            };

            form.ProcessFormular += (s, e) =>
            {
                // Herstellerobjekt ändern und speichern
                location.Name = form.LocationName.Value;
                //Tag = form.Tag.Value;
                location.Description = form.Description.Value;

                ViewModel.Instance.SaveChanges();
            };
        }
    }
}
