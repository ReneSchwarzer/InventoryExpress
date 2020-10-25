using InventoryExpress.Controls;
using InventoryExpress.Model;
using System;
using System.Linq;

namespace InventoryExpress.Pages
{
    public class PageInventoryEdit : PageBase
    {
        /// <summary>
        /// Formular
        /// </summary>
        private ControlFormularInventory form;


        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageInventoryEdit()
            : base("Inventargegenstand bearbeiten")
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Init()
        {
            base.Init();

            form = new ControlFormularInventory(this)
            {
                RedirectUrl = Uri.Take(-1)
            };
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();

            var id = Convert.ToInt32(GetParam("id"));
            var inventory = ViewModel.Instance.Inventories.Where(x => x.ID == id).FirstOrDefault();

            Main.Content.Add(form);
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
