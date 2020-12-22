using WebExpress.UI.WebControl;

namespace InventoryExpress.WebControl
{
    public class ControlFormularLocation : ControlFormular
    {
        /// <summary>
        /// Liefert oder setzt den Namen des Standortes
        /// </summary>
        public ControlFormularItemInputTextBox LocationName { get; set; }

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
        public ControlFormularLocation(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Name = "location";
            EnableCancelButton = false;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Three, PropertySpacing.Space.None, PropertySpacing.Space.None);
            Layout = TypeLayoutFormular.Horizontal;

            LocationName = new ControlFormularItemInputTextBox()
            {
                Name = "name",
                Label = "inventoryexpress.location.form.name.label",
                Help = "inventoryexpress.location.form.name.description",
                Icon = new PropertyIcon(TypeIcon.Font)
            };
            
            Description = new ControlFormularItemInputTextBox("note")
            {
                Name = "description",
                Label = "inventoryexpress.location.form.description.label",
                Help = "inventoryexpress.location.form.description.description",
                Format = TypesEditTextFormat.Wysiwyg,
                Icon = new PropertyIcon(TypeIcon.CommentAlt)
            };

            Image = new ControlFormularItemInputFile()
            {
                Name = "image",
                Label = "inventoryexpress.location.form.image.label",
                Help = "inventoryexpress.location.form.image.description",
                Icon = new PropertyIcon(TypeIcon.Image),
                AcceptFile = new string[] { "image/*" }
            };

            Tag = new ControlFormularItemInputTextBox()
            {
                Name = "tag",
                Label = "inventoryexpress.location.form.tag.label",
                Help = "inventoryexpress.location.form.tag.description",
                Icon = new PropertyIcon(TypeIcon.Tag)
            };

            Add(LocationName);
            Add(Description);
            Add(Image);
            Add(Tag);
        }
    }
}
