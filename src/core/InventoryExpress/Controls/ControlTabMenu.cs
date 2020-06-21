using InventoryExpress.Pages;
using WebExpress.Pages;
using WebExpress.UI.Controls;

namespace InventoryExpress.Controls
{
    public class ControlTabMenu : ControlTab
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        public ControlTabMenu(IPage page)
            : base(page)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Layout = TypeLayoutTab.Pill;
            HorizontalAlignment = TypeHorizontalAlignmentTab.Center;

            Items.Add(new ControlLink(Page)
            {
                Text = "Home",
                Uri = Page.Uri.Root,
                Active = Page is PageDashboard ? TypeActive.Active : TypeActive.None,
                Icon = new PropertyIcon(TypeIcon.Home)
            });
            
            Items.Add(new ControlLink(Page)
            {
                Text = "Standorte",
                Uri = Page.Uri.Root.Append("locations"),
                Active = Page is PageLocations ? TypeActive.Active : TypeActive.None,
                Icon = new PropertyIcon(TypeIcon.Map)
            });

            Items.Add(new ControlLink(Page)
            {
                Text = "Hersteller",
                Uri = Page.Uri.Root.Append("manufactors"),
                Active = Page is PageManufactors ? TypeActive.Active : TypeActive.None,
                Icon = new PropertyIcon(TypeIcon.Industry)
            });

            Items.Add(new ControlLink(Page)
            {
                Text = "Lieferanten",
                Uri = Page.Uri.Root.Append("suppliers"),
                Active = Page is PageSuppliers ? TypeActive.Active : TypeActive.None,
                Icon = new PropertyIcon(TypeIcon.Truck)
            });

            Items.Add(new ControlLink(Page)
            {
                Text = "Sachkonto",
                Uri = Page.Uri.Root.Append("glaccount"),
                Active = Page is PageDashboard ? TypeActive.Active : TypeActive.None,
                Icon = new PropertyIcon(TypeIcon.At)
            });

            Items.Add(new ControlLink(Page)
            {
                Text = "Kostenstelle",
                Uri = Page.Uri.Root.Append("costcenter"),
                Active = Page is PageDashboard ? TypeActive.Active : TypeActive.None,
                Icon = new PropertyIcon(TypeIcon.ShoppingBag)
            });
        }
    }
}
