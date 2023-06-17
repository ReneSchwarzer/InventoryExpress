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
    public class ControlFormularLocation : ControlForm
    {
        /// <summary>
        /// Liefert den Namen des Standortes
        /// </summary>
        public ControlFormItemInputTextBox LocationName { get; } = new ControlFormItemInputTextBox()
        {
            Name = "name",
            Label = "inventoryexpress:inventoryexpress.location.form.name.label",
            Help = "inventoryexpress:inventoryexpress.location.form.name.description",
            Icon = new PropertyIcon(TypeIcon.Font)
        };

        /// <summary>
        /// Liefert die Beschreibung
        /// </summary>
        public ControlFormItemInputTextBox Description { get; } = new ControlFormItemInputTextBox("note")
        {
            Name = "description",
            Label = "inventoryexpress:inventoryexpress.location.form.description.label",
            Help = "inventoryexpress:inventoryexpress.location.form.description.description",
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
            Label = "inventoryexpress:inventoryexpress.location.form.adress.label",
            Help = "inventoryexpress:inventoryexpress.location.form.adress.description",
            Icon = new PropertyIcon(TypeIcon.Home)
        };

        /// <summary>
        /// Liefert die Postleitzahl
        /// </summary>
        public ControlFormItemInputTextBox Zip { get; } = new ControlFormItemInputTextBox("zip")
        {
            Name = "zip",
            Label = "inventoryexpress:inventoryexpress.location.form.zip.label",
            Help = "inventoryexpress:inventoryexpress.location.form.zip.description",
            Icon = new PropertyIcon(TypeIcon.MapMarker)
        };

        /// <summary>
        /// Liefert den Ort
        /// </summary>
        public ControlFormItemInputTextBox Place { get; } = new ControlFormItemInputTextBox("place")
        {
            Name = "place",
            Label = "inventoryexpress:inventoryexpress.location.form.place.label",
            Help = "inventoryexpress:inventoryexpress.location.form.place.description",
            Icon = new PropertyIcon(TypeIcon.City)
        };

        /// <summary>
        /// Liefert das Gebäude
        /// </summary>
        public ControlFormItemInputTextBox Building { get; } = new ControlFormItemInputTextBox("building")
        {
            Name = "building",
            Label = "inventoryexpress:inventoryexpress.location.form.building.label",
            Help = "inventoryexpress:inventoryexpress.location.form.building.description",
            Icon = new PropertyIcon(TypeIcon.Building)
        };

        /// <summary>
        /// Liefert den Room
        /// </summary>
        public ControlFormItemInputTextBox Room { get; } = new ControlFormItemInputTextBox("room")
        {
            Name = "room",
            Label = "inventoryexpress:inventoryexpress.location.form.room.label",
            Help = "inventoryexpress:inventoryexpress.location.form.room.description",
            Icon = new PropertyIcon(TypeIcon.DoorOpen)
        };

        /// <summary>
        /// Liefert Returns or sets the tags.
        /// </summary>
        public ControlApiFormularItemInputSelection Tag { get; } = new ControlApiFormularItemInputSelection("tags")
        {
            Name = "tag",
            Label = "inventoryexpress:inventoryexpress.location.form.tag.label",
            Help = "inventoryexpress:inventoryexpress.location.form.tag.description",
            Icon = new PropertyIcon(TypeIcon.Tag),
            MultiSelect = true
        };

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlFormularLocation(string id = null)
            : base(id)
        {
            Name = "location";
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Three, PropertySpacing.Space.None, PropertySpacing.Space.None);
            BackgroundColor = LayoutSchema.FormularBackground;
            Layout = TypeLayoutFormular.Horizontal;

            LocationName.Validation += LocationNameValidation;
            Zip.Validation += ZipValidation;

            var group1 = new ControlFormItemGroupColumnVertical() { Distribution = new int[] { 33 } };
            group1.Items.Add(Zip);
            group1.Items.Add(Place);

            var group2 = new ControlFormItemGroupColumnVertical() { Distribution = new int[] { 33 } };
            group2.Items.Add(Building);
            group2.Items.Add(Room);

            Add(LocationName);
            Add(Description);
            Add(Address);
            Add(new ControlFormItemInputGroup(null, group1));
            Add(new ControlFormItemInputGroup(null, group2));
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
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.location.validation.zip.tolong"));
            }
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Feld LocationName validiert werden soll.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument./param>
        private void LocationNameValidation(object sender, ValidationEventArgs e)
        {
            var guid = e.Context.Request.GetParameter<ParameterLocationId>()?.Value;
            var location = ViewModel.GetLocation(guid);

            if (e.Value == null || e.Value.Length < 1)
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.location.validation.name.invalid"));
            }
            else if
            (
                location == null &&
                ViewModel.GetLocations(new WqlStatement()).Where(x => x.Name.Equals(e.Value, StringComparison.OrdinalIgnoreCase)).Any()
            )
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.location.validation.name.used"));
            }
            else if
            (
                location != null &&
                !location.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase) &&
                ViewModel.GetLocations(new WqlStatement()).Where(x => x.Name.Equals(e.Value, StringComparison.OrdinalIgnoreCase)).Any()
            )
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.location.validation.name.used"));
            }
        }
    }
}
