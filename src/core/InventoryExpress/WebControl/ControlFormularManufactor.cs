using WebExpress.WebResource;
using WebExpress.UI.WebControl;

namespace InventoryExpress.WebControl
{
    public class ControlFormularManufactor : ControlFormular
    {
        /// <summary>
        /// Liefert oder setzt den Namen des Herstellers
        /// </summary>
        public ControlFormularItemInputTextBox ManufactorName { get; set; }

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
        public ControlFormularManufactor(IPage page, string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Name = "manufactor";
            EnableCancelButton = false;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Three, PropertySpacing.Space.None, PropertySpacing.Space.None);
            Layout = TypeLayoutFormular.Horizontal;

            ManufactorName = new ControlFormularItemInputTextBox()
            {
                Name = "name",
                Label = "inventoryexpress.manufactor.form.name.label",
                Help = "inventoryexpress.manufactor.form.name.description",
                Icon = new PropertyIcon(TypeIcon.Font)
            };

            Description = new ControlFormularItemInputTextBox("note")
            {
                Name = "description",
                Label = "inventoryexpress.manufactor.form.description.label",
                Help = "inventoryexpress.manufactor.form.description.description",
                Format = TypesEditTextFormat.Wysiwyg,
                Icon = new PropertyIcon(TypeIcon.CommentAlt)
            };

            Image = new ControlFormularItemInputFile()
            {
                Name = "image",
                Label = "inventoryexpress.manufactor.form.image.label",
                Help = "inventoryexpress.manufactor.form.image.description",
                Icon = new PropertyIcon(TypeIcon.Image),
                AcceptFile = new string[] { "image/*" }
            };

            Tag = new ControlFormularItemInputTextBox()
            {
                Name = "tag",
                Label = "inventoryexpress.manufactor.form.tag.label",
                Help = "inventoryexpress.manufactor.form.tag.description",
                Icon = new PropertyIcon(TypeIcon.Tag)
            };

            Add(ManufactorName);
            Add(Description);
            Add(Image);
            Add(Tag);

        }
    }
}
