using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using InventoryExpress.WebControl;
using System;
using System.IO;
using System.Linq;
using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebApp.WebAttribute;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.WebSettingPage;
using WebExpress.WebApp.Wql;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebPageSetting
{
    [ID("SettingTemplateEdit")]
    [Title("inventoryexpress:inventoryexpress.template.edit.label")]
    [SegmentGuid("TemplateID", "inventoryexpress:inventoryexpress.template.edit.display")]
    [Path("/Setting/SettingTemplate")]
    [SettingHide()]
    [SettingContext("webexpress.webapp:setting.tab.general.label")]
    [Module("inventoryexpress")]
    [Context("general")]
    [Context("templateedit")]
    public sealed class PageSettingTemplateEdit : PageWebAppSetting, IPageTemplate
    {
        /// <summary>
        /// Liefert das Formular
        /// </summary>
        private ControlFormularTemplate Form { get; } = new ControlFormularTemplate("template")
        {
            Edit = true
        };

        /// <summary>
        /// Liefert oder setzt die Vorlage
        /// </summary>
        private WebItemEntityTemplate Template { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageSettingTemplateEdit()
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
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular initialisiert wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente</param>
        private void InitializeFormular(object sender, FormularEventArgs e)
        {
            Form.RedirectUri = e.Context.Uri.Take(-1);
            Form.Attributes.Options.Clear();

            foreach (var v in ViewModel.GetAttributes(new WqlStatement()))
            {
                Form.Attributes.Options.Add(new ControlFormularItemInputSelectionItem()
                {
                    ID = v.Id,
                    Label = v.Name
                });
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular gefüllt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente</param>
        private void FillFormular(object sender, FormularEventArgs e)
        {
            Form.TemplateName.Value = Template?.Name;
            Form.Attributes.Value = string.Join(";", Template.Attributes);
            Form.Description.Value = Template?.Description;
            Form.Tag.Value = Template?.Tag;
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Formular verarbeitet werden soll.
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente/param>
        private void ProcessFormular(object sender, FormularEventArgs e)
        {
            var file = e.Context.Request.GetParameter(Form.Image.Name) as ParameterFile;
            var attributes = Form.Attributes.Value?.Split(";", StringSplitOptions.RemoveEmptyEntries);
            using var transaction = ViewModel.BeginTransaction();

            // Vorlage ändern und speichern
            Template.Name = Form.TemplateName.Value;
            Template.Description = Form.Description.Value;
            Template.Attributes = ViewModel.GetAttributes(new WqlStatement()).Where(x => attributes.Contains(x.Id));
            Template.Tag = Form.Tag.Value;
            Template.Updated = DateTime.Now;
            Template.Media.Name = file?.Value;

            ViewModel.AddOrUpdateTemplate(Template);

            if (file != null)
            {
                ViewModel.AddOrUpdateMedia(Template.Media, file);
            }

            transaction.Commit();

            NotificationManager.CreateNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(Culture, "inventoryexpress:inventoryexpress.template.notification.edit"),
                    new ControlLink()
                    {
                        Text = Template.Name,
                        Uri = new UriRelative(ViewModel.GetTemplateUri(Template.Id))
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: new UriRelative(Template.Image),
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

            var guid = context.Request.GetParameter("TemplateID")?.Value;
            Template = ViewModel.GetTemplate(guid);

            context.Request.Uri.Display = Template.Name;
            context.VisualTree.Content.Primary.Add(Form);
        }
    }
}
