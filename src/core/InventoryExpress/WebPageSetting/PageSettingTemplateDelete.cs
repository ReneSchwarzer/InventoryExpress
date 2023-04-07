using InventoryExpress.Model;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebUri;
using WebExpress.WebApp.WebAttribute;
using WebExpress.WebApp.WebControl;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.WebSettingPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebPageSetting
{
    [Id("SettingTemplateDelete")]
    [Title("inventoryexpress:inventoryexpress.template.delete.label")]
    [Segment("del")]
    [ContextPath("/Setting/SettingTemplate/SettingTemplateEdit")]
    [SettingHide()]
    [SettingContext("webexpress.webapp:setting.tab.general.label")]
    [Module("inventoryexpress")]
    [Context("general")]
    [Context("templatedelete")]
    public sealed class PageSettingTemplateDelete : PageWebAppSetting, IPageTemplate
    {
        /// <summary>
        /// Liefert das Formular
        /// </summary>
        private ControlFormularConfirmDelete Form { get; } = new ControlFormularConfirmDelete("template")
        {
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageSettingTemplateDelete()
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
            Form.Confirm += OnConfirmFormular;
            Form.RedirectUri = ContextPath.Append("setting/templates");
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular initialisiert wird
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void InitializeFormular(object sender, FormularEventArgs e)
        {
            var guid = e.Context.Request.GetParameter("TemplateID")?.Value;
            var template = ViewModel.GetTemplate(guid);

            Form.Content.Text = string.Format(InternationalizationManager.I18N(e.Context, "inventoryexpress:inventoryexpress.template.delete.description"), template?.Name);
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Formular bestätigt urde.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">Die Eventargumente/param>
        private void OnConfirmFormular(object sender, FormularEventArgs e)
        {
            var guid = e.Context.Request.GetParameter("TemplateID")?.Value;
            var template = ViewModel.GetTemplate(guid);

            using (var transaction = ViewModel.BeginTransaction())
            {
                ViewModel.DeleteTemplate(guid);

                transaction.Commit();
            }

            NotificationManager.CreateNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(Culture, "inventoryexpress:inventoryexpress.template.notification.delete"),
                    new ControlText()
                    {
                        Text = template.Name,
                        TextColor = new PropertyColorText(TypeColorText.Danger),
                        Format = TypeFormatText.Span
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: new UriRelative(template.Image),
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
