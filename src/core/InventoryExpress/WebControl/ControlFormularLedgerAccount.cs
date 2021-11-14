using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebPage;

namespace InventoryExpress.WebControl
{
    public class ControlFormularLedgerAccount : ControlFormular
    {
        /// <summary>
        /// Liefert den Namen des Sachkontos
        /// </summary>
        public ControlFormularItemInputTextBox LedgerAccountName { get; } = new ControlFormularItemInputTextBox()
        {
            Name = "name",
            Label = "inventoryexpress:inventoryexpress.ledgeraccount.form.name.label",
            Help = "inventoryexpress:inventoryexpress.ledgeraccount.form.name.description",
            Icon = new PropertyIcon(TypeIcon.Font)
        };

        /// <summary>
        /// Liefert die Beschreibung
        /// </summary>
        public ControlFormularItemInputTextBox Description { get; } = new ControlFormularItemInputTextBox("note")
        {
            Name = "description",
            Label = "inventoryexpress:inventoryexpress.ledgeraccount.form.description.label",
            Help = "inventoryexpress:inventoryexpress.ledgeraccount.form.description.description",
            Format = TypesEditTextFormat.Wysiwyg,
            Icon = new PropertyIcon(TypeIcon.CommentAlt)
        };

        /// <summary>
        /// Liefert das Bild
        /// </summary>
        public ControlFormularItemInputFile Image { get; } = new ControlFormularItemInputFile()
        {
            Name = "image",
            Label = "inventoryexpress:inventoryexpress.ledgeraccount.form.image.label",
            Help = "inventoryexpress:inventoryexpress.ledgeraccount.form.image.description",
            Icon = new PropertyIcon(TypeIcon.Image),
            AcceptFile = new string[] { "image/*" }
        };

        /// <summary>
        /// Liefert die Schlagwörter
        /// </summary>
        public ControlFormularItemInputTag Tag { get; } = new ControlFormularItemInputTag("tags")
        {
            Name = "tag",
            Label = "inventoryexpress:inventoryexpress.ledgeraccount.form.tag.label",
            Help = "inventoryexpress:inventoryexpress.ledgeraccount.form.tag.description",
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
        public ControlFormularLedgerAccount(string id = null)
            : base(id)
        {
            Name = "ledgeraccount";
            EnableCancelButton = false;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Three, PropertySpacing.Space.None, PropertySpacing.Space.None);
            BackgroundColor = LayoutSchema.FormularBackground;
            Layout = TypeLayoutFormular.Horizontal;

            Add(LedgerAccountName);
            Add(Description);

            if (!Edit)
            {
                Add(Image);
            }

            Add(Tag);
        }
    }
}
