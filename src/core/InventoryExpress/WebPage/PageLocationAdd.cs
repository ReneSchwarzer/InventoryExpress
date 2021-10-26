using InventoryExpress.Model;
using InventoryExpress.WebControl;
using System;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [ID("LocationAdd")]
    [Title("inventoryexpress:inventoryexpress.location.add.label")]
    [Segment("add", "inventoryexpress:inventoryexpress.location.add.label")]
    [Path("/Location")]
    [Module("inventoryexpress")]
    [Context("general")]
    public sealed class PageLocationAdd : PageWebApp, IPageLocation
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageLocationAdd()
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

            var form = new ControlFormularLocation();
            form.EnableCancelButton = true;
            form.RedirectUri = context.Request.Uri.Take(-1);
            form.BackUri = context.Request.Uri.Take(-1);

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

                if (context.Request.GetParameter(form.Image.Name) is ParameterFile file)
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

            context.VisualTree.Content.Primary.Add(form);
        }
    }
}
