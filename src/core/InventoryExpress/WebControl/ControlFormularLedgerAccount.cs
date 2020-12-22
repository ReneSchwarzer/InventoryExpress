using WebExpress.UI.WebControl;

namespace InventoryExpress.WebControl
{
    public class ControlFormularLedgerAccount : ControlFormular
    {
        /// <summary>
        /// Liefert oder setzt den Namen des Sachkontos
        /// </summary>
        public ControlFormularItemInputTextBox GLAccountName { get; set; }

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
        public ControlFormularLedgerAccount(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Name = "ledgeraccount";
            EnableCancelButton = false;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Three, PropertySpacing.Space.None, PropertySpacing.Space.None);
            Layout = TypeLayoutFormular.Horizontal;

            GLAccountName = new ControlFormularItemInputTextBox()
            {
                Name = "name",
                Label = "inventoryexpress.ledgeraccount.form.name.label",
                Help = "inventoryexpress.ledgeraccount.form.name.description",
                Icon = new PropertyIcon(TypeIcon.Font)
            };

            Description = new ControlFormularItemInputTextBox("note")
            {
                Name = "description",
                Label = "inventoryexpress.ledgeraccount.form.description.label",
                Help = "inventoryexpress.ledgeraccount.form.description.description",
                Format = TypesEditTextFormat.Wysiwyg,
                Icon = new PropertyIcon(TypeIcon.CommentAlt)
            };

            Image = new ControlFormularItemInputFile()
            {
                Name = "image",
                Label = "inventoryexpress.ledgeraccount.form.image.label",
                Help = "inventoryexpress.ledgeraccount.form.image.description",
                Icon = new PropertyIcon(TypeIcon.Image),
                AcceptFile = new string[] { "image/*" }
            };

            Tag = new ControlFormularItemInputTextBox()
            {
                Name = "tag",
                Label = "inventoryexpress.ledgeraccount.form.tag.label",
                Help = "inventoryexpress.ledgeraccount.form.tag.description",
                Icon = new PropertyIcon(TypeIcon.Tag)
            };

            Add(GLAccountName);
            Add(Description);
            Add(Image);
            Add(Tag);

        }
    }
}
