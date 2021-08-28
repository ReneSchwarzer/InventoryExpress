using InventoryExpress.Model;
using InventoryExpress.WebControl;
using System;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.Attribute;
using WebExpress.WebApp.WebResource.PageSetting;

namespace InventoryExpress.WebResource.PageSetting
{
    [ID("SettingTemplateEdit")]
    [Title("inventoryexpress.template.edit.label")]
    [SegmentGuid("TemplateID", "inventoryexpress.template.edit.display")]
    [Path("/SettingGeneral/SettingTemplate")]
    [SettingHide()]
    [SettingContext("inventoryexpress.setting.general.label")]
    [Module("InventoryExpress")]
    [Context("general")]
    [Context("templateedit")]
    public sealed class PageSettingTemplateEdit : PageTemplateWebAppSetting
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
        public override void Initialization()
        {
            base.Initialization();

            form = new ControlFormularTemplate()
            {
                RedirectUri = Uri.Take(-1),
                Edit = true,
                BackUri = Uri.Take(-1),
                EnableCancelButton = false
            };
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();

            lock (ViewModel.Instance.Database)
            {
                var guid = GetParamValue("TemplateID");
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

                Content.Primary.Add(form);

                form.TemplateName.Validation += (s, e) =>
                {
                    if (e.Value.Length < 1)
                    {
                        e.Results.Add(new ValidationResult() { Text = this.I18N("inventoryexpress.template.validation.name.invalid"), Type = TypesInputValidity.Error });
                    }
                    else if (!template.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase) && ViewModel.Instance.Templates.Where(x => x.Name.Equals(e.Value)).Any())
                    {
                        e.Results.Add(new ValidationResult() { Text = this.I18N("inventoryexpress.template.validation.name.used"), Type = TypesInputValidity.Error });
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
