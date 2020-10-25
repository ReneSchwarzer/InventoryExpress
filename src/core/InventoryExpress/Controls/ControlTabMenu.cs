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
                Text = "Inventar",
                Uri = Page.Uri.Root,
                Active = Page is PageInventories || Page is PageInventoryAdd ? TypeActive.Active : TypeActive.None,
                Icon = new PropertyIcon(TypeIcon.Home)
            });
            
            Items.Add(new ControlLink(Page)
            {
                Text = "Standorte",
                Uri = Page.Uri.Root.Append("locations"),
                Active = Page is ILocation ? TypeActive.Active : TypeActive.None,
                Icon = new PropertyIcon(TypeIcon.Map)
            });

            Items.Add(new ControlLink(Page)
            {
                Text = "Hersteller",
                Uri = Page.Uri.Root.Append("manufactors"),
                Active = Page is IManufactor ? TypeActive.Active : TypeActive.None,
                Icon = new PropertyIcon(TypeIcon.Industry)
            });

            Items.Add(new ControlLink(Page)
            {
                Text = "Lieferanten",
                Uri = Page.Uri.Root.Append("suppliers"),
                Active = Page is ISupplier ? TypeActive.Active : TypeActive.None,
                Icon = new PropertyIcon(TypeIcon.Truck)
            });

            Items.Add(new ControlLink(Page)
            {
                Text = "Sachkonten",
                Uri = Page.Uri.Root.Append("glaccounts"),
                Active = Page is IGLAccount ? TypeActive.Active : TypeActive.None,
                Icon = new PropertyIcon(TypeIcon.At)
            });

            Items.Add(new ControlLink(Page)
            {
                Text = "Kostenstellen",
                Uri = Page.Uri.Root.Append("costcenters"),
                Active = Page is ICostCenter ? TypeActive.Active : TypeActive.None,
                Icon = new PropertyIcon(TypeIcon.ShoppingBag)
            });
            
            Items.Add(new ControlLink(Page)
            {
                Text = "Vorlagen",
                Uri = Page.Uri.Root.Append("templates"),
                Active = Page is ITemplate ? TypeActive.Active : TypeActive.None,
                Icon = new PropertyIcon(TypeIcon.Book)
            });
        }
    }
}
