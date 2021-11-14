using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebPage;

namespace InventoryExpress.WebControl
{
    public class ControlFormularTemplate : ControlFormular
    {
        /// <summary>
        /// Liefert den Namen der Vorlage
        /// </summary>
        public ControlFormularItemInputTextBox TemplateName { get; } = new ControlFormularItemInputTextBox()
        {
            Name = "name",
            Label = "inventoryexpress:inventoryexpress.template.form.name.label",
            Help = "inventoryexpress:inventoryexpress.template.form.name.description",
            Icon = new PropertyIcon(TypeIcon.Font)
        };

        /// <summary>
        /// Liefert die ungenutzten Attribute
        /// </summary>
        public ControlFormularItemInputMoveSelector Attributes { get; } = new ControlFormularItemInputMoveSelector("attributes")
        {
            Name = "attributes",
            Label = "inventoryexpress:inventoryexpress.template.form.unused.label",
            Help = "inventoryexpress:inventoryexpress.template.form.unused.description",
            Icon = new PropertyIcon(TypeIcon.Cubes)
        };

        /// <summary>
        /// Liefert die Beschreibung
        /// </summary>
        public ControlFormularItemInputTextBox Description { get; } = new ControlFormularItemInputTextBox("note")
        {
            Name = "description",
            Label = "inventoryexpress:inventoryexpress.template.form.description.label",
            Help = "inventoryexpress:inventoryexpress.template.form.description.description",
            Format = TypesEditTextFormat.Wysiwyg,
            Icon = new PropertyIcon(TypeIcon.CommentAlt)
        };

        /// <summary>
        /// Liefert das Bild
        /// </summary>
        public ControlFormularItemInputFile Image { get; } = new ControlFormularItemInputFile()
        {
            Name = "image",
            Label = "inventoryexpress:inventoryexpress.template.form.image.label",
            Help = "inventoryexpress:inventoryexpress.template.form.image.description",
            Icon = new PropertyIcon(TypeIcon.Image),
            AcceptFile = new string[] { "image/*" }
        };

        /// <summary>
        /// Liefert die Schlagwörter
        /// </summary>
        public ControlFormularItemInputTag Tag { get; } = new ControlFormularItemInputTag("tags")
        {
            Name = "tag",
            Label = "inventoryexpress:inventoryexpress.template.form.tag.label",
            Help = "inventoryexpress:inventoryexpress.template.form.tag.description",
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
        public ControlFormularTemplate(string id = null)
            : base(id)
        {
            Name = "template";
            EnableCancelButton = false;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Three, PropertySpacing.Space.None, PropertySpacing.Space.None);
            BackgroundColor = LayoutSchema.FormularBackground;
            Layout = TypeLayoutFormular.Horizontal;

            Add(TemplateName);
            Add(Attributes);
            Add(Description);

            if (!Edit)
            {
                Add(Image);
            }

            Add(Tag);
        }
    }
}
