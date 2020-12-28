using WebExpress.Html;
using WebExpress.UI.WebControl;
using WebExpress.WebResource;

namespace InventoryExpress.WebControl
{
    public class ControlFormularManufacturer : ControlFormular
    {
        /// <summary>
        /// Liefert oder setzt den Namen des Herstellers
        /// </summary>
        public ControlFormularItemInputTextBox ManufacturerName { get; set; }

        /// <summary>
        /// Liefert oder setzt die Beschreibung
        /// </summary>
        public ControlFormularItemInputTextBox Description { get; set; }

        /// <summary>
        /// Liefert oder setzt die Aaddresse
        /// </summary>
        public ControlFormularItemInputTextBox Address { get; set; }

        /// <summary>
        /// Liefert oder setzt die Postleitzahl
        /// </summary>
        public ControlFormularItemInputTextBox Zip { get; set; }

        /// <summary>
        /// Liefert oder setzt den Ort
        /// </summary>
        public ControlFormularItemInputTextBox Place { get; set; }

        /// <summary>
        /// Liefert oder setzt das Bild
        /// </summary>
        public ControlFormularItemInputFile Image { get; set; }

        /// <summary>
        /// Liefert oder setzt die Schlagwörter
        /// </summary>
        public ControlFormularItemInputTag Tag { get; set; }

        /// <summary>
        /// Bestimmt, ob das Formular zum Bearbeiten oder zum Neuanlegen verwendet werden soll.
        /// </summary>
        public bool Edit { get; set; } = false;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormularManufacturer(IPage page, string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Name = "manufacturer";
            EnableCancelButton = false;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Three);
            Layout = TypeLayoutFormular.Horizontal;

            ManufacturerName = new ControlFormularItemInputTextBox()
            {
                Name = "name",
                Label = "inventoryexpress.manufacturer.form.name.label",
                Help = "inventoryexpress.manufacturer.form.name.description",
                Icon = new PropertyIcon(TypeIcon.Font)
            };

            Description = new ControlFormularItemInputTextBox("note")
            {
                Name = "description",
                Label = "inventoryexpress.manufacturer.form.description.label",
                Help = "inventoryexpress.manufacturer.form.description.description",
                Format = TypesEditTextFormat.Wysiwyg,
                Icon = new PropertyIcon(TypeIcon.CommentAlt),
                Rows = 10
            };

            Address = new ControlFormularItemInputTextBox("adress")
            {
                Name = "adress",
                Label = "inventoryexpress.manufacturer.form.adress.label",
                Help = "inventoryexpress.manufacturer.form.adress.description",
                Icon = new PropertyIcon(TypeIcon.Home)
            };

            Zip = new ControlFormularItemInputTextBox("zip")
            {
                Name = "zip",
                Label = "inventoryexpress.manufacturer.form.zip.label",
                Help = "inventoryexpress.manufacturer.form.zip.description",
                Icon = new PropertyIcon(TypeIcon.MapMarker)
            };

            Place = new ControlFormularItemInputTextBox("place")
            {
                Name = "place",
                Label = "inventoryexpress.manufacturer.form.place.label",
                Help = "inventoryexpress.manufacturer.form.place.description",
                Icon = new PropertyIcon(TypeIcon.City)
            };

            Image = new ControlFormularItemInputFile()
            {
                Name = "image",
                Label = "inventoryexpress.manufacturer.form.image.label",
                Help = "inventoryexpress.manufacturer.form.image.description",
                Icon = new PropertyIcon(TypeIcon.Image),
                AcceptFile = new string[] { "image/*" }
            };

            Tag = new ControlFormularItemInputTag("tags")
            {
                Name = "tag",
                Label = "inventoryexpress.manufacturer.form.tag.label",
                Help = "inventoryexpress.manufacturer.form.tag.description",
                Icon = new PropertyIcon(TypeIcon.Tag)
            };
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
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

            return base.Render(context);
        }
    }
}
