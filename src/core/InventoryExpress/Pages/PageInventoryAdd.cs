using InventoryExpress.Controls;
using InventoryExpress.Model;
using System;
using System.Collections.Generic;
using System.Text;
using WebExpress.UI.Controls;

namespace InventoryExpress.Pages
{
    public class PageInventoryAdd : PageBase
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageInventoryAdd()
            : base("Inventargegenstand hinzufügen")
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Init()
        {
            base.Init();
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();

            Main.Content.Add(new ControlFormularInventory(this)
            { 
            });
        }

        /// <summary>
        /// In String konvertieren
        /// </summary>
        /// <returns>Das Objekt als String</returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
