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
    [ID("ManufacturerDelete")]
    [Title("inventoryexpress:inventoryexpress.manufacturer.delete.label")]
    [Segment("del", "inventoryexpress:inventoryexpress.manufacturer.delete.display")]
    [Path("/Manufacturer/ManufacturerEdit")]
    [Module("inventoryexpress")]
    [Context("general")]
    [Context("manufacturerdelete")]
    public sealed class PageManufacturerDelete : PageWebApp, IPageManufacturer
    {
        /// <summary>
        /// Liefert das Formular
        /// </summary>
        private ControlFormularConfirmDelete Form { get; } = new ControlFormularConfirmDelete("manufacturer")
        {
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageManufacturerDelete()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            Form.RedirectUri = context.ContextPath.Append("manufacturers");
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
            var guid = e.Context.Request.GetParameter("ManufacturerID")?.Value;
            var manufacturer = ViewModel.GetManufacturer(guid);

            using var transaction = ViewModel.BeginTransaction();

            ViewModel.DeleteManufacturer(guid);

            NotificationManager.CreateNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    I18N(Culture, "inventoryexpress:inventoryexpress.manufacturer.notification.delete"),
                    new ControlLink()
                    {
                        Text = manufacturer.Name,
                        Uri = new UriRelative(ViewModel.GetManufacturerUri(manufacturer.ID))
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: new UriRelative(manufacturer.Image),
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
