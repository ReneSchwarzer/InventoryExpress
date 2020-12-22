using InventoryExpress.WebControl;
using InventoryExpress.Model;
using System;
using System.Linq;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebResource;
using WebExpress.Attribute;

namespace InventoryExpress.WebResource
{
    [ID("TemplateAdd")]
    [Title("inventoryexpress.template.add.label")]
    [Segment("add", "inventoryexpress.template.add.label")]
    [Path("/Template")]
    [Module("InventoryExpress")]
    [Context("general")]
    public sealed class PageTemplateAdd : PageTemplateWebApp, IPageTemplate
    {
        /// <summary>
        /// Formular
        /// </summary>
        private ControlFormularTemplate form;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageTemplateAdd()
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
                RedirectUrl = Uri.Take(-1)
            };
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();

            Content.Content.Add(form);

            form.TemplateName.Validation += (s, e) =>
            {
                if (e.Value.Count() < 1)
                {
                    e.Results.Add(new ValidationResult() { Text = "Geben Sie einen gültigen Namen ein!", Type = TypesInputValidity.Error });
                }
                else if (ViewModel.Instance.Templates.Where(x => x.Name.Equals(e.Value)).Count() > 0)
                {
                    e.Results.Add(new ValidationResult() { Text = "Die Vorlage wird bereits verwendet. Geben Sie einen anderen Namen an!", Type = TypesInputValidity.Error });
                }
            };

            form.ProcessFormular += (s, e) =>
            {
                // Neues Herstellerobjekt erstellen und speichern
                var template = new Template()
                {
                    Name = form.TemplateName.Value,
                    //Tag = form.Tag.Value,
                    Description = form.Description.Value,
                    Guid = Guid.NewGuid().ToString()
                };

                ViewModel.Instance.Templates.Add(template);
                ViewModel.Instance.SaveChanges();
            };
        }
    }
}
