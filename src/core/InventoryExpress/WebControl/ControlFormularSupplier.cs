using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebPage;

namespace InventoryExpress.WebControl
{
    public class ControlFormularSupplier : ControlFormular
    {
        /// <summary>
        /// Liefert den Namen des Lieferanten
        /// </summary>
        public ControlFormularItemInputTextBox SupplierName { get; } = new ControlFormularItemInputTextBox()
        {
            Name = "name",
            Label = "inventoryexpress:inventoryexpress.supplier.form.name.label",
            Help = "inventoryexpress:inventoryexpress.supplier.form.name.description",
            Icon = new PropertyIcon(TypeIcon.Font)
        };

        /// <summary>
        /// Liefert die Beschreibung
        /// </summary>
        public ControlFormularItemInputTextBox Description { get; } = new ControlFormularItemInputTextBox("note")
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
        public ControlFormularItemInputTextBox Address { get; } = new ControlFormularItemInputTextBox("adress")
        {
            Name = "adress",
            Label = "inventoryexpress:inventoryexpress.supplier.form.adress.label",
            Help = "inventoryexpress:inventoryexpress.supplier.form.adress.description",
            Icon = new PropertyIcon(TypeIcon.Home)
        };

        /// <summary>
        /// Liefert die Postleitzahl
        /// </summary>
        public ControlFormularItemInputTextBox Zip { get; } = new ControlFormularItemInputTextBox("zip")
        {
            Name = "zip",
            Label = "inventoryexpress:inventoryexpress.supplier.form.zip.label",
            Help = "inventoryexpress:inventoryexpress.supplier.form.zip.description",
            Icon = new PropertyIcon(TypeIcon.MapMarker)
        };

        /// <summary>
        /// Liefert den Ort
        /// </summary>
        public ControlFormularItemInputTextBox Place { get; } = new ControlFormularItemInputTextBox("place")
        {
            Name = "place",
            Label = "inventoryexpress:inventoryexpress.supplier.form.place.label",
            Help = "inventoryexpress:inventoryexpress.supplier.form.place.description",
            Icon = new PropertyIcon(TypeIcon.City)
        };

        /// <summary>
        /// Liefert das Bild
        /// </summary>
        public ControlFormularItemInputFile Image { get; } = new ControlFormularItemInputFile()
        {
            Name = "image",
            Label = "inventoryexpress:inventoryexpress.supplier.form.image.label",
            Help = "inventoryexpress:inventoryexpress.supplier.form.image.description",
            Icon = new PropertyIcon(TypeIcon.Image),
            AcceptFile = new string[] { "image/*" }
        };

        /// <summary>
        /// Liefert die Schlagwörter
        /// </summary>
        public ControlFormularItemInputTag Tag { get; } = new ControlFormularItemInputTag("tags")
        {
            Name = "tag",
            Label = "inventoryexpress:inventoryexpress.supplier.form.tag.label",
            Help = "inventoryexpress:inventoryexpress.supplier.form.tag.description",
            Icon = new PropertyIcon(TypeIcon.Tag)
        };

        /// <summary>
        /// Bestimmt, ob das Formular zum Bearbeiten oder zum Neuanlegen verwendet werden soll.
        /// </summary>
        public bool Edit { get; set; } = false;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormularSupplier(IPage page, string id = null)
            : base(id)
        {
            Name = "supplier";
            EnableCancelButton = false;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Three, PropertySpacing.Space.None, PropertySpacing.Space.None);
            BackgroundColor = LayoutSchema.FormularBackground;
            Layout = TypeLayoutFormular.Horizontal;

            var group = new ControlFormularItemGroupColumnVertical() { Distribution = new int[] { 33 } };
            group.Items.Add(Zip);
            group.Items.Add(Place);

            Add(SupplierName);
            Add(Description);
            Add(Address);
            Add(new ControlFormularItemInputGroup(null, group));

            if (!Edit)
            {
                Add(Image);
            }

            Add(Tag);
        }
    }
}
