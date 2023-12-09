using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using InventoryExpress.Parameter;
using InventoryExpress.WebControl;
using System;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebApp.WebPage;
using WebExpress.WebCore.Internationalization;
using WebExpress.WebCore.WebAttribute;
using WebExpress.WebCore.WebComponent;
using WebExpress.WebCore.WebResource;
using WebExpress.WebCore.WebScope;
using WebExpress.WebUI.WebControl;

namespace InventoryExpress.WebPage
{
    [Title("inventoryexpress:inventoryexpress.costcenter.edit.label")]
    [SegmentGuid<ParameterCostCenterId>("inventoryexpress:inventoryexpress.costcenter.edit.display")]
    [ContextPath("/")]
    [Parent<PageCostCenters>]
    [Module<Module>]
    public sealed class PageCostCenterEdit : PageWebApp, IPageCostCenter, IScope
    {
        /// <summary>
        /// Returns the form.
        /// </summary>
        private ControlFormularCostCenter Form { get; } = new ControlFormularCostCenter("costcenter")
        {
        };

        /// <summary>
        /// Returns or sets the cost center.
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
            // change and save cost center
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
                        Uri = ViewModel.GetManufacturerUri(CostCenter.Guid)
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

            var guid = context.Request.GetParameter<ParameterCostCenterId>()?.Value;
            CostCenter = ViewModel.GetCostCenter(guid);

            context.Uri.Display = CostCenter.Name;
            context.VisualTree.Content.Primary.Add(Form);
        }
    }
}
