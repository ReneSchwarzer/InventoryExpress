using InventoryExpress.Model;
using InventoryExpress.Model.Entity;
using InventoryExpress.WebControl;
using System;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [ID("TemplateAdd")]
    [Title("inventoryexpress:inventoryexpress.template.add.label")]
    [Segment("add", "inventoryexpress:inventoryexpress.template.add.label")]
    [Path("/Template")]
    [Module("inventoryexpress")]
    [Context("general")]
    public sealed class PageTemplateAdd : PageWebApp, IPageTemplate
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageTemplateAdd()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var form = new ControlFormularTemplate();
            form.EnableCancelButton = true;
            form.RedirectUri = context.Uri.Take(-1);
            form.BackUri = context.Uri.Take(-1);

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

                ViewModel.Instance.Templates.Add(template);
                ViewModel.Instance.SaveChanges();
            };

            context.VisualTree.Content.Primary.Add(form);
        }
    }
}
