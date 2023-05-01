﻿using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using InventoryExpress.WebControl;
using System;
using System.Linq;
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
    [WebExID("SettingTemplateEdit")]
    [WebExTitle("inventoryexpress:inventoryexpress.template.edit.label")]
    [WebExSegmentGuid("TemplateID", "inventoryexpress:inventoryexpress.template.edit.display")]
    [WebExContextPath("/Setting/SettingTemplate")]
    [SettingHide()]
    [SettingContext("webexpress.webapp:setting.tab.general.label")]
    [WebExModule("inventoryexpress")]
    [WebExContext("general")]
    [WebExContext("templateedit")]
    public sealed class PageSettingTemplateEdit : PageWebAppSetting, IPageTemplate
    {
        /// <summary>
        /// Liefert das Formular
        /// </summary>
        private ControlFormularTemplate Form { get; } = new ControlFormularTemplate("template")
        {
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
        /// <param name="context">The context.</param>
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
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
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
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
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
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">Die Eventargumente/param>
        private void ProcessFormular(object sender, FormularEventArgs e)
        {
            var attributes = Form.Attributes.Value?.Split(";", StringSplitOptions.RemoveEmptyEntries);

            // Vorlage ändern und speichern
            Template.Name = Form.TemplateName.Value;
            Template.Description = Form.Description.Value;
            Template.Attributes = ViewModel.GetAttributes(new WqlStatement()).Where(x => attributes.Contains(x.Id));
            Template.Tag = Form.Tag.Value;
            Template.Updated = DateTime.Now;

            using (var transaction = ViewModel.BeginTransaction())
            {
                ViewModel.AddOrUpdateTemplate(Template);

                transaction.Commit();
            }

            NotificationManager.CreateNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(Culture, "inventoryexpress:inventoryexpress.template.notification.edit"),
                    new ControlLink()
                    {
                        Text = Template.Name,
                        Uri = ViewModel.GetTemplateUri(Template.Id)
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: Template.Image,
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

            var guid = context.Request.GetParameter("TemplateID")?.Value;
            Template = ViewModel.GetTemplate(guid);

            Uri.Display = Template.Name;
            context.VisualTree.Content.Primary.Add(Form);
        }
    }
}
