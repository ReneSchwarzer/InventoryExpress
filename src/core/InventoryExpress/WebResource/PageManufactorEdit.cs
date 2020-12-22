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
    [ID("ManufactorEdit")]
    [Title("inventoryexpress.manufactor.edit.label")]
    [SegmentGuid("ManufactorID", "inventoryexpress.manufactor.edit.display", SegmentGuidAttribute.Format.Simple)]
    [Path("/Manufactor")]
    [Module("InventoryExpress")]
    [Context("general")]
    public sealed class PageManufactorEdit : PageTemplateWebApp, IPageManufactor
    {
        /// <summary>
        /// Formular
        /// </summary>
        private ControlFormularManufactor form;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageManufactorEdit()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Initialization()
        {
            base.Initialization();

            form = new ControlFormularManufactor(this)
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

            var guid = GetParamValue("ManufactorID");
            var manufacturer = ViewModel.Instance.Manufacturers.Where(x => x.Guid == guid).FirstOrDefault();

            Content.Content.Add(form);

            form.ManufactorName.Value = manufacturer?.Name;
            form.Description.Value = manufacturer?.Description;

            form.ManufactorName.Validation += (s, e) =>
            {
                if (e.Value.Count() < 1)
                {
                    e.Results.Add(new ValidationResult() { Text = "Geben Sie einen gültigen Namen ein!", Type = TypesInputValidity.Error });
                }
                else if (!manufacturer.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase) && ViewModel.Instance.Suppliers.Where(x => x.Name.Equals(e.Value)).Count() > 0)
                {
                    e.Results.Add(new ValidationResult() { Text = "Der Hersteller wird bereits verwendet. Geben Sie einen anderen Namen an!", Type = TypesInputValidity.Error });
                }
            };

            form.ProcessFormular += (s, e) =>
            {
                // Herstellerobjekt ändern und speichern
                manufacturer.Name = form.ManufactorName.Value;
                manufacturer.Description = form.Description.Value;
                if (GetParam(form.Image.Name) is ParameterFile file)
                {
                    if (manufacturer.Media == null)
                    {
                        manufacturer.Media = new Media() 
                        { 
                            Name = file.Value, 
                            Data = file.Data,
                            Guid = Guid.NewGuid().ToString()
                        };
                    }
                }

                //manufacturer.Tag = form.Tag.Value;

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
