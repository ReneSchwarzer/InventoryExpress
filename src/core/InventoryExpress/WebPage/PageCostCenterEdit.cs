using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using InventoryExpress.WebControl;
using System;
using System.IO;
using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebApp.WebNotificaation;
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
        /// Liefert oder setzt die Kostenstelle
        /// </summary>
        private WebItemEntityCostCenter CostCenter { get; set; }

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
            Form.ProcessFormular += ProcessFormular;
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular initialisiert wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente</param>
        private void InitializeFormular(object sender, FormularEventArgs e)
        {
            Form.RedirectUri = e.Context.Uri.Take(-1);
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular gefüllt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente</param>
        private void FillFormular(object sender, FormularEventArgs e)
        {
            Form.CostCenterName.Value = CostCenter?.Name;
            Form.Description.Value = CostCenter?.Description;
            Form.Tag.Value = CostCenter?.Tag;
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Formular verarbeitet werden soll.
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente/param>
        private void ProcessFormular(object sender, FormularEventArgs e)
        {
            var file = e.Context.Request.GetParameter(Form.Image.Name) as ParameterFile;

            // Herstellerobjekt ändern und speichern
            CostCenter.Name = Form.CostCenterName.Value;
            CostCenter.Description = Form.Description.Value;
            CostCenter.Tag = Form.Tag.Value;
            CostCenter.Updated = DateTime.Now;
            CostCenter.Media.Name = file?.Value;

            ViewModel.AddOrUpdateCostCenter(CostCenter);
            ViewModel.Instance.SaveChanges();

            if (file != null)
            {
                ViewModel.AddOrUpdateMedia(CostCenter.Media, file?.Data);
                ViewModel.Instance.SaveChanges();
            }

            NotificationManager.CreateNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(Culture, "inventoryexpress:inventoryexpress.costcenter.notification.edit"),
                    new ControlLink()
                    {
                        Text = CostCenter.Name,
                        Uri = new UriRelative(ViewModel.GetManufacturerUri(CostCenter.ID))
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: new UriRelative(CostCenter.Image),
                durability: 10000
            );
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var guid = context.Request.GetParameter("CostCenterID")?.Value;
            CostCenter = ViewModel.GetCostCenter(guid);

            context.Request.Uri.Display = CostCenter.Name;
            context.VisualTree.Content.Primary.Add(Form);
        }
    }
}
