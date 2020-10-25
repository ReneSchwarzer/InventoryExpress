using InventoryExpress.Controls;
using InventoryExpress.Model;
using System;
using System.Collections.Generic;
using System.Text;
using WebExpress.UI.Controls;

namespace InventoryExpress.Pages
{
    public class PageInventories : PageBase
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageInventories()
            : base("Überblick")
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
                Uri = Uri.Root.Append("add"),
                TextColor = new PropertyColorText(TypeColorText.White)
            },
            new ControlToolBarItemButton(this)
            {
                Icon = new PropertyIcon(TypeIcon.Print),
                Uri = Uri.Root.Append("print"),
                Title = "Drucken",
                Size = new PropertySizeText(TypeSizeText.Default),
                TextColor = new PropertyColorText(TypeColorText.White)
            },
            new ControlToolBarItemButton(this)
            {
                Icon = new PropertyIcon(TypeIcon.Download),
                Uri = Uri.Root.Append("export"),
                Title = "Exportieren",
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

            foreach (var inventory in ViewModel.Instance.Inventories)
            {
                var card = new ControlCardInventory(this) 
                { 
                     Inventory = inventory
                };

                grid.Add(i++, card);
            }

            Main.Content.Add(grid);
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
