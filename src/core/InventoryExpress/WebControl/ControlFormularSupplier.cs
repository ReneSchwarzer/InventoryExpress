using WebExpress.WebResource;
using WebExpress.UI.WebControl;

namespace InventoryExpress.WebControl
{
    public class ControlFormularSupplier : ControlFormular
    {
        /// <summary>
        /// Liefert oder setzt den Namen des Lieferanten
        /// </summary>
        public ControlFormularItemInputTextBox SupplierName { get; set; }

        /// <summary>
        /// Liefert oder setzt die Beschreibung
        /// </summary>
        public ControlFormularItemInputTextBox Description { get; set; }

        /// <summary>
        /// Liefert oder setzt das Bild
        /// </summary>
        public ControlFormularItemInputFile Image { get; set; }

        /// <summary>
        /// Liefert oder setzt die Schlagwörter
        /// </summary>
        public ControlFormularItemInputTextBox Tag { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormularSupplier(IPage page, string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Name = "supplier";
            EnableCancelButton = false;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Three, PropertySpacing.Space.None, PropertySpacing.Space.None);
            Layout = TypeLayoutFormular.Horizontal;

            SupplierName = new ControlFormularItemInputTextBox()
            {
                Name = "name",
                Label = "inventoryexpress.supplier.form.name.label",
                Help = "inventoryexpress.supplier.form.name.description",
                Icon = new PropertyIcon(TypeIcon.Font)
            };

            Description = new ControlFormularItemInputTextBox("note")
            {
                Name = "description",
                Label = "inventoryexpress.supplier.form.description.label",
                Help = "inventoryexpress.supplier.form.description.description",
                Format = TypesEditTextFormat.Wysiwyg,
                Icon = new PropertyIcon(TypeIcon.CommentAlt)
            };

            Image = new ControlFormularItemInputFile()
            {
                Name = "image",
                Label = "inventoryexpress.supplier.form.image.label",
                Help = "inventoryexpress.supplier.form.image.description",
                Icon = new PropertyIcon(TypeIcon.Image),
                AcceptFile = new string[] { "image/*" }
            };

            Tag = new ControlFormularItemInputTextBox()
            {
                Name = "tag",
                Label = "inventoryexpress.supplier.form.tag.label",
                Help = "inventoryexpress.supplier.form.tag.description",
                Icon = new PropertyIcon(TypeIcon.Tag)
            };

            Add(SupplierName);
            Add(Description);
            Add(Image);
            Add(Tag);

        }
    }
}
