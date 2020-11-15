using WebExpress.UI.Controls;

namespace InventoryExpress.Controls
{
    public class ControlFormularLedgerAccount : ControlFormular
    {
        /// <summary>
        /// Liefert oder setzt den Namen des Sachkontos
        /// </summary>
        public ControlFormularItemInputTextBox GLAccountName { get; set; }

        /// <summary>
        /// Liefert oder setzt die Schlagwörter
        /// </summary>
        public ControlFormularItemInputTextBox Tag { get; set; }

        /// <summary>
        /// Liefert oder setzt die Beschreibung
        /// </summary>
        public ControlFormularItemInputTextBox Discription { get; set; }

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
            Name = "inventory";
            EnableCancelButton = false;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Three, PropertySpacing.Space.None, PropertySpacing.Space.None);

            GLAccountName = new ControlFormularItemInputTextBox()
            {
                Name = "name",
                Label = "Name",
                Help = "Der Name des Sachkontos",
                Icon = new PropertyIcon(TypeIcon.Font)
            };

            Tag = new ControlFormularItemInputTextBox()
            {
                Name = "tag",
                Label = "Schlagwörter",
                Help = "",
                Icon = new PropertyIcon(TypeIcon.Tag)
            };

            Discription = new ControlFormularItemInputTextBox()
            {
                Name = "memo",
                Label = "Beschreibung",
                Help = "",
                Format = TypesEditTextFormat.Wysiwyg,
                Icon = new PropertyIcon(TypeIcon.CommentAlt)
            };

            Add(GLAccountName);
            Add(Tag);
            Add(Discription);

        }
    }
}
