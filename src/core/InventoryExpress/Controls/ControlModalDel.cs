using System;
using System.Collections.Generic;
using System.Text;
using WebExpress.Pages;
using WebExpress.UI.Controls;

namespace InventoryExpress.Controls
{
    public class ControlModalDel : ControlModal
    {
        /// <summary>
        /// Liefert oder setzt die Botschaft
        /// </summary>
        public string Message { get; set; } = "Möchten Sie das Element wirklich löschen?";
        
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlModalDel()
            : base("modal_del")
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Header = "Löschen";

            Content.Add(new ControlText()
            {
                Text = Message
            });

            Content.Add(new ControlButton()
            {
                Text = "Löschen",
                Icon = new PropertyIcon(TypeIcon.TrashAlt),
                Margin = new PropertySpacingMargin(PropertySpacing.Space.One),
                Color = new PropertyColorButton(TypeColorButton.Danger),
                OnClick = "window.location.href = '/del'"
            });
        }
    }
}
