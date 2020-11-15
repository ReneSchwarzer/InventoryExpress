
using InventoryExpress.Model;
using System.Linq;
using WebExpress.UI.Controls;

namespace InventoryExpress.Controls
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
            Name = "inventory";
            EnableCancelButton = false;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Three, PropertySpacing.Space.None, PropertySpacing.Space.None);

            TemplateName = new ControlFormularItemInputTextBox()
            {
                Name = "name",
                Label = "Name",
                Help = "Der Name der Vorlage",
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

            UnusedAttributes = new ControlFormularItemInputComboBox()
            {
                Name = "unusedattributes",
                Label = "Attribute",
                Help = "Weitere Attribute"
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

            Add(TemplateName);
            Add(Tag);
            Add(Discription);
            Add(UnusedAttributes);

        }
    }
}
