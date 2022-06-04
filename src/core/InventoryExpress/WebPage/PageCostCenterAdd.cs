using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using InventoryExpress.WebControl;
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
            Form.ProcessFormular += ProcessFormular;
            Form.RedirectUri = context.Application.ContextPath.Append("costcenters");
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular initialisiert wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente</param>
        private void InitializeFormular(object sender, FormularEventArgs e)
        {
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
        /// Wird ausgelöst, wenn das Formular verarbeitet werden soll.
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente/param>
        private void ProcessFormular(object sender, FormularEventArgs e)
        {
            var file = e.Context.Request.GetParameter(Form.Image.Name) as ParameterFile;
            using var transaction = ViewModel.BeginTransaction();

            // Neue Kostenstelle erstellen und speichern
            var costcenter = new WebItemEntityCostCenter()
            {
                Name = Form.CostCenterName.Value,
                Description = Form.Description.Value,
                Tag = Form.Tag.Value,
                Media = new WebItemEntityMedia()
                {
                    Name = file?.Value
                }
            };

            ViewModel.AddOrUpdateCostCenter(costcenter);

            if (file != null)
            {
                ViewModel.AddOrUpdateMedia(costcenter.Media, file);
            }

            transaction.Commit();

            NotificationManager.CreateNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(Culture, "inventoryexpress:inventoryexpress.costcenter.notification.add"),
                    new ControlLink()
                    {
                        Text = costcenter.Name,
                        Uri = new UriRelative(ViewModel.GetCostCenterUri(costcenter.Id))
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: new UriRelative(costcenter.Image),
                durability: 10000
            );

            Form.RedirectUri = Form.RedirectUri.Append(costcenter.Id);
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
