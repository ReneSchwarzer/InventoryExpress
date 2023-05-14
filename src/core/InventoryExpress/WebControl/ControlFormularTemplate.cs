using InventoryExpress.Model;
using System;
using System.Linq;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebApiControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.Wql;

namespace InventoryExpress.WebControl
{
    public class ControlFormularTemplate : ControlFormular
    {
        /// <summary>
        /// Liefert den Namen der Vorlage
        /// </summary>
        public ControlFormularItemInputTextBox TemplateName { get; } = new ControlFormularItemInputTextBox()
        {
            Name = "name",
            Label = "inventoryexpress:inventoryexpress.template.form.name.label",
            Help = "inventoryexpress:inventoryexpress.template.form.name.description",
            Icon = new PropertyIcon(TypeIcon.Font)
        };

        /// <summary>
        /// Liefert die ungenutzten Attribute
        /// </summary>
        public ControlFormularItemInputMove Attributes { get; } = new ControlFormularItemInputMove("attributes")
        {
            Name = "attributes",
            Label = "inventoryexpress:inventoryexpress.template.form.unused.label",
            Help = "inventoryexpress:inventoryexpress.template.form.unused.description",
            Icon = new PropertyIcon(TypeIcon.Cubes)
        };

        /// <summary>
        /// Liefert die Beschreibung
        /// </summary>
        public ControlFormularItemInputTextBox Description { get; } = new ControlFormularItemInputTextBox("note")
        {
            Name = "description",
            Label = "inventoryexpress:inventoryexpress.template.form.description.label",
            Help = "inventoryexpress:inventoryexpress.template.form.description.description",
            Format = TypesEditTextFormat.Wysiwyg,
            Icon = new PropertyIcon(TypeIcon.CommentAlt)
        };

        /// <summary>
        /// Liefert die Schlagwörter
        /// </summary>
        public ControlApiFormularItemInputSelection Tag { get; } = new ControlApiFormularItemInputSelection("tags")
        {
            Name = "tag",
            Label = "inventoryexpress:inventoryexpress.template.form.tag.label",
            Help = "inventoryexpress:inventoryexpress.template.form.tag.description",
            Icon = new PropertyIcon(TypeIcon.Tag),
            MultiSelect = true
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlFormularTemplate(string id = null)
            : base(id)
        {
            Name = "template";
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Three, PropertySpacing.Space.None, PropertySpacing.Space.None);
            BackgroundColor = LayoutSchema.FormularBackground;
            Layout = TypeLayoutFormular.Horizontal;

            TemplateName.Validation += TemplateNameValidation;

            Add(TemplateName);
            Add(Attributes);
            Add(Description);
            Add(Tag);
        }

        /// <summary>
        /// Initialisiert das Formular
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        public override void Initialize(RenderContextFormular context)
        {
            base.Initialize(context);

            Tag.RestUri = context.Uri.ModuleRoot.Append("api/v1/tags");
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Feld TemplateName validiert werden soll.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">Die Eventargumente/param>
        private void TemplateNameValidation(object sender, ValidationEventArgs e)
        {
            var guid = e.Context.Request.GetParameter("TemplateID")?.Value;
            var template = ViewModel.GetTemplate(guid);

            if (e.Value == null || e.Value.Length < 1)
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.template.validation.name.invalid"));
            }
            else if
            (
                template == null &&
                ViewModel.GetTemplates(new WqlStatement()).Where(x => x.Name.Equals(e.Value, StringComparison.OrdinalIgnoreCase)).Any()
            )
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.template.validation.name.used"));
            }
            else if
            (
                template != null &&
                !template.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase) &&
                ViewModel.GetTemplates(new WqlStatement()).Where(x => x.Name.Equals(e.Value, StringComparison.OrdinalIgnoreCase)).Any()
            )
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.template.validation.name.used"));
            }
        }
    }
}
