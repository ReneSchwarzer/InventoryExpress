﻿
using InventoryExpress.Model;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Pages;
using WebExpress.UI.Controls;

namespace InventoryExpress.Controls
{
    public class ControlFormularTemplate : ControlPanelFormular
    {
        /// <summary>
        /// Liefert oder setzt den Namen des Herstellers
        /// </summary>
        public ControlFormularItemTextBox ManufactorName { get; set; }

        /// <summary>
        /// Liefert oder setzt die Schlagwörter
        /// </summary>
        public ControlFormularItemTextBox Tag { get; set; }

        /// <summary>
        /// Liefert oder setzt die Beschreibung
        /// </summary>
        public ControlFormularItemTextBox Discription { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlFormularTemplate(IPage page, string id = null)
            : base(page, id)
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

            ManufactorName = new ControlFormularItemTextBox(this)
            {
                Name = "name",
                Label = "Name",
                Help = "Der Name des Herstellers",
                Icon = new PropertyIcon(TypeIcon.Font)
            };

            Tag = new ControlFormularItemTextBox(this)
            {
                Name = "tag",
                Label = "Schlagwörter",
                Help = "",
                Icon = new PropertyIcon(TypeIcon.Tag)
            };

            Discription = new ControlFormularItemTextBox(this)
            {
                Name = "memo",
                Label = "Beschreibung",
                Help = "",
                Format = TypesEditTextFormat.Wysiwyg,
                Icon = new PropertyIcon(TypeIcon.CommentAlt)
            };

            Add(ManufactorName);
            Add(Tag);
            Add(Discription);

        }
    }
}
