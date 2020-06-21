using InventoryExpress.Controls;
using InventoryExpress.Model;
using System;
using System.Collections.Generic;
using System.Text;
using WebExpress.UI.Controls;

namespace InventoryExpress.Pages
{
    public class PageManufactors : PageBase
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageManufactors()
            : base("Hersteller")
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

            foreach (var manufactor in ViewModel.Instance.Manufacturers)
            {
                var card = new ControlManufactorsCard(this)
                {
                    Manufactur = manufactor
                };

                grid.Add(i++, card);
            }

            Main.Content.Add(grid);
        }
    }
}
