
using InventoryExpress.Model;
using System.Linq;
using WebExpress.Html;
using WebExpress.UI.WebControl;

namespace InventoryExpress.WebControl
{
    public class ControlFormularTemplate : ControlFormular
    {
        /// <summary>
        /// Liefert oder setzt den Namen der Vorlage
        /// </summary>
        public ControlFormularItemInputTextBox TemplateName { get; set; }

        /// <summary>
        /// Liefert oder setzt die ungenutzten Attribute
        /// </summary>
        private ControlFormularItemInputComboBox UnusedAttributes { get; set; }

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
        public ControlFormularItemInputTag Tag { get; set; }

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
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Name = "template";
            EnableCancelButton = false;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Three, PropertySpacing.Space.None, PropertySpacing.Space.None);
            Layout = TypeLayoutFormular.Horizontal;

            TemplateName = new ControlFormularItemInputTextBox()
            {
                Name = "name",
                Label = "inventoryexpress.template.form.name.label",
                Help = "inventoryexpress.template.form.name.description",
                Icon = new PropertyIcon(TypeIcon.Font)
            };

            UnusedAttributes = new ControlFormularItemInputComboBox()
            {
                Name = "unusedattributes",
                Label = "inventoryexpress.template.form.unused.label",
                Help = "inventoryexpress.template.form.unused.description"
            };

            UnusedAttributes.Items.Add(new ControlFormularItemInputComboBoxItem()
            {
                Text = string.Empty,
                Value = null
            });

            UnusedAttributes.Items.AddRange(ViewModel.Instance.Attributes.Select(x => new ControlFormularItemInputComboBoxItem()
            {
                Text = x.Name,
                Value = x.ID.ToString()
            }));

            Description = new ControlFormularItemInputTextBox("note")
            {
                Name = "description",
                Label = "inventoryexpress.template.form.description.label",
                Help = "inventoryexpress.template.form.description.description",
                Format = TypesEditTextFormat.Wysiwyg,
                Icon = new PropertyIcon(TypeIcon.CommentAlt)
            };

            Image = new ControlFormularItemInputFile()
            {
                Name = "image",
                Label = "inventoryexpress.template.form.image.label",
                Help = "inventoryexpress.template.form.image.description",
                Icon = new PropertyIcon(TypeIcon.Image),
                AcceptFile = new string[] { "image/*" }
            };

            Tag = new ControlFormularItemInputTag("tags")
            {
                Name = "tag",
                Label = "inventoryexpress.template.form.tag.label",
                Help = "inventoryexpress.template.form.tag.description",
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
            Add(TemplateName);
            Add(UnusedAttributes);
            Add(Description);

            if (!Edit)
            {
                Add(Image);
            }

            Add(Tag);

            return base.Render(context);
        }
    }
}
