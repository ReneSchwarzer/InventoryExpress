using InventoryExpress.WebControl;
using InventoryExpress.Model;
using System;
using System.Linq;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebResource;
using WebExpress.Attribute;
using WebExpress.Message;

namespace InventoryExpress.WebResource
{
    [ID("LocationMedia")]
    [Title("inventoryexpress.location.media.label")]
    [Segment("media", "inventoryexpress.location.media.display")]
    [Path("/Location/LocationEdit")]
    [Module("InventoryExpress")]
    [Context("general")]
    [Context("media")]
    public sealed class PageLocationMedia : PageTemplateWebApp, IPageLocation
    {
        /// <summary>
        /// Formular
        /// </summary>
        private ControlFormularMedia Form { get; set; }

        /// <summary>
        /// Liefert oder setzt den Standort
        /// </summary>
        private Location Location { get; set; }

        /// <summary>
        /// Liefert oder setzt das Bild
        /// </summary>
        private Media Media { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageLocationMedia()
        {
            
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Initialization()
        {
            base.Initialization();

            Form = new ControlFormularMedia("media")
            {
                RedirectUri = Uri,
                EnableCancelButton = true,
                BackUri = Uri.Take(-1)
            };

            var guid = GetParamValue("LocationID");
            Location = ViewModel.Instance.Locations.Where(x => x.Guid == guid).FirstOrDefault();
            Media = ViewModel.Instance.Media.Where(x => x.Id == (Location != null ? Location.MediaId : null)).FirstOrDefault();

            AddParam("MediaID", Media?.Guid, ParameterScope.Local);
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();

            Content.Preferences.Add(new ControlImage() 
            { 
                Uri = Media != null? Uri.Root.Append($"media/{Media.Guid}") : Uri.Root.Append("/assets/img/inventoryexpress.svg"),
                Width = 400,
                Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.Three)
            });

            Content.Primary.Add(Form);

            Form.Tag.Value = Media?.Tag;

            Form.Image.Validation += (s, e) =>
            {
                //if (e.Value.Count() < 1)
                //{
                //    e.Results.Add(new ValidationResult() { Text = "Geben Sie einen gültigen Namen ein!", Type = TypesInputValidity.Error });
                //}
                //else if (!manufactur.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase) && ViewModel.Instance.Suppliers.Where(x => x.Name.Equals(e.Value)).Count() > 0)
                //{
                //    e.Results.Add(new ValidationResult() { Text = "Der Hersteller wird bereits verwendet. Geben Sie einen anderen Namen an!", Type = TypesInputValidity.Error });
                //}
            };

            Form.ProcessFormular += (s, e) =>
            {
                if (GetParam(Form.Image.Name) is ParameterFile file)
                {
                    // Image speichern
                    if (Media == null)
                    {
                        Location.Media = new Media() 
                        { 
                            Name = file.Value, 
                            Data = file.Data,
                            Tag = Form.Tag.Value,
                            Created = DateTime.Now,
                            Updated = DateTime.Now,
                            Guid = Guid.NewGuid().ToString()
                        };
                    }
                    else
                    {
                        // Image ändern
                        Media.Name = file.Value;
                        Media.Data = file.Data;
                        Media.Tag = Form.Tag.Value;
                        Media.Updated = DateTime.Now;
                    }
                }

                if (Form.Tag.Value != Media?.Tag)
                {
                    Location.Media.Tag = Form.Tag.Value;
                }

                ViewModel.Instance.SaveChanges();
            };
        }
    }
}
