using InventoryExpress.Model;
using InventoryExpress.Model.Entity;
using InventoryExpress.WebControl;
using System;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.Attribute;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.WebSettingPage;
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
    public sealed class PageSettingTemplateEdit : PageWebAppSetting
    {
        /// <summary>
        /// Formular
        /// </summary>
        private ControlFormularTemplate form;

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

            form = new ControlFormularTemplate()
            {
                Edit = true,
                EnableCancelButton = false
            };
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var visualTree = context.VisualTree;

            form.RedirectUri = context.Uri.Take(-1);
            form.BackUri = context.Uri.Take(-1);

            lock (ViewModel.Instance.Database)
            {
                var guid = context.Request.GetParameter("TemplateID")?.Value;
                var template = ViewModel.Instance.Templates.Where(x => x.Guid == guid).FirstOrDefault();
                var orignalAttributes = ViewModel.Instance.TemplateAttributes
                    .Where(x => x.TemplateId == template.Id)
                    .Join(ViewModel.Instance.Attributes, t => t.AttributeId, a => a.Id, (t, a) => a.Guid).ToList();

                foreach (var v in ViewModel.Instance.Attributes)
                {
                    form.Attributes.Options.Add(new ControlFormularItemInputMoveSelectorItem()
                    {
                        ID = v.Guid,
                        Value = v.Name
                    });
                }

                form.TemplateName.Value = template?.Name;
                form.Attributes.Value = string.Join(";", orignalAttributes);
                form.Description.Value = template?.Description;
                form.Tag.Value = template?.Tag;

                visualTree.Content.Primary.Add(form);

                form.TemplateName.Validation += (s, e) =>
                {
                    if (e.Value.Length < 1)
                    {
                        e.Results.Add(new ValidationResult() { Text = this.I18N("inventoryexpress:inventoryexpress.template.validation.name.invalid"), Type = TypesInputValidity.Error });
                    }
                    else if (!template.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase) && ViewModel.Instance.Templates.Where(x => x.Name.Equals(e.Value)).Any())
                    {
                        e.Results.Add(new ValidationResult() { Text = this.I18N("inventoryexpress:inventoryexpress.template.validation.name.used"), Type = TypesInputValidity.Error });
                    }
                };

                form.ProcessFormular += (s, e) =>
                {
                    // Vorlage ändern und speichern
                    template.Name = form.TemplateName.Value;
                    template.Description = form.Description.Value;
                    template.Tag = form.Tag.Value;
                    template.Updated = DateTime.Now;

                    ViewModel.Instance.SaveChanges();

                    var changeAttributes = form.Attributes.Value?.Split(";", StringSplitOptions.RemoveEmptyEntries);

                    // lösche nicht mehr verwendete Attribute
                    foreach (var removeItem in orignalAttributes.Except(changeAttributes).Join(ViewModel.Instance.Attributes, x => x, y => y.Guid, (x, y) => y))
                    {
                        var attr = ViewModel.Instance.TemplateAttributes.Where(x => x.TemplateId == template.Id && x.AttributeId == removeItem.Id).FirstOrDefault();
                        ViewModel.Instance.TemplateAttributes.Remove(attr);
                    }

                    // verknüpfe Attribute
                    foreach (var newItems in changeAttributes.Except(orignalAttributes).Join(ViewModel.Instance.Attributes, x => x, y => y.Guid, (x, y) => y))
                    {
                        ViewModel.Instance.TemplateAttributes.Add(new TemplateAttribute() { TemplateId = template.Id, AttributeId = newItems.Id });
                    }

                    ViewModel.Instance.SaveChanges();

                    form.Reset();
                };
            }
        }
    }
}
