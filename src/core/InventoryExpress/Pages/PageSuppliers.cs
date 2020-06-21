using InventoryExpress.Controls;
using InventoryExpress.Model;
using System;
using System.Collections.Generic;
using System.Text;
using WebExpress.UI.Controls;

namespace InventoryExpress.Pages
{
    public class PageSuppliers : PageBase
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageSuppliers()
            : base("Lieferanten")
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

            var grid = new ControlGrid(this) { Fluid = false };
            int i = 0;

            foreach (var supplier in ViewModel.Instance.Suppliers)
            {
                var card = new ControlSuppliersCard(this)
                {
                    Supplier = supplier
                };

                grid.Add(i++, card);
            }

            Main.Content.Add(grid);
        }
    }
}
