﻿using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using InventoryExpress.WebControl;
using System;
using System.Linq;
using WebExpress.Internationalization;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.WebSettingPage;
using WebExpress.WebApp.Wql;
using WebExpress.WebAttribute;
using WebExpress.WebComponent;
using WebExpress.WebResource;

namespace InventoryExpress.WebPageSetting
{
    [WebExTitle("inventoryexpress:inventoryexpress.template.edit.label")]
    [WebExSegmentGuid("TemplateId", "inventoryexpress:inventoryexpress.template.edit.display")]
    [WebExContextPath("/")]
    [WebExParent(typeof(PageSettingTemplates))]
    [WebExSettingHide()]
    [WebExSettingContext("webexpress.webapp:setting.tab.general.label")]
    [WebExModule<Module>]
    [WebExContext("general")]
    [WebExContext("templateedit")]
    public sealed class PageSettingTemplateEdit : PageWebAppSetting, IPageTemplate
    {
        /// <summary>
        /// Returns the form
        /// </summary>
        private ControlFormularTemplate Form { get; } = new ControlFormularTemplate("template")
        {
        };

        /// <summary>
        /// Liefert oder setzt die Vorlage
        /// </summary>
        private WebItemEntityTemplate Template { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public PageSettingTemplateEdit()
        {
        }

        /// <summary>
        /// Initialization
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
        /// Invoked when the form is initialized.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void InitializeFormular(object sender, FormularEventArgs e)
        {
            Form.RedirectUri = e.Context.Uri.Take(-1);
            Form.Attributes.Options.Clear();

            foreach (var v in ViewModel.GetAttributes(new WqlStatement()))
            {
                Form.Attributes.Options.Add(new ControlFormItemInputSelectionItem()
                {
                    Id = v.Id,
                    Label = v.Name
                });
            }
        }

        /// <summary>
        /// Invoked when the form is about to be populated.
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
        /// Fired when the form is about to be processed.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument./param>
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

            ComponentManager.GetComponent<NotificationManager>()?.AddNotification
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

            var guid = context.Request.GetParameter("TemplateId")?.Value;
            Template = ViewModel.GetTemplate(guid);

            Uri.Display = Template.Name;
            context.VisualTree.Content.Primary.Add(Form);
        }
    }
}
