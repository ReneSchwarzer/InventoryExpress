using InventoryExpress.Model;
using InventoryExpress.Parameter;
using System;
using System.Linq;
using WebExpress.WebUI.WebControl;
using WebExpress.WebApp.WebPage;

namespace InventoryExpress.WebControl
{
    public class ControlFormularAttribute : ControlForm
    {
        /// <summary>
        /// Returns the name.
        /// </summary>
        public ControlFormItemInputTextBox AttributeName { get; } = new ControlFormItemInputTextBox()
        {
            Name = "name",
            Label = "inventoryexpress:inventoryexpress.attribute.form.name.label",
            Help = "inventoryexpress:inventoryexpress.attribute.form.name.description",
            Icon = new PropertyIcon(TypeIcon.Font)
        };

        /// <summary>
        /// Returns the description.
        /// </summary>
        public ControlFormItemInputTextBox Description { get; } = new ControlFormItemInputTextBox("note")
        {
            Name = "description",
            Label = "inventoryexpress:inventoryexpress.attribute.form.description.label",
            Help = "inventoryexpress:inventoryexpress.attribute.form.description.description",
            Format = TypesEditTextFormat.Wysiwyg,
            Icon = new PropertyIcon(TypeIcon.CommentAlt)
        };

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlFormularAttribute(string id = null)
            : base(id)
        {
            Name = "attribute";
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Three, PropertySpacing.Space.None, PropertySpacing.Space.None);
            BackgroundColor = LayoutSchema.FormularBackground;
            Layout = TypeLayoutForm.Horizontal;

            AttributeName.Validation += AttributeNameValidation;

            Add(AttributeName);
            Add(Description);
        }

        /// <summary>
        /// Initializes the form.
        /// </summary>
        /// <param name="context">The context in which the control is represented.</param>
        public override void Initialize(RenderContextFormular context)
        {
            base.Initialize(context);
        }

        /// <summary>
        /// Dispatched when the AttributeName field is to be validated.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument./param>
        private void AttributeNameValidation(object sender, ValidationEventArgs e)
        {
            var guid = e.Context.Request.GetParameter<ParameterAttributeId>()?.Value;
            var attribute = ViewModel.GetAttribute(guid);

            if (e.Value == null || e.Value.Length < 1)
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.attribute.validation.name.invalid"));
            }
            else if
            (
                attribute == null &&
                ViewModel.GetAttributes().Where(x => x.Name.Equals(e.Value, StringComparison.OrdinalIgnoreCase)).Any()
            )
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.attribute.validation.name.used"));
            }
            else if
            (
                attribute != null &&
                !attribute.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase) &&
                ViewModel.GetAttributes().Where(x => x.Name.Equals(e.Value, StringComparison.OrdinalIgnoreCase)).Any()
            )
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.attribute.validation.name.used"));
            }
        }
    }
}
