using InventoryExpress.Model;
using InventoryExpress.Model.Entity;
using InventoryExpress.WebControl;
using InventoryExpress.WebPage;
using System;
using System.IO;
using System.Linq;
using WebExpress.Message;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebAttribute;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.WebSettingPage;
using WebExpress.WebAttribute;
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
    public sealed class PageSettingTemplateAdd : PageWebAppSetting, IPageTemplate
    {
        /// <summary>
        /// Liefert das Formular
        /// </summary>
        private ControlFormularTemplate Form { get; } = new ControlFormularTemplate("template")
        {
            Edit = false
        };

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

            Form.InitializeFormular += InitializeFormular;
            Form.FillFormular += FillFormular;
            Form.TemplateName.Validation += TemplateNameValidation;
            Form.ProcessFormular += ProcessFormular;
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Formular verarbeitet werden soll.
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente/param>
        private void ProcessFormular(object sender, FormularEventArgs e)
        {

            // Neue Vorlage erstellen und speichern
            var template = new Template()
            {
                Name = Form.TemplateName.Value,
                Description = Form.Description.Value,
                Tag = Form.Tag.Value,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Guid = Guid.NewGuid().ToString()
            };

            if (e.Context.Request.GetParameter(Form.Image.Name) is ParameterFile file)
            {
                if (template.Media == null)
                {
                    template.Media = new Media()
                    {
                        Name = file.Value,
                        Created = DateTime.Now,
                        Updated = DateTime.Now,
                        Guid = Guid.NewGuid().ToString()
                    };
                }
                else
                {
                    template.Media.Name = file.Value;
                    template.Media.Updated = DateTime.Now;
                }

                File.WriteAllBytes(Path.Combine(e.Context.Application.AssetPath, "media", template.Media.Guid), file.Data);
            }

            var newAttributes = Form.Attributes.Value?.Split(";", StringSplitOptions.RemoveEmptyEntries);

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

            Form.Reset();
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Feld TemplateName validiert werden soll.
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente/param>
        private void TemplateNameValidation(object sender, ValidationEventArgs e)
        {
            lock (ViewModel.Instance.Database)
            {
                if (e.Value.Count() < 1)
                {
                    e.Results.Add(new ValidationResult() { Text = "inventoryexpress:inventoryexpress.template.validation.name.invalid", Type = TypesInputValidity.Error });
                }
                else if (ViewModel.Instance.Templates.Where(x => x.Name.Equals(e.Value)).Any())
                {
                    e.Results.Add(new ValidationResult() { Text = "inventoryexpress:inventoryexpress.template.validation.name.used", Type = TypesInputValidity.Error });
                }
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular initialisiert wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente</param>
        private void InitializeFormular(object sender, FormularEventArgs e)
        {
            Form.RedirectUri = e.Context.Uri.Take(-1);
            Form.BackUri = e.Context.Uri.Take(-1);
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular gefüllt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente</param>
        private void FillFormular(object sender, FormularEventArgs e)
        {
            lock (ViewModel.Instance.Database)
            {
                foreach (var v in ViewModel.Instance.Attributes)
                {
                    Form.Attributes.Options.Add(new ControlFormularItemInputMoveSelectorItem()
                    {
                        ID = v.Guid,
                        Value = v.Name
                    });
                }

                Form.Attributes.Value = string.Join(";", ViewModel.Instance.Attributes);
            }
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            context.VisualTree.Content.Primary.Add(Form);
        }
    }
}
