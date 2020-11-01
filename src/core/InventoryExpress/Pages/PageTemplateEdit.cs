using InventoryExpress.Controls;
using InventoryExpress.Model;
using System;
using System.Linq;
using WebExpress.UI.Controls;

namespace InventoryExpress.Pages
{
    public class PageTemplateEdit : PageBase, ITemplate
    {
        /// <summary>
        /// Formular
        /// </summary>
        private ControlFormularTemplate form;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageTemplateEdit()
            : base("Vorlage bearbeiten")
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Init()
        {
            base.Init();

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

            var id = Convert.ToInt32(GetParam("id"));
            var template = ViewModel.Instance.Templates.Where(x => x.ID == id).FirstOrDefault();

            Main.Content.Add(form);

            form.TemplateName.Value = template?.Name;
            form.Discription.Value = template?.Discription;

            form.TemplateName.Validation += (s, e) =>
            {
                if (e.Value.Count() < 1)
                {
                    e.Results.Add(new ValidationResult() { Text = "Geben Sie einen gültigen Namen ein!", Type = TypesInputValidity.Error });
                }
                else if (ViewModel.Instance.Templates.Where(x => x.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase)).Count() > 0)
                {
                    e.Results.Add(new ValidationResult() { Text = "Die Vorlage wird bereits verwendet. Geben Sie einen anderen Namen an!", Type = TypesInputValidity.Error });
                }
            };

            form.ProcessFormular += (s, e) =>
            {
                // Herstellerobjekt ändern und speichern
                template.Name = form.TemplateName.Value;
                //Tag = form.Tag.Value;
                template.Discription = form.Discription.Value;

                ViewModel.Instance.SaveChanges();
            };
        }
    }
}
