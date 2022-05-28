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
    [ID("LocationDelete")]
    [Title("inventoryexpress:inventoryexpress.location.delete.label")]
    [Segment("del", "inventoryexpress:inventoryexpress.location.delete.display")]
    [Path("/Location/LocationEdit")]
    [Module("inventoryexpress")]
    [Context("general")]
    [Context("locationdelete")]
    public sealed class PageLocationDelete : PageWebApp, IPageLocation
    {
        /// <summary>
        /// Liefert das Formular
        /// </summary>
        private ControlFormularConfirmDelete Form { get; } = new ControlFormularConfirmDelete("location")
        {
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageLocationDelete()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            Form.RedirectUri = context.ContextPath.Append("locations");
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
            var guid = e.Context.Request.GetParameter("LocationID")?.Value;
            var location = ViewModel.GetLocation(guid);

            ViewModel.DeleteLocation(guid);
            ViewModel.Instance.SaveChanges();

            NotificationManager.CreateNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    I18N(Culture, "inventoryexpress:inventoryexpress.location.notification.delete"),
                    new ControlLink()
                    {
                        Text = location.Name,
                        Uri = new UriRelative(ViewModel.GetLocationUri(location.ID))
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: new UriRelative(location.Image),
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

            context.VisualTree.Content.Primary.Add(Form);
        }
    }
}
