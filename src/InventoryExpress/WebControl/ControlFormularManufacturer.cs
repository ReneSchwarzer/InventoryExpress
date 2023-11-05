using InventoryExpress.Model;
using InventoryExpress.Parameter;
using System;
using System.Linq;
using WebExpress.WebUI.WebControl;
using WebExpress.WebApp.WebApiControl;
using WebExpress.WebApp.WebPage;

namespace InventoryExpress.WebControl
{
    public class ControlFormularManufacturer : ControlForm
    {
        /// <summary>
        /// Liefert den Namen des Herstellers
        /// </summary>
        public ControlFormItemInputTextBox ManufacturerName { get; } = new ControlFormItemInputTextBox()
        {
            Name = "name",
            Label = "inventoryexpress:inventoryexpress.manufacturer.form.name.label",
            Help = "inventoryexpress:inventoryexpress.manufacturer.form.name.description",
            Icon = new PropertyIcon(TypeIcon.Font)
        };

        /// <summary>
        /// Liefert die Beschreibung
        /// </summary>
        public ControlFormItemInputTextBox Description { get; } = new ControlFormItemInputTextBox("note")
        {
            Name = "description",
            Label = "inventoryexpress:inventoryexpress.manufacturer.form.description.label",
            Help = "inventoryexpress:inventoryexpress.manufacturer.form.description.description",
            Format = TypesEditTextFormat.Wysiwyg,
            Icon = new PropertyIcon(TypeIcon.CommentAlt),
            Rows = 10
        };

        /// <summary>
        /// Liefert die Aaddresse
        /// </summary>
        public ControlFormItemInputTextBox Address { get; } = new ControlFormItemInputTextBox("adress")
        {
            Name = "adress",
            Label = "inventoryexpress:inventoryexpress.manufacturer.form.adress.label",
            Help = "inventoryexpress:inventoryexpress.manufacturer.form.adress.description",
            Icon = new PropertyIcon(TypeIcon.Home)
        };

        /// <summary>
        /// Liefert die Postleitzahl
        /// </summary>
        public ControlFormItemInputTextBox Zip { get; } = new ControlFormItemInputTextBox("zip")
        {
            Name = "zip",
            Label = "inventoryexpress:inventoryexpress.manufacturer.form.zip.label",
            Help = "inventoryexpress:inventoryexpress.manufacturer.form.zip.description",
            Icon = new PropertyIcon(TypeIcon.MapMarker)
        };

        /// <summary>
        /// Liefert den Ort
        /// </summary>
        public ControlFormItemInputTextBox Place { get; } = new ControlFormItemInputTextBox("place")
        {
            Name = "place",
            Label = "inventoryexpress:inventoryexpress.manufacturer.form.place.label",
            Help = "inventoryexpress:inventoryexpress.manufacturer.form.place.description",
            Icon = new PropertyIcon(TypeIcon.City)
        };

        /// <summary>
        /// Liefert Returns or sets the tags.
        /// </summary>
        public ControlApiFormularItemInputSelection Tag { get; } = new ControlApiFormularItemInputSelection("tags")
        {
            Name = "tag",
            Label = "inventoryexpress:inventoryexpress.manufacturer.form.tag.label",
            Help = "inventoryexpress:inventoryexpress.manufacturer.form.tag.description",
            Icon = new PropertyIcon(TypeIcon.Tag),
            MultiSelect = true
        };

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlFormularManufacturer(string id = null)
            : base(id)
        {
            Name = "manufacturer";
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Three);
            BackgroundColor = LayoutSchema.FormularBackground;
            Layout = TypeLayoutForm.Horizontal;

            ManufacturerName.Validation += ManufacturerNameValidation;
            Zip.Validation += ZipValidation;

            var group = new ControlFormItemGroupColumnVertical() { Distribution = new int[] { 33 } };
            group.Items.Add(Zip);
            group.Items.Add(Place);

            Add(ManufacturerName);
            Add(Description);
            Add(Address);
            Add(new ControlFormItemInputGroup(null, group));
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
        /// Wird ausgelöst, wenn das Feld Zip validiert werden soll.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument./param>
        private void ZipValidation(object sender, ValidationEventArgs e)
        {
            if (e.Value != null && e.Value.Length >= 10)
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.manufacturer.validation.zip.tolong"));
            }
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Feld ManufacturerName validiert werden soll.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument./param>
        private void ManufacturerNameValidation(object sender, ValidationEventArgs e)
        {
            var guid = e.Context.Request.GetParameter<ParameterManufacturerId>()?.Value;
            var manufacturer = ViewModel.GetManufacturer(guid);

            if (e.Value == null || e.Value.Length < 1)
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.manufacturer.validation.name.invalid"));
            }
            else if
            (
                manufacturer == null &&
                ViewModel.GetManufacturers().Where(x => x.Name.Equals(e.Value, StringComparison.OrdinalIgnoreCase)).Any()
            )
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.manufacturer.validation.name.used"));
            }
            else if
            (
                manufacturer != null &&
                !manufacturer.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase) &&
                ViewModel.GetManufacturers().Where(x => x.Name.Equals(e.Value, StringComparison.OrdinalIgnoreCase)).Any()
            )
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.manufacturer.validation.name.used"));
            }
        }
    }
}
