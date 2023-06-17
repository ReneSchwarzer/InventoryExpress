using InventoryExpress.Model;
using InventoryExpress.Parameter;
using System;
using System.Linq;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebApiControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.Wql;

namespace InventoryExpress.WebControl
{
    public class ControlFormularTemplate : ControlForm
    {
        /// <summary>
        /// Liefert den Namen der Vorlage
        /// </summary>
        public ControlFormItemInputTextBox TemplateName { get; } = new ControlFormItemInputTextBox()
        {
            Name = "name",
            Label = "inventoryexpress:inventoryexpress.template.form.name.label",
            Help = "inventoryexpress:inventoryexpress.template.form.name.description",
            Icon = new PropertyIcon(TypeIcon.Font)
        };

        /// <summary>
        /// Liefert die ungenutzten Attribute
        /// </summary>
        public ControlFormItemInputMove Attributes { get; } = new ControlFormItemInputMove("attributes")
        {
            Name = "attributes",
            Label = "inventoryexpress:inventoryexpress.template.form.unused.label",
            Help = "inventoryexpress:inventoryexpress.template.form.unused.description",
            Icon = new PropertyIcon(TypeIcon.Cubes)
        };

        /// <summary>
        /// Liefert die Beschreibung
        /// </summary>
        public ControlFormItemInputTextBox Description { get; } = new ControlFormItemInputTextBox("note")
        {
            Name = "description",
            Label = "inventoryexpress:inventoryexpress.template.form.description.label",
            Help = "inventoryexpress:inventoryexpress.template.form.description.description",
            Format = TypesEditTextFormat.Wysiwyg,
            Icon = new PropertyIcon(TypeIcon.CommentAlt)
        };

        /// <summary>
        /// Liefert Returns or sets the tags.
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
        /// Constructor
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
        /// Initializes the form.
        /// </summary>
        /// <param name="context">The context in which the control is represented.</param>
        public override void Initialize(RenderContextFormular context)
        {
            base.Initialize(context);

            Tag.RestUri = context.Uri.ModuleRoot.Append("api/v1/tags");
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Feld TemplateName validiert werden soll.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument./param>
        private void TemplateNameValidation(object sender, ValidationEventArgs e)
        {
            var guid = e.Context.Request.GetParameter<ParameterTemplateId>()?.Value;
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
