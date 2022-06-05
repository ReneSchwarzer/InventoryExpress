using InventoryExpress.Model;
using System;
using System.Linq;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebApiControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.Wql;

namespace InventoryExpress.WebControl
{
    public class ControlFormularLocation : ControlFormular
    {
        /// <summary>
        /// Liefert den Namen des Standortes
        /// </summary>
        public ControlFormularItemInputTextBox LocationName { get; } = new ControlFormularItemInputTextBox()
        {
            Name = "name",
            Label = "inventoryexpress:inventoryexpress.location.form.name.label",
            Help = "inventoryexpress:inventoryexpress.location.form.name.description",
            Icon = new PropertyIcon(TypeIcon.Font)
        };

        /// <summary>
        /// Liefert die Beschreibung
        /// </summary>
        public ControlFormularItemInputTextBox Description { get; } = new ControlFormularItemInputTextBox("note")
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
        public ControlFormularItemInputTextBox Address { get; } = new ControlFormularItemInputTextBox("adress")
        {
            Name = "adress",
            Label = "inventoryexpress:inventoryexpress.location.form.adress.label",
            Help = "inventoryexpress:inventoryexpress.location.form.adress.description",
            Icon = new PropertyIcon(TypeIcon.Home)
        };

        /// <summary>
        /// Liefert die Postleitzahl
        /// </summary>
        public ControlFormularItemInputTextBox Zip { get; } = new ControlFormularItemInputTextBox("zip")
        {
            Name = "zip",
            Label = "inventoryexpress:inventoryexpress.location.form.zip.label",
            Help = "inventoryexpress:inventoryexpress.location.form.zip.description",
            Icon = new PropertyIcon(TypeIcon.MapMarker)
        };

        /// <summary>
        /// Liefert den Ort
        /// </summary>
        public ControlFormularItemInputTextBox Place { get; } = new ControlFormularItemInputTextBox("place")
        {
            Name = "place",
            Label = "inventoryexpress:inventoryexpress.location.form.place.label",
            Help = "inventoryexpress:inventoryexpress.location.form.place.description",
            Icon = new PropertyIcon(TypeIcon.City)
        };

        /// <summary>
        /// Liefert das Gebäude
        /// </summary>
        public ControlFormularItemInputTextBox Building { get; } = new ControlFormularItemInputTextBox("building")
        {
            Name = "building",
            Label = "inventoryexpress:inventoryexpress.location.form.building.label",
            Help = "inventoryexpress:inventoryexpress.location.form.building.description",
            Icon = new PropertyIcon(TypeIcon.Building)
        };

        /// <summary>
        /// Liefert den Room
        /// </summary>
        public ControlFormularItemInputTextBox Room { get; } = new ControlFormularItemInputTextBox("room")
        {
            Name = "room",
            Label = "inventoryexpress:inventoryexpress.location.form.room.label",
            Help = "inventoryexpress:inventoryexpress.location.form.room.description",
            Icon = new PropertyIcon(TypeIcon.DoorOpen)
        };

        /// <summary>
        /// Liefert die Schlagwörter
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
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormularLocation(string id = null)
            : base(id)
        {
            Name = "location";
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Three, PropertySpacing.Space.None, PropertySpacing.Space.None);
            BackgroundColor = LayoutSchema.FormularBackground;
            Layout = TypeLayoutFormular.Horizontal;

            LocationName.Validation += LocationNameValidation;
            Zip.Validation += ZipValidation;

            var group1 = new ControlFormularItemGroupColumnVertical() { Distribution = new int[] { 33 } };
            group1.Items.Add(Zip);
            group1.Items.Add(Place);

            var group2 = new ControlFormularItemGroupColumnVertical() { Distribution = new int[] { 33 } };
            group2.Items.Add(Building);
            group2.Items.Add(Room);

            Add(LocationName);
            Add(Description);
            Add(Address);
            Add(new ControlFormularItemInputGroup(null, group1));
            Add(new ControlFormularItemInputGroup(null, group2));
            Add(Tag);
        }

        /// <summary>
        /// Initialisiert das Formular
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        public override void Initialize(RenderContextFormular context)
        {
            base.Initialize(context);

            Tag.RestUri = context.Uri.Root.Append("api/v1/tags");
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Feld Zip validiert werden soll.
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente/param>
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
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente/param>
        private void LocationNameValidation(object sender, ValidationEventArgs e)
        {
            var guid = e.Context.Request.GetParameter("LocationID")?.Value;
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
