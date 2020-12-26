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
    [ID("SupplierMedia")]
    [Title("inventoryexpress.supplier.media.label")]
    [Segment("media", "inventoryexpress.supplier.media.display")]
    [Path("/Supplier/SupplierEdit")]
    [Module("InventoryExpress")]
    [Context("general")]
    [Context("media")]
    public sealed class PageSupplierMedia : PageTemplateWebApp, IPageManufacturer
    {
        /// <summary>
        /// Formular
        /// </summary>
        private ControlFormularMedia form;

        /// <summary>
        /// Liefert oder setzt den Lieferanten
        /// </summary>
        private Supplier Supplier { get; set; }

        /// <summary>
        /// Liefert oder setzt das Bild
        /// </summary>
        private Media Media { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageSupplierMedia()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Initialization()
        {
            base.Initialization();

            var guid = GetParamValue("SupplierID");
            Supplier = ViewModel.Instance.Suppliers.Where(x => x.Guid == guid).FirstOrDefault();
            Media = ViewModel.Instance.Media.Where(x => x.ID == Supplier.MediaID).FirstOrDefault();

            AddParam("MediaID", Media?.Guid, ParameterScope.Local);

            form = new ControlFormularMedia("media")
            {
                RedirectUrl = Uri
            };
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

            Content.Primary.Add(form);

            form.Image.Validation += (s, e) =>
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

            form.ProcessFormular += (s, e) =>
            {
                if (GetParam(form.Image.Name) is ParameterFile file)
                {
                    // Image speichern
                    if (Media == null)
                    {
                        Supplier.Media = new Media() 
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
                        // Image ändern
                        Media.Name = file.Value;
                        Media.Data = file.Data;
                        Media.Updated = DateTime.Now;
                    }
                }

                ViewModel.Instance.SaveChanges();
            };
        }

        /// <summary>
        /// In String konvertieren
        /// </summary>
        /// <returns>Das Objekt als String</returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
