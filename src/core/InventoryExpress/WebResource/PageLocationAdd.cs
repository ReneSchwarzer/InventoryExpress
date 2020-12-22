using InventoryExpress.WebControl;
using InventoryExpress.Model;
using System;
using System.Linq;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebResource;
using WebExpress.Attribute;

namespace InventoryExpress.WebResource
{
    [ID("LocationAdd")]
    [Title("inventoryexpress.location.add.label")]
    [Segment("add", "inventoryexpress.location.add.label")]
    [Path("/Location")]
    [Module("InventoryExpress")]
    [Context("general")]
    public sealed class PageLocationAdd : PageTemplateWebApp, IPageLocation
    {
        /// <summary>
        /// Formular
        /// </summary>
        private ControlFormularLocation form;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageLocationAdd()
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

            Content.Content.Add(form);

            form.LocationName.Validation += (s, e) =>
            {
                if (e.Value.Count() < 1)
                {
                    e.Results.Add(new ValidationResult() { Text = "Geben Sie einen  gültigen Namen ein!", Type = TypesInputValidity.Error });
                }
                else if (ViewModel.Instance.Locations.Where(x => x.Name.Equals(e.Value)).Count() > 0)
                {
                    e.Results.Add(new ValidationResult() { Text = "Der Hersteller wird bereits verwendet. Geben Sie einen anderen Namen an!", Type = TypesInputValidity.Error });
                }
            };

            form.ProcessFormular += (s, e) =>
            {
                // Neues Standortobjekt erstellen und speichern
                var location = new Location()
                {
                    Name = form.LocationName.Value,
                    //Tag = form.Tag.Value,
                    Description = form.Description.Value,
                    Guid = Guid.NewGuid().ToString()
                };

                ViewModel.Instance.Locations.Add(location);
                ViewModel.Instance.SaveChanges();
            };
        }
    }
}
