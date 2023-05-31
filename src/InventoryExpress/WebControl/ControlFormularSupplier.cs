using InventoryExpress.Model;
using System;
using System.Linq;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebApiControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.Wql;

namespace InventoryExpress.WebControl
{
    public class ControlFormularSupplier : ControlForm
    {
        /// <summary>
        /// Liefert den Namen des Lieferanten
        /// </summary>
        public ControlFormItemInputTextBox SupplierName { get; } = new ControlFormItemInputTextBox()
        {
            Name = "name",
            Label = "inventoryexpress:inventoryexpress.supplier.form.name.label",
            Help = "inventoryexpress:inventoryexpress.supplier.form.name.description",
            Icon = new PropertyIcon(TypeIcon.Font)
        };

        /// <summary>
        /// Liefert die Beschreibung
        /// </summary>
        public ControlFormItemInputTextBox Description { get; } = new ControlFormItemInputTextBox("note")
        {
            Name = "description",
            Label = "inventoryexpress:inventoryexpress.supplier.form.description.label",
            Help = "inventoryexpress:inventoryexpress.supplier.form.description.description",
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
            Label = "inventoryexpress:inventoryexpress.supplier.form.adress.label",
            Help = "inventoryexpress:inventoryexpress.supplier.form.adress.description",
            Icon = new PropertyIcon(TypeIcon.Home)
        };

        /// <summary>
        /// Liefert die Postleitzahl
        /// </summary>
        public ControlFormItemInputTextBox Zip { get; } = new ControlFormItemInputTextBox("zip")
        {
            Name = "zip",
            Label = "inventoryexpress:inventoryexpress.supplier.form.zip.label",
            Help = "inventoryexpress:inventoryexpress.supplier.form.zip.description",
            Icon = new PropertyIcon(TypeIcon.MapMarker)
        };

        /// <summary>
        /// Liefert den Ort
        /// </summary>
        public ControlFormItemInputTextBox Place { get; } = new ControlFormItemInputTextBox("place")
        {
            Name = "place",
            Label = "inventoryexpress:inventoryexpress.supplier.form.place.label",
            Help = "inventoryexpress:inventoryexpress.supplier.form.place.description",
            Icon = new PropertyIcon(TypeIcon.City)
        };

        /// <summary>
        /// Liefert Returns or sets the tags.
        /// </summary>
        public ControlApiFormularItemInputSelection Tag { get; } = new ControlApiFormularItemInputSelection("tags")
        {
            Name = "tag",
            Label = "inventoryexpress:inventoryexpress.supplier.form.tag.label",
            Help = "inventoryexpress:inventoryexpress.supplier.form.tag.description",
            Icon = new PropertyIcon(TypeIcon.Tag),
            MultiSelect = true
        };

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Returns or sets the id.</param>
        public ControlFormularSupplier(string id = null)
            : base(id)
        {
            Name = "supplier";
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Three, PropertySpacing.Space.None, PropertySpacing.Space.None);
            BackgroundColor = LayoutSchema.FormularBackground;
            Layout = TypeLayoutFormular.Horizontal;

            SupplierName.Validation += SupplierNameValidation;
            Zip.Validation += ZipValidation;

            var group = new ControlFormItemGroupColumnVertical() { Distribution = new int[] { 33 } };
            group.Items.Add(Zip);
            group.Items.Add(Place);

            Add(SupplierName);
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
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.supplier.validation.zip.tolong"));
            }
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Feld SupplierName validiert werden soll.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument./param>
        private void SupplierNameValidation(object sender, ValidationEventArgs e)
        {
            var guid = e.Context.Request.GetParameter("SupplierId")?.Value;
            var supplier = ViewModel.GetSupplier(guid);

            if (e.Value == null || e.Value.Length < 1)
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.supplier.validation.name.invalid"));
            }
            else if
            (
                supplier == null &&
                ViewModel.GetSuppliers(new WqlStatement()).Where(x => x.Name.Equals(e.Value, StringComparison.OrdinalIgnoreCase)).Any()
            )
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.supplier.validation.name.used"));
            }
            else if
            (
                supplier != null &&
                !supplier.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase) &&
                ViewModel.GetSuppliers(new WqlStatement()).Where(x => x.Name.Equals(e.Value, StringComparison.OrdinalIgnoreCase)).Any()
            )
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.supplier.validation.name.used"));
            }
        }
    }
}
