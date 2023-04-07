﻿using InventoryExpress.Model;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebUri;
using WebExpress.WebApp.WebControl;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [Id("CostCenterDelete")]
    [Title("inventoryexpress:inventoryexpress.costcenter.delete.label")]
    [Segment("del", "inventoryexpress:inventoryexpress.costcenter.delete.display")]
    [ContextPath("/CostCenter/CostCenterEdit")]
    [Module("inventoryexpress")]
    [Context("general")]
    [Context("costcenterdelete")]
    public sealed class PageCostCenterDelete : PageWebApp, IPageCostCenter
    {
        /// <summary>
        /// Liefert das Formular
        /// </summary>
        private ControlFormularConfirmDelete Form { get; } = new ControlFormularConfirmDelete("costcenter")
        {
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageCostCenterDelete()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            Form.RedirectUri = ContextPath.Append("costcenters");
            Form.InitializeFormular += OnInitializeFormular;
            Form.Confirm += OnConfirmFormular;
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular initialisiert wird
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void OnInitializeFormular(object sender, FormularEventArgs e)
        {
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Formular bestätigt urde.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">Die Eventargumente/param>
        private void OnConfirmFormular(object sender, FormularEventArgs e)
        {
            var guid = e.Context.Request.GetParameter("CostCenterID")?.Value;
            var costcenter = ViewModel.GetCostCenter(guid);

            using (var transaction = ViewModel.BeginTransaction())
            {
                ViewModel.DeleteCostCenter(guid);

                transaction.Commit();
            }

            NotificationManager.CreateNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(Culture, "inventoryexpress:inventoryexpress.costcenter.notification.delete"),
                    new ControlText()
                    {
                        Text = costcenter.Name,
                        TextColor = new PropertyColorText(TypeColorText.Danger),
                        Format = TypeFormatText.Span
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: new UriRelative(costcenter.Image),
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

            context.VisualTree.Content.Primary.Add(Form);
        }
    }
}
