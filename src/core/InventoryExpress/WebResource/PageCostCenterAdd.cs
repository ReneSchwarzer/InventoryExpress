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
    [ID("CostCenterAdd")]
    [Title("inventoryexpress.costcenter.add.label")]
    [Segment("add", "inventoryexpress.costcenter.add.label")]
    [Path("/CostCenter")]
    [Module("InventoryExpress")]
    [Context("general")]
    public sealed class PageCostCenterAdd : PageTemplateWebApp, IPageCostCenter
    {
        /// <summary>
        /// Formular
        /// </summary>
        private ControlFormularCostCenter form;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageCostCenterAdd()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Initialization()
        {
            base.Initialization();

            form = new ControlFormularCostCenter()
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

            form.CostCenterName.Validation += (s, e) =>
            {
                if (e.Value.Count() < 1)
                {
                    e.Results.Add(new ValidationResult() { Text = this.I18N("inventoryexpress.costcenter.validation.name.invalid"), Type = TypesInputValidity.Error });
                }
                else if (ViewModel.Instance.CostCenters.Where(x => x.Name.Equals(e.Value)).Count() > 0)
                {
                    e.Results.Add(new ValidationResult() { Text = this.I18N("inventoryexpress.costcenter.validation.name.used"), Type = TypesInputValidity.Error });
                }
            };

            form.ProcessFormular += (s, e) =>
            {
                // Neue Kostenstelle erstellen und speichern
                var costcenter = new CostCenter()
                {
                    Name = form.CostCenterName.Value,
                    Description = form.Description.Value,
                    Tag = form.Tag.Value,
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                    Guid = Guid.NewGuid().ToString()
                };

                if (GetParam(form.Image.Name) is ParameterFile file)
                {
                    if (costcenter.Media == null)
                    {
                        costcenter.Media = new Media()
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
                        costcenter.Media.Name = file.Value;
                        costcenter.Media.Data = file.Data;
                        costcenter.Media.Updated = DateTime.Now;
                    }
                }

                ViewModel.Instance.CostCenters.Add(costcenter);
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
