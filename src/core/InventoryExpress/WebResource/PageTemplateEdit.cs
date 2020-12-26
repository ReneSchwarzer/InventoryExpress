﻿using InventoryExpress.WebControl;
using InventoryExpress.Model;
using System;
using System.Linq;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebResource;
using WebExpress.Attribute;

namespace InventoryExpress.WebResource
{
    [ID("TemplateEdit")]
    [Title("inventoryexpress.template.edit.label")]
    [SegmentGuid("TemplateID", "inventoryexpress.template.edit.display")]
    [Path("/Template")]
    [Module("InventoryExpress")]
    [Context("general")]
    [Context("templateedit")]
    public sealed class PageTemplateEdit : PageTemplateWebApp, IPageTemplate
    {
        /// <summary>
        /// Formular
        /// </summary>
        private ControlFormularTemplate form;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageTemplateEdit()
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
                RedirectUrl = Uri.Take(-1),
                Edit = true
            };
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();

            var guid =GetParamValue("TemplateID");
            var template = ViewModel.Instance.Templates.Where(x => x.Guid == guid).FirstOrDefault();

            Content.Primary.Add(form);

            form.TemplateName.Value = template?.Name;
            form.Description.Value = template?.Description;

            form.TemplateName.Validation += (s, e) =>
            {
                if (e.Value.Count() < 1)
                {
                    e.Results.Add(new ValidationResult() { Text = "Geben Sie einen gültigen Namen ein!", Type = TypesInputValidity.Error });
                }
                else if (!template.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase) && ViewModel.Instance.Suppliers.Where(x => x.Name.Equals(e.Value)).Count() > 0)
                {
                    e.Results.Add(new ValidationResult() { Text = "Die Vorlage wird bereits verwendet. Geben Sie einen anderen Namen an!", Type = TypesInputValidity.Error });
                }
            };

            form.ProcessFormular += (s, e) =>
            {
                // Vorlage ändern und speichern
                template.Name = form.TemplateName.Value;
                template.Description = form.Description.Value;
                template.Updated = DateTime.Now;
                //manufacturer.Tag = form.Tag.Value;

                ViewModel.Instance.SaveChanges();
            };
        }
    }
}
