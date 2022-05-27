using InventoryExpress.Model;
using System;
using System.Linq;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebApiControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.Wql;

namespace InventoryExpress.WebControl
{
    public class ControlFormularManufacturer : ControlFormular
    {
        /// <summary>
        /// Liefert den Namen des Herstellers
        /// </summary>
        public ControlFormularItemInputTextBox ManufacturerName { get; } = new ControlFormularItemInputTextBox()
        {
            Name = "name",
            Label = "inventoryexpress:inventoryexpress.manufacturer.form.name.label",
            Help = "inventoryexpress:inventoryexpress.manufacturer.form.name.description",
            Icon = new PropertyIcon(TypeIcon.Font)
        };

        /// <summary>
        /// Liefert die Beschreibung
        /// </summary>
        public ControlFormularItemInputTextBox Description { get; } = new ControlFormularItemInputTextBox("note")
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
        public ControlFormularItemInputTextBox Address { get; } = new ControlFormularItemInputTextBox("adress")
        {
            Name = "adress",
            Label = "inventoryexpress:inventoryexpress.manufacturer.form.adress.label",
            Help = "inventoryexpress:inventoryexpress.manufacturer.form.adress.description",
            Icon = new PropertyIcon(TypeIcon.Home)
        };

        /// <summary>
        /// Liefert die Postleitzahl
        /// </summary>
        public ControlFormularItemInputTextBox Zip { get; } = new ControlFormularItemInputTextBox("zip")
        {
            Name = "zip",
            Label = "inventoryexpress:inventoryexpress.manufacturer.form.zip.label",
            Help = "inventoryexpress:inventoryexpress.manufacturer.form.zip.description",
            Icon = new PropertyIcon(TypeIcon.MapMarker)
        };

        /// <summary>
        /// Liefert den Ort
        /// </summary>
        public ControlFormularItemInputTextBox Place { get; } = new ControlFormularItemInputTextBox("place")
        {
            Name = "place",
            Label = "inventoryexpress:inventoryexpress.manufacturer.form.place.label",
            Help = "inventoryexpress:inventoryexpress.manufacturer.form.place.description",
            Icon = new PropertyIcon(TypeIcon.City)
        };

        /// <summary>
        /// Liefert das Bild
        /// </summary>
        public ControlFormularItemInputFile Image { get; } = new ControlFormularItemInputFile()
        {
            Name = "image",
            Label = "inventoryexpress:inventoryexpress.manufacturer.form.image.label",
            Help = "inventoryexpress:inventoryexpress.manufacturer.form.image.description",
            Icon = new PropertyIcon(TypeIcon.Image),
            AcceptFile = new string[] { "image/*" }
        };

        /// <summary>
        /// Liefert die Schlagwörter
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
        /// Bestimmt, ob das Formular zum Bearbeiten oder zum Neuanlegen verwendet werden soll.
        /// </summary>
        public bool Edit { get; set; } = false;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormularManufacturer(string id = null)
            : base(id)
        {
            Name = "manufacturer";
            EnableCancelButton = false;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Three);
            BackgroundColor = LayoutSchema.FormularBackground;
            Layout = TypeLayoutFormular.Horizontal;

            ManufacturerName.Validation += ManufacturerNameValidation;
            Zip.Validation += ZipValidation;

            var group = new ControlFormularItemGroupColumnVertical() { Distribution = new int[] { 33 } };
            group.Items.Add(Zip);
            group.Items.Add(Place);

            Add(ManufacturerName);
            Add(Description);
            Add(Address);
            Add(new ControlFormularItemInputGroup(null, group));

            if (!Edit)
            {
                Add(Image);
            }

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
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.manufacturer.validation.zip.tolong"));
            }
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Feld ManufacturerName validiert werden soll.
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente/param>
        private void ManufacturerNameValidation(object sender, ValidationEventArgs e)
        {
            var guid = e.Context.Request.GetParameter("ManufacturerID")?.Value;
            var manufacturer = ViewModel.GetManufacturer(guid);

            if (e.Value == null || e.Value.Length < 1)
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.manufacturer.validation.name.invalid"));
            }
            else if
            (
                manufacturer == null &&
                ViewModel.GetManufacturers(new WqlStatement()).Where(x => x.Name.Equals(e.Value, StringComparison.OrdinalIgnoreCase)).Any()
            )
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.manufacturer.validation.name.used"));
            }
            else if
            (
                manufacturer != null &&
                !manufacturer.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase) &&
                ViewModel.GetManufacturers(new WqlStatement()).Where(x => x.Name.Equals(e.Value, StringComparison.OrdinalIgnoreCase)).Any()
            )
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.manufacturer.validation.name.used"));
            }
        }
    }
}
