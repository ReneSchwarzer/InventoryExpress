using InventoryExpress.Model;
using InventoryExpress.WebControl;
using System;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebResource;

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
                RedirectUri = Uri.Take(-1),
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

            Content.Primary.Add(form);

            form.LocationName.Validation += (s, e) =>
            {
                if (e.Value.Count() < 1)
                {
                    e.Results.Add(new ValidationResult() { Text = this.I18N("inventoryexpress.location.validation.name.invalid"), Type = TypesInputValidity.Error });
                }
                else if (ViewModel.Instance.Locations.Where(x => x.Name.Equals(e.Value)).Count() > 0)
                {
                    e.Results.Add(new ValidationResult() { Text = this.I18N("inventoryexpress.location.validation.name.used"), Type = TypesInputValidity.Error });
                }
            };

            form.ProcessFormular += (s, e) =>
            {
                // Neues Standortobjekt erstellen und speichern
                var location = new Location()
                {
                    Name = form.LocationName.Value,
                    Description = form.Description.Value,
                    Address = form.Address.Value,
                    Zip = form.Zip.Value,
                    Place = form.Place.Value,
                    Building = form.Building.Value,
                    Room = form.Room.Value,
                    Tag = form.Tag.Value,
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                    Guid = Guid.NewGuid().ToString()
                };

                if (GetParam(form.Image.Name) is ParameterFile file)
                {
                    if (location.Media == null)
                    {
                        location.Media = new Media()
                        {
                            Name = file.Value,
                            Data = file.Data,
                            Created = DateTime.Now,
                            Updated = DateTime.Now,
                            Guid = Guid.NewGuid().ToString()
                        };
                    }
                    else
                    {
                        location.Media.Name = file.Value;
                        location.Media.Data = file.Data;
                        location.Media.Updated = DateTime.Now;
                    }
                }

                lock (ViewModel.Instance.Database)
                {
                    ViewModel.Instance.Locations.Add(location);
                    ViewModel.Instance.SaveChanges();
                }
            };
        }
    }
}
