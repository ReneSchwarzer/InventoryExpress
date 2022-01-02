using InventoryExpress.Model;
using System;
using System.IO;
using System.Linq;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebApiControl;
using WebExpress.WebApp.WebAttribute;
using WebExpress.WebApp.WebControl;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.WebSettingPage;
using WebExpress.WebAttribute;
using WebExpress.WebPage;
using WebExpress.WebResource;
using WebExpress.WebTask;

namespace InventoryExpress.WebPageSetting
{
    [ID("SettingExport")]
    [Title("inventoryexpress:inventoryexpress.importexport.label")]
    [Segment("export", "inventoryexpress:inventoryexpress.importexport.label")]
    [Path("/Setting")]
    [SettingSection(SettingSection.Primary)]
    [SettingIcon(TypeIcon.FileExport)]
    [SettingGroup("inventoryexpress:inventoryexpress.setting.data.label")]
    [SettingContext("webexpress.webapp:setting.tab.general.label")]
    [Module("inventoryexpress")]
    [Context("admin")]
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
        private ControlFormularInline ExportForm { get; } = new ControlFormularInline("export")
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
            OnClick = $"$('#modal_import').modal('show');"
        };

        /// <summary>
        /// Das Importformular
        /// </summary>
        private ControlModalFormFileUpload ImportForm { get; } = new ControlModalFormFileUpload("import")
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two),
            AcceptFile = new[] { ".zip" },
            Header = "inventoryexpress:inventoryexpress.import.header"
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

            ExportForm.ProcessFormular += OnProcessFormular;
            ExportForm.SubmitButton.Text = "inventoryexpress:inventoryexpress.export.label";
            ExportForm.SubmitButton.Icon = new PropertyIcon(TypeIcon.Download);
            ExportForm.SubmitButton.Color = new PropertyColorButton(TypeColorButton.Primary);

            ImportForm.Upload += ImportFormUpload;
        }

        /// <summary>
        /// Wird aufgerufen, wenn eine Importdatei hochgeladen wurde.
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente</param>
        private void ImportFormUpload(object sender, FormularUploadEventArgs e)
        {

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
                task.Finish += OnTaskFinish;

                task.Run();
            }
        }

        /// <summary>
        /// Ausführung des Export-Task
        /// </summary>
        /// <param name="sender">Der Sender</param>
        /// <param name="e">Die Eventargumente</param>
        private void OnTaskProcess(object sender, EventArgs e)
        {
            var file = Path.Combine(Path.GetTempPath(), $"{ Guid.NewGuid() }.zip");

            ViewModel.Instance.Export(file, Context.Application.AssetPath, i => { (sender as Task).Progress = i; });
        }

        /// <summary>
        /// Der Export-Task ist beendet
        /// </summary>
        /// <param name="sender">Der Sender</param>
        /// <param name="e">Die Eventargumente</param>
        private void OnTaskFinish(object sender, TaskEventArgs e)
        {
            var context = (sender as Task)?.Arguments?.Where(x => x is RenderContext).FirstOrDefault() as RenderContext;

            var notification = NotificationManager.CreateNotification(context?.Request, "Export", 100000);
            //notification.
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

            context.VisualTree.Content.Preferences.Add(ExportHelp);
            context.VisualTree.Content.Primary.Add(ExportForm);
            context.VisualTree.Content.Primary.Add(ImportHelp);
            context.VisualTree.Content.Primary.Add(ImportButton);
            context.VisualTree.Content.Primary.Add(taskState);

            context.VisualTree.Content.Secondary.Add(ImportForm);
        }
    }
}
