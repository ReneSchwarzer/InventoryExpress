﻿using InventoryExpress.Controls;
using InventoryExpress.Model;
using WebExpress.UI.Controls;

namespace InventoryExpress.Pages
{
    public class PageLocations : PageBase, IPageLocation
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageLocations()
            : base("inventoryexpress.locations.label")
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Init()
        {
            base.Init();

            // ToolBar.Add(new ControlToolBarItemButton()
            // {
            //     Icon = new PropertyIcon(TypeIcon.Plus),
            //     Text = "Hinzufügen",
            //     Title = "Neu",
            //     Uri = Uri.Append("add"),
            //     TextColor = new PropertyColorText(TypeColorText.White)
            // },
            //new ControlToolBarItemButton()
            //{
            //    Icon = new PropertyIcon(TypeIcon.Print),
            //    Uri = Uri.Append("print"),
            //    Title = "Drucken",
            //    Size = new PropertySizeText(TypeSizeText.Default),
            //    TextColor = new PropertyColorText(TypeColorText.White)
            //});

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

            var grid = new ControlPanelGrid() { Fluid = TypePanelContainer.Fluid };
            int i = 0;

            foreach (var location in ViewModel.Instance.Locations)
            {
                var card = new ControlCardLocation()
                {
                    Location = location
                };

                grid.Content.Add(card);
            }

            Content.Content.Add(grid);
        }
    }
}
