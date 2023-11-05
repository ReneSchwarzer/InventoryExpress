using InventoryExpress.Model;
using WebExpress.WebUI.WebAttribute;
using WebExpress.WebUI.WebControl;
using WebExpress.WebApp.WebApiControl;
using WebExpress.WebApp.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.WebScope;
using WebExpress.WebApp.WebSettingPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebPageSetting
{
    [Title("inventoryexpress:inventoryexpress.importexport.label")]
    [Segment("export", "inventoryexpress:inventoryexpress.importexport.label")]
    [ContextPath("/setting")]
    [SettingSection(SettingSection.Primary)]
    [SettingIcon(TypeIcon.FileExport)]
    [SettingGroup("inventoryexpress:inventoryexpress.setting.data.label")]
    [SettingContext("webexpress.webapp:setting.tab.general.label")]
    [Module<Module>]
    [Scope<ScopeAdmin>]
    public sealed class PageSettingExport : PageWebAppSetting
    {
        /// <summary>
        /// Hilfetext der Seite
        /// </summary>
        private ControlText ExportHelp { get; } = new ControlText()
        {
            Text = "inventoryexpress:inventoryexpress.export.help.label",
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two)
        };

        /// <summary>
        /// Hilfetext der Seite
        /// </summary>
        private ControlText ImportHelp { get; } = new ControlText()
        {
            Text = "inventoryexpress:inventoryexpress.import.help.label",
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two)
        };

        /// <summary>
        /// Das Exportformular
        /// </summary>
        private ControlFormInline ExportForm { get; } = new ControlFormInline("export")
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two)
        };

        /// <summary>
        /// Die Importschaltfläche
        /// </summary>
        private ControlButton ImportButton { get; } = new ControlButton()
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two),
            Icon = new PropertyIcon(TypeIcon.Upload),
            Text = "inventoryexpress:inventoryexpress.import.label",
            BackgroundColor = new PropertyColorButton(TypeColorButton.Primary),
            OnClick = new PropertyOnClick($"$('#modal_import').modal('show');")
        };

        /// <summary>
        /// Das Importformular
        /// </summary>
        private ControlModalFormularFileUpload ImportForm { get; } = new ControlModalFormularFileUpload("import")
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two),
            AcceptFile = new[] { ".zip" },
            Header = "inventoryexpress:inventoryexpress.import.header"
        };

        /// <summary>
        /// Constructor
        /// </summary>
        public PageSettingExport()
        {
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            ExportForm.ProcessFormular += OnProcessFormular;
            ExportForm.SubmitButton.Text = "inventoryexpress:inventoryexpress.export.label";
            ExportForm.SubmitButton.Icon = new PropertyIcon(TypeIcon.Download);
            ExportForm.SubmitButton.Color = new PropertyColorButton(TypeColorButton.Primary);

            ImportForm.Upload += ImportFormUpload;
        }

        /// <summary>
        /// Wird aufgerufen, wenn eine Importdatei hochgeladen wurde.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void ImportFormUpload(object sender, FormularUploadEventArgs e)
        {

        }

        /// <summary>
        /// Formular wird verarbeitet
        /// </summary>
        /// <param name="sender">Der Sender</param>
        /// <param name="e">The event argument.</param>
        private void OnProcessFormular(object sender, FormularEventArgs e)
        {
            ViewModel.CreateExportTask(e.Context);
        }

        /// <summary>
        /// Processing of the resource.
        /// </summary>
        /// <param name="context">The context for rendering the page.</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var id = $"inventoryexpress_export_{context.Request.Session.Id}";
            var taskState = new ControlApiProgressBarTaskState(id)
            {
                Margin = new PropertySpacingMargin(PropertySpacing.Space.Two),
                Color = new PropertyColorProgress(TypeColorProgress.Primary),
                Format = TypeFormatProgress.Animated
            };

            context.VisualTree.Content.Preferences.Add(ExportHelp);
            context.VisualTree.Content.Primary.Add(ExportForm);
            context.VisualTree.Content.Primary.Add(ImportHelp);
            context.VisualTree.Content.Primary.Add(ImportButton);
            context.VisualTree.Content.Primary.Add(taskState);

            context.VisualTree.Content.Secondary.Add(ImportForm);
        }
    }
}
