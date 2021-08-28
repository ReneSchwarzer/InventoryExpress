using InventoryExpress.Model;
using InventoryExpress.WebControl;
using System;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.Attribute;
using WebExpress.WebApp.WebResource.PageSetting;

namespace InventoryExpress.WebResource.PageSetting
{
    [ID("SettingTemplateAdd")]
    [Title("inventoryexpress.template.add.label")]
    [Segment("add", "inventoryexpress.template.add.label")]
    [Path("/SettingGeneral/SettingTemplate")]
    [SettingHide()]
    [SettingContext("inventoryexpress.setting.general.label")]
    [Module("InventoryExpress")]
    [Context("general")]
    [Context("templateadd")]
    public sealed class PageSettingTemplateAdd : PageTemplateWebAppSetting
    {
        /// <summary>
        /// Formular
        /// </summary>
        private ControlFormularTemplate form;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageSettingTemplateAdd()
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
                EnableCancelButton = false,
                BackUri = Uri.Take(-1)
            };

            lock (ViewModel.Instance.Database)
            {
                foreach (var v in ViewModel.Instance.Attributes)
                {
                    form.Attributes.Options.Add(new ControlFormularItemInputMoveSelectorItem()
                    {
                        ID = v.Guid,
                        Value = v.Name
                    });
                }

                form.TemplateName.Value = string.Empty;
                form.Attributes.Value = string.Join(";", ViewModel.Instance.Attributes);
                form.Description.Value = string.Empty;
                form.Tag.Value = string.Empty;
            }
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();

            form.TemplateName.Validation += (s, e) =>
            {
                if (e.Value.Count() < 1)
                {
                    e.Results.Add(new ValidationResult() { Text = this.I18N("inventoryexpress.template.validation.name.invalid"), Type = TypesInputValidity.Error });
                }
                else if (ViewModel.Instance.Templates.Where(x => x.Name.Equals(e.Value)).Any())
                {
                    e.Results.Add(new ValidationResult() { Text = this.I18N("inventoryexpress.template.validation.name.used"), Type = TypesInputValidity.Error });
                }
            };

            form.ProcessFormular += (s, e) =>
            {
                // Neue Vorlage erstellen und speichern
                var template = new Template()
                {
                    Name = form.TemplateName.Value,
                    Description = form.Description.Value,
                    Tag = form.Tag.Value,
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                    Guid = Guid.NewGuid().ToString()
                };

                if (GetParam(form.Image.Name) is ParameterFile file)
                {
                    if (template.Media == null)
                    {
                        template.Media = new Media()
                        {
                            Name = file.Value,
                            Data = file.Data,
                            Created = DateTime.Now,
                            Updated = DateTime.Now,
                            Guid = Guid.NewGuid().ToString()
                        };
                    }
                    else
                    {
                        template.Media.Name = file.Value;
                        template.Media.Data = file.Data;
                        template.Media.Updated = DateTime.Now;
                    }
                }

                var newAttributes = form.Attributes.Value?.Split(";", StringSplitOptions.RemoveEmptyEntries);

                lock (ViewModel.Instance.Database)
                {
                    ViewModel.Instance.Templates.Add(template);
                    ViewModel.Instance.SaveChanges();

                    // verknüpfe Attribute
                    foreach (var newItems in newAttributes.Join(ViewModel.Instance.Attributes, x => x, y => y.Guid, (x, y) => y))
                    {
                        ViewModel.Instance.TemplateAttributes.Add(new TemplateAttribute() { TemplateId = template.Id, AttributeId = newItems.Id });
                    }

                    ViewModel.Instance.SaveChanges();
                }

                form.Reset();
            };

            Content.Primary.Add(form);
        }
    }
}
