using InventoryExpress.Controls;
using InventoryExpress.Model;
using System;
using System.Collections.Generic;
using System.Text;
using WebExpress.UI.Controls;

namespace InventoryExpress.Pages
{
    public class PageLocations : PageBase
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageLocations()
            : base("Standorte")
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Init()
        {
            base.Init();
            //var menu = new ControlMenu(this, null, 
            //    new ControlLink(this) { Text = "Home", Icon = "fas fa-map", Url = GetUrl(0) },
            //    new ControlDropdownMenuDivider(this) { }
                
            //)
            //{
            //    Icon = "fas fa-bars",
            //    Text = "",
            //    Layout = TypesLayoutButton.Primary
            //};

            //var add = new ControlButtonLink(this)
            //{
            //    Icon = "fas fa-plus",
            //    Text = "Hinzufügen",
            //    Url = GetUrl(0, "add"),
            //    Layout = TypesLayoutButton.Primary
            //};

            //ToolBar.Add(menu);
            //ToolBar.Add(add);
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();

            var grid = new ControlGrid(this) { Fluid = false };
            int i = 0;

            foreach (var location in ViewModel.Instance.Locations)
            {
                var card = new ControlLocationCard(this)
                { 
                     Location = location
                };

                grid.Add(i++, card);
            }

            Main.Content.Add(grid);
        }
    }
}
