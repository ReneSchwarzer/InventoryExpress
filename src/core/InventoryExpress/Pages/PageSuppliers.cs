using InventoryExpress.Controls;
using InventoryExpress.Model;
using System;
using System.Collections.Generic;
using System.Text;
using WebExpress.UI.Controls;

namespace InventoryExpress.Pages
{
    public class PageSuppliers : PageBase, ISupplier
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

            ToolBar.Add(new ControlToolBarItemButton(this)
            {
                Icon = new PropertyIcon(TypeIcon.Plus),
                Text = "Hinzufügen",
                Title = "Neu",
                Uri = Uri.Append("add"),
                TextColor = new PropertyColorText(TypeColorText.White)
            },
            new ControlToolBarItemButton(this)
            {
                Icon = new PropertyIcon(TypeIcon.Print),
                Uri = Uri.Append("print"),
                Title = "Drucken",
                Size = new PropertySizeText(TypeSizeText.Default),
                TextColor = new PropertyColorText(TypeColorText.White)
            });
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();

            var grid = new ControlGrid(this) { Fluid = false };
            int i = 0;

            foreach (var supplier in DB.Instance.Suppliers)
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
