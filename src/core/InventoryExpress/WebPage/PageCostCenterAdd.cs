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
    [ID("CostCenterAdd")]
    [Title("inventoryexpress:inventoryexpress.costcenter.add.label")]
    [Segment("add", "inventoryexpress:inventoryexpress.costcenter.add.label")]
    [Path("/CostCenter")]
    [Module("inventoryexpress")]
    [Context("general")]
    public sealed class PageCostCenterAdd : PageWebApp, IPageCostCenter
    {
        /// <summary>
        /// Liefert das Formular
        /// </summary>
        private ControlFormularCostCenter Form { get; } = new ControlFormularCostCenter("costcenter")
        {
            Edit = false
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageCostCenterAdd()
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
            Form.CostCenterName.Validation += CostCenterNameValidation;
            Form.ProcessFormular += ProcessFormular;
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Formular verarbeitet werden soll.
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente/param>
        private void ProcessFormular(object sender, FormularEventArgs e)
        {
            // Neue Kostenstelle erstellen und speichern
            var costcenter = new CostCenter()
            {
                Name = Form.CostCenterName.Value,
                Description = Form.Description.Value,
                Tag = Form.Tag.Value,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Guid = Guid.NewGuid().ToString()
            };

            if (e.Context.Request.GetParameter(Form.Image.Name) is ParameterFile file)
            {
                if (costcenter.Media == null)
                {
                    costcenter.Media = new Media()
                    {
                        Name = file.Value,
                        Created = DateTime.Now,
                        Updated = DateTime.Now,
                        Guid = Guid.NewGuid().ToString()
                    };
                }
                else
                {
                    costcenter.Media.Name = file.Value;
                    costcenter.Media.Updated = DateTime.Now;
                }

                File.WriteAllBytes(Path.Combine(e.Context.Application.AssetPath, "media", costcenter.Media.Guid), file.Data);
            }

            lock (ViewModel.Instance.Database)
            {
                ViewModel.Instance.CostCenters.Add(costcenter);
                ViewModel.Instance.SaveChanges();
            }
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Feld CostCenterName validiert werden soll.
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente/param>
        private void CostCenterNameValidation(object sender, ValidationEventArgs e)
        {
            lock (ViewModel.Instance.Database)
            {
                if (e.Value.Length < 1)
                {
                    e.Results.Add(new ValidationResult() { Text = "inventoryexpress:inventoryexpress.costcenter.validation.name.invalid", Type = TypesInputValidity.Error });
                }
                else if (ViewModel.Instance.CostCenters.Where(x => x.Name.Equals(e.Value)).Any())
                {
                    e.Results.Add(new ValidationResult() { Text = "inventoryexpress:inventoryexpress.costcenter.validation.name.used", Type = TypesInputValidity.Error });
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
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            context.VisualTree.Content.Primary.Add(Form);
        }
    }
}
