
using InventoryExpress.Model;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Pages;
using WebExpress.UI.Controls;

namespace InventoryExpress.Controls
{
    public class ControlFormularLocation : ControlFormular
    {
        /// <summary>
        /// Liefert oder setzt den Namen des Standortes
        /// </summary>
        public ControlFormularItemInputTextBox LocationName { get; set; }

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
            Name = "inventory";
            EnableCancelButton = false;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Three, PropertySpacing.Space.None, PropertySpacing.Space.None);

            LocationName = new ControlFormularItemInputTextBox()
            {
                Name = "name",
                Label = "Name",
                Help = "Der Name des Herstellers",
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

            Add(LocationName);
            Add(Tag);
            Add(Discription);

        }
    }
}
