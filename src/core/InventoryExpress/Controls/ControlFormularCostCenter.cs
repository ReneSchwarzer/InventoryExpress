using WebExpress.Pages;
using WebExpress.UI.Controls;

namespace InventoryExpress.Controls
{
    public class ControlFormularCostCenter : ControlFormular
    {
        /// <summary>
        /// Liefert oder setzt den Namen der Kostenstelle
        /// </summary>
        public ControlFormularItemInputTextBox CostCenterName { get; set; }

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
        public ControlFormularCostCenter(string id = null)
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

            CostCenterName = new ControlFormularItemInputTextBox()
            {
                Name = "name",
                Label = "Name",
                Help = "Der Name der Kostenstelle",
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

            Add(CostCenterName);
            Add(Tag);
            Add(Discription);

        }
    }
}
