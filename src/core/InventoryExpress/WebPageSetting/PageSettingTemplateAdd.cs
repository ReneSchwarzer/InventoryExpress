using InventoryExpress.Model;
using InventoryExpress.WebControl;
using System;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.Attribute;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.WebSettingPage;
using WebExpress.WebResource;

namespace InventoryExpress.WebPageSetting
{
    [ID("SettingTemplateAdd")]
    [Title("inventoryexpress:inventoryexpress.template.add.label")]
    [Segment("add", "inventoryexpress:inventoryexpress.template.add.label")]
    [Path("/Setting/SettingTemplate")]
    [SettingHide()]
    [SettingContext("webexpress.webapp:setting.tab.general.label")]
    [Module("inventoryexpress")]
    [Context("general")]
    [Context("templateadd")]
    public sealed class PageSettingTemplateAdd : PageWebAppSetting
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
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            form = new ControlFormularTemplate()
            {
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

            form.RedirectUri = context.Request.Uri.Take(-1);
            form.BackUri = context.Request.Uri.Take(-1);

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

                if (context.Request.GetParameter(form.Image.Name) is ParameterFile file)
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

            visualTree.Content.Primary.Add(form);
        }
    }
}
