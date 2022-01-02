using InventoryExpress.Model;
using InventoryExpress.Model.Entity;
using InventoryExpress.WebControl;
using System;
using System.IO;
using System.Linq;
using WebExpress.Message;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
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
        /// Liefert das Formular
        /// </summary>
        private ControlFormularLocation Form { get; } = new ControlFormularLocation("location")
        {
            Edit = true
        };

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

            Form.InitializeFormular += InitializeFormular;
            Form.FillFormular += FillFormular;
            Form.LocationName.Validation += LocationNameValidation;
            Form.Zip.Validation += ZipValidation;
            Form.ProcessFormular += ProcessFormular;
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Formular verarbeitet werden soll.
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente/param>
        private void ProcessFormular(object sender, FormularEventArgs e)
        {
            lock (ViewModel.Instance.Database)
            {
                var guid = e.Context.Request.GetParameter("LocationID")?.Value;
                var location = ViewModel.Instance.Locations.Where(x => x.Guid == guid).FirstOrDefault();

                // Standort ändern und speichern
                location.Name = Form.LocationName.Value;
                location.Description = Form.Description.Value;
                location.Address = Form.Address.Value;
                location.Zip = Form.Zip.Value;
                location.Place = Form.Place.Value;
                location.Building = Form.Building.Value;
                location.Room = Form.Room.Value;
                location.Tag = Form.Tag.Value;
                location.Updated = DateTime.Now;

                ViewModel.Instance.SaveChanges();

                if (e.Context.Request.GetParameter(Form.Image.Name) is ParameterFile file)
                {
                    if (location.Media == null)
                    {
                        location.Media = new Media()
                        {
                            Name = file.Value,
                            Created = DateTime.Now,
                            Updated = DateTime.Now,
                            Guid = Guid.NewGuid().ToString()
                        };
                    }
                    else
                    {
                        location.Media.Name = file.Value;
                        location.Media.Updated = DateTime.Now;
                    }

                    File.WriteAllBytes(Path.Combine(e.Context.Application.AssetPath, "media", location.Media.Guid), file.Data);
                }
            }
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Feld Zip validiert werden soll.
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente/param>
        private void ZipValidation(object sender, ValidationEventArgs e)
        {
            if (e.Value != null && e.Value.Count() >= 10)
            {
                e.Results.Add(new ValidationResult() { Text = "inventoryexpress:inventoryexpress.location.validation.zip.tolong", Type = TypesInputValidity.Error });
            }
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Feld LocationName validiert werden soll.
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente/param>
        private void LocationNameValidation(object sender, ValidationEventArgs e)
        {
            lock (ViewModel.Instance.Database)
            {
                var guid = e.Context.Request.GetParameter("LocationID")?.Value;
                var location = ViewModel.Instance.Locations.Where(x => x.Guid == guid).FirstOrDefault();

                if (e.Value.Count() < 1)
                {
                    e.Results.Add(new ValidationResult() { Text = "inventoryexpress:inventoryexpress.location.validation.name.invalid", Type = TypesInputValidity.Error });
                }
                else if (!location.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase) && ViewModel.Instance.Suppliers.Where(x => x.Name.Equals(e.Value)).Count() > 0)
                {
                    e.Results.Add(new ValidationResult() { Text = "inventoryexpress:inventoryexpress.location.validation.name.used", Type = TypesInputValidity.Error });
                }
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular initialisiert wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente</param>
        private void InitializeFormular(object sender, FormularEventArgs e)
        {
            Form.RedirectUri = e.Context.Uri.Take(-1);
            Form.BackUri = e.Context.Uri.Take(-1);
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular gefüllt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente</param>
        private void FillFormular(object sender, FormularEventArgs e)
        {
            lock (ViewModel.Instance.Database)
            {
                var guid = e.Context.Request.GetParameter("LocationID")?.Value;
                var location = ViewModel.Instance.Locations.Where(x => x.Guid == guid).FirstOrDefault();

                Form.LocationName.Value = location?.Name;
                Form.Description.Value = location?.Description;
                Form.Address.Value = location?.Address;
                Form.Zip.Value = location?.Zip;
                Form.Place.Value = location?.Place;
                Form.Building.Value = location?.Building;
                Form.Room.Value = location?.Room;
                Form.Tag.Value = location?.Tag;
            }
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            lock (ViewModel.Instance.Database)
            {
                var guid = context.Request.GetParameter("LocationID")?.Value;
                var location = ViewModel.Instance.Locations.Where(x => x.Guid == guid).FirstOrDefault();

                context.Request.Uri.Display = location.Name;
                context.VisualTree.Content.Primary.Add(Form);
            }
        }
    }
}
