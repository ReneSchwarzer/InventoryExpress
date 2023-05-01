using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using InventoryExpress.WebControl;
using System;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [WebExID("CostCenterEdit")]
    [WebExTitle("inventoryexpress:inventoryexpress.costcenter.edit.label")]
    [WebExSegmentGuid("CostCenterID", "inventoryexpress:inventoryexpress.costcenter.edit.display")]
    [WebExContextPath("/")]
    [WebExParent("CostCenter")]
    [WebExModule("inventoryexpress")]
    [WebExContext("general")]
    [WebExContext("costcenteredit")]
    public sealed class PageCostCenterEdit : PageWebApp, IPageCostCenter
    {
        /// <summary>
        /// Liefert das Formular
        /// </summary>
        private ControlFormularCostCenter Form { get; } = new ControlFormularCostCenter("costcenter")
        {
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
        /// <param name="context">The context.</param>
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
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void InitializeFormular(object sender, FormularEventArgs e)
        {
            Form.RedirectUri = e.Context.Uri.Take(-1);
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular gefüllt werden soll
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void FillFormular(object sender, FormularEventArgs e)
        {
            Form.CostCenterName.Value = CostCenter?.Name;
            Form.Description.Value = CostCenter?.Description;
            Form.Tag.Value = CostCenter?.Tag;
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Formular verarbeitet werden soll.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">Die Eventargumente/param>
        private void ProcessFormular(object sender, FormularEventArgs e)
        {
            // Herstellerobjekt ändern und speichern
            CostCenter.Name = Form.CostCenterName.Value;
            CostCenter.Description = Form.Description.Value;
            CostCenter.Tag = Form.Tag.Value;
            CostCenter.Updated = DateTime.Now;

            using (var transaction = ViewModel.BeginTransaction())
            {
                ViewModel.AddOrUpdateCostCenter(CostCenter);

                transaction.Commit();
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
                        Uri = ViewModel.GetManufacturerUri(CostCenter.Id)
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: CostCenter.Image,
                durability: 10000
            );
        }

        /// <summary>
        /// Processing of the resource.
        /// </summary>
        /// <param name="context">The context for rendering the page.</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var guid = context.Request.GetParameter("CostCenterID")?.Value;
            CostCenter = ViewModel.GetCostCenter(guid);

            Uri.Display = CostCenter.Name;
            context.VisualTree.Content.Primary.Add(Form);
        }
    }
}
