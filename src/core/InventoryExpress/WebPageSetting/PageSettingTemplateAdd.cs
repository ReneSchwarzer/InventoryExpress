using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using InventoryExpress.WebControl;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebAttribute;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.WebSettingPage;
using WebExpress.WebApp.Wql;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebPageSetting
{
    [WebExID("SettingTemplateAdd")]
    [WebExTitle("inventoryexpress:inventoryexpress.template.add.label")]
    [WebExSegment("add", "inventoryexpress:inventoryexpress.template.add.label")]
    [WebExContextPath("/")]
    [WebExParent("SettingTemplate")]
    [SettingHide()]
    [SettingContext("webexpress.webapp:setting.tab.general.label")]
    [WebExModule("inventoryexpress")]
    [WebExContext("general")]
    [WebExContext("templateadd")]
    public sealed class PageSettingTemplateAdd : PageWebAppSetting, IPageTemplate
    {
        /// <summary>
        /// Liefert das Formular
        /// </summary>
        private ControlFormularTemplate Form { get; } = new ControlFormularTemplate("template")
        {
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageSettingTemplateAdd()
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
            Form.RedirectUri = ResourceContext.ContextPath.Append("setting/templates");
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
            foreach (var v in ViewModel.GetAttributes(new WqlStatement()))
            {
                Form.Attributes.Options.Add(new ControlFormularItemInputSelectionItem()
                {
                    ID = v.Id,
                    Label = v.Name
                });
            }

            Form.Attributes.Value = string.Empty;
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Formular verarbeitet werden soll.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">Die Eventargumente/param>
        private void ProcessFormular(object sender, FormularEventArgs e)
        {
            // Neue Vorlage erstellen und speichern
            var template = new WebItemEntityTemplate()
            {
                Name = Form.TemplateName.Value,
                Description = Form.Description.Value,
                Tag = Form.Tag.Value
            };

            using (var transaction = ViewModel.BeginTransaction())
            {
                ViewModel.AddOrUpdateTemplate(template);

                transaction.Commit();
            }

            NotificationManager.CreateNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(Culture, "inventoryexpress:inventoryexpress.template.notification.add"),
                    new ControlLink()
                    {
                        Text = template.Name,
                        Uri = ViewModel.GetTemplateUri(template.Id)
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: template.Image,
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
