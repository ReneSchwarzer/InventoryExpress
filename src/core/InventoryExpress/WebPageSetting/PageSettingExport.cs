using System.Threading;
using WebExpress.Attribute;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.Attribute;
using WebExpress.WebApp.WebApiControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.WebSettingPage;
using WebExpress.WebResource;
using WebExpress.WebTask;

namespace InventoryExpress.WebPageSetting
{
    [ID("SettingExport")]
    [Title("inventoryexpress:inventoryexpress.export.label")]
    [Segment("export", "inventoryexpress:inventoryexpress.export.label")]
    [Path("/Setting")]
    [SettingSection(SettingSection.Primary)]
    [SettingIcon(TypeIcon.Download)]
    [SettingGroup("inventoryexpress:inventoryexpress.setting.data.label")]
    [SettingContext("webexpress.webapp:setting.tab.general.label")]
    [Module("inventoryexpress")]
    [Context("admin")]
    public sealed class PageSettingExport : PageWebAppSetting
    {
        /// <summary>
        /// Hilfetext der Seite
        /// </summary>
        private ControlText Help { get; } = new ControlText()
        {
            Text = "inventoryexpress:inventoryexpress.export.help.label",
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two)
        };

        /// <summary>
        /// Das Uploadformular
        /// </summary>
        private ControlFormularInline Form { get; } = new ControlFormularInline("export")
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two)
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageSettingExport()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            Form.ProcessFormular += OnProcessFormular;
            Form.SubmitButton.Text = "inventoryexpress:inventoryexpress.export.label";
            Form.SubmitButton.Icon = new PropertyIcon(TypeIcon.Download);
            Form.SubmitButton.Color = new PropertyColorButton(TypeColorButton.Primary);

        }

        /// <summary>
        /// Formular wird verarbeitet
        /// </summary>
        /// <param name="sender">Der Sender</param>
        /// <param name="e">Die Eventargumente</param>
        private void OnProcessFormular(object sender, FormularEventArgs e)
        {
            var id = $"inventoryexpress_export_{e.Context.Request.Session.ID}";

            if (!TaskManager.ContainsTask(id))
            {
                var task = TaskManager.CreateTask(id, OnTaskProcess, e.Context);
                task.Process += OnTaskProcess;



                task.Run();
            }
        }

        /// <summary>
        /// Ausführung des Export-Task
        /// </summary>
        /// <param name="sender">Der Sender</param>
        /// <param name="e">Die Eventargumente</param>
        private void OnTaskProcess(object sender, System.EventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                (sender as Task).Progress = i;
                Thread.Sleep(10000);
            }
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var id = $"inventoryexpress_export_{context.Request.Session.ID}";
            var taskState = new ControlApiProgressBarTaskState(id)
            {
                Margin = new PropertySpacingMargin(PropertySpacing.Space.Two),
                Color = new PropertyColorProgress(TypeColorProgress.Primary),
                Format = TypeFormatProgress.Animated
            };

            context.VisualTree.Content.Preferences.Add(Help);
            context.VisualTree.Content.Primary.Add(Form);
            context.VisualTree.Content.Primary.Add(taskState);



        }
    }
}
