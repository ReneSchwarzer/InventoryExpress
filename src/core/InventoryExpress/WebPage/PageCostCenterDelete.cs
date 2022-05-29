using InventoryExpress.Model;
using System.IO;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebApp.WebControl;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;
using static WebExpress.Internationalization.InternationalizationManager;

namespace InventoryExpress.WebPage
{
    [ID("CostCenterDelete")]
    [Title("inventoryexpress:inventoryexpress.costcenter.delete.label")]
    [Segment("del", "inventoryexpress:inventoryexpress.costcenter.delete.display")]
    [Path("/CostCenter/CostCenterEdit")]
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
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            Form.RedirectUri = context.ContextPath.Append("costcenters");
            Form.InitializeFormular += OnInitializeFormular;
            Form.Confirm += OnConfirmFormular;
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular initialisiert wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente</param>
        private void OnInitializeFormular(object sender, FormularEventArgs e)
        {
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Formular bestätigt urde.
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente/param>
        private void OnConfirmFormular(object sender, FormularEventArgs e)
        {
            var guid = e.Context.Request.GetParameter("CostCenterID")?.Value;
            var costcenter = ViewModel.GetCostCenter(guid);

            using var transaction = ViewModel.BeginTransaction();

            ViewModel.DeleteCostCenter(guid);

            NotificationManager.CreateNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    I18N(Culture, "inventoryexpress:inventoryexpress.costcenter.notification.delete"),
                    new ControlLink()
                    {
                        Text = costcenter.Name,
                        Uri = new UriRelative(ViewModel.GetCostCenterUri(costcenter.ID))
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: new UriRelative(costcenter.Image),
                durability: 10000
            );

            transaction.Commit();
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
