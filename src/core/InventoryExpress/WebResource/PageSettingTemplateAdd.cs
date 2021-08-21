using InventoryExpress.WebControl;
using InventoryExpress.Model;
using System;
using System.Linq;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebResource;
using WebExpress.Attribute;
using WebExpress.Message;
using WebExpress.Internationalization;
using WebExpress.WebApp.Attribute;

namespace InventoryExpress.WebResource
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
    public sealed class PageSettingTemplateAdd : PageTemplateWebAppSetting, IPageTemplate
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
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();

            Content.Primary.Add(form);

            form.TemplateName.Validation += (s, e) =>
            {
                if (e.Value.Count() < 1)
                {
                    e.Results.Add(new ValidationResult() { Text = this.I18N("inventoryexpress.template.validation.name.invalid"), Type = TypesInputValidity.Error });
                }
                else if (ViewModel.Instance.Templates.Where(x => x.Name.Equals(e.Value)).Count() > 0)
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

                ViewModel.Instance.Templates.Add(template);
                ViewModel.Instance.SaveChanges();
            };
        }
    }
}
