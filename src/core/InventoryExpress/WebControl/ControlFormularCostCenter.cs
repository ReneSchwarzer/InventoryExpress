using WebExpress.UI.WebControl;

namespace InventoryExpress.WebControl
{
    public class ControlFormularCostCenter : ControlFormular
    {
        /// <summary>
        /// Liefert oder setzt den Namen der Kostenstelle
        /// </summary>
        public ControlFormularItemInputTextBox CostCenterName { get; set; }

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
            Name = "costcenter";
            EnableCancelButton = false;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Three, PropertySpacing.Space.None, PropertySpacing.Space.None);
            Layout = TypeLayoutFormular.Horizontal;

            CostCenterName = new ControlFormularItemInputTextBox()
            {
                Name = "name",
                Label = "inventoryexpress.costcenter.form.name.label",
                Help = "inventoryexpress.costcenter.form.name.description",
                Icon = new PropertyIcon(TypeIcon.Font)
            };

            Description = new ControlFormularItemInputTextBox("note")
            {
                Name = "description",
                Label = "inventoryexpress.costcenter.form.description.label",
                Help = "inventoryexpress.costcenter.form.description.description",
                Format = TypesEditTextFormat.Wysiwyg,
                Icon = new PropertyIcon(TypeIcon.CommentAlt)
            };

            Image = new ControlFormularItemInputFile()
            {
                Name = "image",
                Label = "inventoryexpress.costcenter.form.image.label",
                Help = "inventoryexpress.costcenter.form.image.description",
                Icon = new PropertyIcon(TypeIcon.Image),
                AcceptFile = new string[] { "image/*" }
            };

            Tag = new ControlFormularItemInputTextBox()
            {
                Name = "tag",
                Label = "inventoryexpress.costcenter.form.tag.label",
                Help = "inventoryexpress.costcenter.form.tag.description",
                Icon = new PropertyIcon(TypeIcon.Tag)
            };

            Add(CostCenterName);
            Add(Description);
            Add(Image);
            Add(Tag);
        }
    }
}
