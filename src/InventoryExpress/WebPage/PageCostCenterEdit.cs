using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using InventoryExpress.Parameters;
using InventoryExpress.WebControl;
using System;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebComponent;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [WebExTitle("inventoryexpress:inventoryexpress.costcenter.edit.label")]
    [WebExSegmentGuid(typeof(ParameterCostCenterId), "inventoryexpress:inventoryexpress.costcenter.edit.display")]
    [WebExContextPath("/")]
    [WebExParent(typeof(PageCostCenters))]
    [WebExModule<Module>]
    [WebExContext("general")]
    [WebExContext("costcenteredit")]
    public sealed class PageCostCenterEdit : PageWebApp, IPageCostCenter
    {
        /// <summary>
        /// Returns the form
        /// </summary>
        private ControlFormularCostCenter Form { get; } = new ControlFormularCostCenter("costcenter")
        {
        };

        /// <summary>
        /// Liefert oder setzt die Kostenstelle
        /// </summary>
        private WebItemEntityCostCenter CostCenter { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public PageCostCenterEdit()
        {
        }

        /// <summary>
        /// Initialization
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
        /// Invoked when the form is initialized.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void InitializeFormular(object sender, FormularEventArgs e)
        {
            Form.RedirectUri = e.Context.Uri.Take(-1);
        }

        /// <summary>
        /// Invoked when the form is about to be populated.
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
        /// Fired when the form is about to be processed.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument./param>
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

            ComponentManager.GetComponent<NotificationManager>()?.AddNotification
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

            var guid = context.Request.GetParameter("CostCenterId")?.Value;
            CostCenter = ViewModel.GetCostCenter(guid);

            Uri.Display = CostCenter.Name;
            context.VisualTree.Content.Primary.Add(Form);
        }
    }
}
