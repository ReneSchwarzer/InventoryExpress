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
    [ID("CostCenterEdit")]
    [Title("inventoryexpress:inventoryexpress.costcenter.edit.label")]
    [SegmentGuid("CostCenterID", "inventoryexpress:inventoryexpress.costcenter.edit.display")]
    [Path("/CostCenter")]
    [Module("inventoryexpress")]
    [Context("general")]
    [Context("costcenteredit")]
    public sealed class PageCostCenterEdit : PageWebApp, IPageCostCenter
    {
        /// <summary>
        /// Liefert das Formular
        /// </summary>
        private ControlFormularCostCenter Form { get; } = new ControlFormularCostCenter("costcenter")
        {
            Edit = true
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageCostCenterEdit()
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
            lock (ViewModel.Instance.Database)
            {
                var guid = e.Context.Request.GetParameter("CostCenterID")?.Value;
                var costcenter = ViewModel.Instance.CostCenters.Where(x => x.Guid == guid).FirstOrDefault();

                // Kostenstelle ändern und speichern
                costcenter.Description = Form.Description.Value;
                costcenter.Updated = DateTime.Now;
                costcenter.Tag = Form.Tag.Value;

                ViewModel.Instance.SaveChanges();

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
                var guid = e.Context.Request.GetParameter("CostCenterID")?.Value;
                var costcenter = ViewModel.Instance.CostCenters.Where(x => x.Guid == guid).FirstOrDefault();

                if (e.Value.Count() < 1)
                {
                    e.Results.Add(new ValidationResult() { Text = "inventoryexpress:inventoryexpress.costcenter.validation.name.invalid", Type = TypesInputValidity.Error });
                }
                else if (!costcenter.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase) && ViewModel.Instance.Suppliers.Where(x => x.Name.Equals(e.Value)).Count() > 0)
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
            lock (ViewModel.Instance.Database)
            {
                var guid = e.Context.Request.GetParameter("CostCenterID")?.Value;
                var costcenter = ViewModel.Instance.CostCenters.Where(x => x.Guid == guid).FirstOrDefault();

                Form.CostCenterName.Value = costcenter?.Name;
                Form.Description.Value = costcenter?.Description;
                Form.Tag.Value = costcenter?.Tag;
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
                var guid = context.Request.GetParameter("CostCenterID")?.Value;
                var costcenter = ViewModel.Instance.CostCenters.Where(x => x.Guid == guid).FirstOrDefault();

                context.Request.Uri.Display = costcenter.Name;
                context.VisualTree.Content.Primary.Add(Form);
            }
        }
    }
}
