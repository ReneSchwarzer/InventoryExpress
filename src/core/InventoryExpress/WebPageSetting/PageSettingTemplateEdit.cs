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
            lock (ViewModel.Instance.Database)
            {
                var guid = e.Context.Request.GetParameter("TemplateID")?.Value;
                var template = ViewModel.Instance.Templates.Where(x => x.Guid == guid).FirstOrDefault();
                var orignalAttributes = ViewModel.Instance.TemplateAttributes
                    .Where(x => x.TemplateId == template.Id)
                    .Join(ViewModel.Instance.Attributes, t => t.AttributeId, a => a.Id, (t, a) => a.Guid).ToList();

                // Vorlage ändern und speichern
                template.Name = Form.TemplateName.Value;
                template.Description = Form.Description.Value;
                template.Tag = Form.Tag.Value;
                template.Updated = DateTime.Now;

                ViewModel.Instance.SaveChanges();

                var changeAttributes = Form.Attributes.Value?.Split(";", StringSplitOptions.RemoveEmptyEntries);

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
            }
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
                var guid = e.Context.Request.GetParameter("TemplateID")?.Value;
                var template = ViewModel.Instance.Templates.Where(x => x.Guid == guid).FirstOrDefault();

                if (e.Value.Count() < 1)
                {
                    e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.template.validation.name.invalid"));
                }
                else if (!template.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase) && ViewModel.Instance.Suppliers.Where(x => x.Name.Equals(e.Value)).Count() > 0)
                {
                    e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.template.validation.name.used"));
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

            var guid = e.Context.Request.GetParameter("TemplateID")?.Value;
            var template = ViewModel.Instance.Templates.Where(x => x.Guid == guid).FirstOrDefault();
            var orignalAttributes = ViewModel.Instance.TemplateAttributes
                .Where(x => x.TemplateId == template.Id)
                .Join(ViewModel.Instance.Attributes, t => t.AttributeId, a => a.Id, (t, a) => a.Guid).ToList();

            Form.Attributes.Options.Clear();

            lock (ViewModel.Instance.Database)
            {
                foreach (var v in ViewModel.Instance.Attributes)
                {
                    Form.Attributes.Options.Add(new ControlFormularItemInputSelectionItem()
                    {
                        ID = v.Guid,
                        Label = v.Name
                    });
                }
            }
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
                var guid = e.Context.Request.GetParameter("TemplateID")?.Value;
                var template = ViewModel.Instance.Templates.Where(x => x.Guid == guid).FirstOrDefault();
                var orignalAttributes = ViewModel.Instance.TemplateAttributes
                    .Where(x => x.TemplateId == template.Id)
                    .Join(ViewModel.Instance.Attributes, t => t.AttributeId, a => a.Id, (t, a) => a.Guid).ToList();

                Form.TemplateName.Value = template?.Name;
                Form.Attributes.Value = string.Join(";", orignalAttributes);
                Form.Description.Value = template?.Description;
                Form.Tag.Value = template?.Tag;
            }
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            lock (ViewModel.Instance.Database)
            {
                var guid = context.Request.GetParameter("TemplateID")?.Value;
                var template = ViewModel.Instance.Templates.Where(x => x.Guid == guid).FirstOrDefault();

                context.Request.Uri.Display = template.Name;
            }

            context.VisualTree.Content.Primary.Add(Form);
        }
    }
}
