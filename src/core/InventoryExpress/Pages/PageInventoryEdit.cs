using InventoryExpress.Controls;
using InventoryExpress.Model;
using System;
using System.Linq;

namespace InventoryExpress.Pages
{
    public class PageInventoryEdit : PageBase, IPageInventory
    {
        /// <summary>
        /// Formular
        /// </summary>
        private ControlFormularInventory form;


        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageInventoryEdit()
            : base("inventoryexpress.Inventory.edit.label")
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Init()
        {
            base.Init();

            form = new ControlFormularInventory()
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

            Content.Content.Add(form);
        }
    }
}
