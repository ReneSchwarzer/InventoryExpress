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
        {
            Init(page);
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init(IPage page)
        {
            Layout = TypeLayoutTab.Pill;
            HorizontalAlignment = TypeHorizontalAlignmentTab.Center;

            Items.Add(new ControlLink()
            {
                Text = "Inventar",
                Uri = page.Uri.Root,
                Active = page is PageInventories || page is PageInventoryAdd ? TypeActive.Active : TypeActive.None,
                Icon = new PropertyIcon(TypeIcon.Home)
            });
            
            Items.Add(new ControlLink()
            {
                Text = "Standorte",
                Uri = page.Uri.Root.Append("locations"),
                Active = page is ILocation ? TypeActive.Active : TypeActive.None,
                Icon = new PropertyIcon(TypeIcon.Map)
            });

            Items.Add(new ControlLink()
            {
                Text = "Hersteller",
                Uri = page.Uri.Root.Append("manufactors"),
                Active = page is IManufactor ? TypeActive.Active : TypeActive.None,
                Icon = new PropertyIcon(TypeIcon.Industry)
            });

            Items.Add(new ControlLink()
            {
                Text = "Lieferanten",
                Uri = page.Uri.Root.Append("suppliers"),
                Active = page is ISupplier ? TypeActive.Active : TypeActive.None,
                Icon = new PropertyIcon(TypeIcon.Truck)
            });

            Items.Add(new ControlLink()
            {
                Text = "Sachkonten",
                Uri = page.Uri.Root.Append("glaccounts"),
                Active = page is IGLAccount ? TypeActive.Active : TypeActive.None,
                Icon = new PropertyIcon(TypeIcon.At)
            });

            Items.Add(new ControlLink()
            {
                Text = "Kostenstellen",
                Uri = page.Uri.Root.Append("costcenters"),
                Active = page is ICostCenter ? TypeActive.Active : TypeActive.None,
                Icon = new PropertyIcon(TypeIcon.ShoppingBag)
            });
            
            Items.Add(new ControlLink()
            {
                Text = "Vorlagen",
                Uri = page.Uri.Root.Append("templates"),
                Active = page is ITemplate ? TypeActive.Active : TypeActive.None,
                Icon = new PropertyIcon(TypeIcon.Book)
            });
        }
    }
}
