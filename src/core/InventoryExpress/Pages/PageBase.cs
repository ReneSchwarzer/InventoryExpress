using InventoryExpress.Controls;
using InventoryExpress.Model;
using System;
using System.Collections.Generic;
using System.Text;
using WebExpress.Html;
using WebExpress.UI.Controls;
using WebExpress.UI.Pages;

namespace InventoryExpress.Pages
{
    public class PageBase : PageTemplateWebApp
    {       
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="title">Der Titel der Seite</param>
        public PageBase(string title)
            : base()
        {
            Title = "Inventory Express";

            if (!string.IsNullOrWhiteSpace(title))
            {
                Title += " - " + title;
            }

            Favicons.Add(new Favicon("/Assets/img/Favicon.png", TypeFavicon.PNG));
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Init()
        {
            base.Init();

            Head.Sticky = TypeSticky.Top;
            Head.Content.Add(HamburgerMenu);
            HamburgerMenu.HorizontalAlignment = TypeHorizontalAlignment.Left;
            HamburgerMenu.Image = Uri?.Root.Append("Assets/img/Logo.png");
            HamburgerMenu.Add(new ControlLink(this) { Text = "Home", Icon = new PropertyIcon(TypeIcon.Home), Uri = Uri.Root });
            HamburgerMenu.AddSeperator();
            HamburgerMenu.Add(new ControlLink(this) { Text = "Standorte", Icon = new PropertyIcon(TypeIcon.Map), Uri = Uri.Root.Append("locations") });
            HamburgerMenu.Add(new ControlLink(this) { Text = "Hersteller", Icon = new PropertyIcon(TypeIcon.Industry), Uri = Uri.Root.Append("manufactors") });
            HamburgerMenu.Add(new ControlLink(this) { Text = "Lieferanten", Icon = new PropertyIcon(TypeIcon.Truck), Uri = Uri.Root.Append("suppliers") });
            HamburgerMenu.Add(new ControlLink(this) { Text = "Sachkonto", Icon = new PropertyIcon(TypeIcon.At), Uri = Uri.Root.Append("glaccount") });
            HamburgerMenu.Add(new ControlLink(this) { Text = "Kostenstelle", Icon = new PropertyIcon(TypeIcon.ShoppingBag), Uri = Uri.Root.Append("costcenter") });

            if (this is PageDashboard)
            {
                HamburgerMenu.AddSeperator();
                HamburgerMenu.Add(new ControlLink(this) { Text = "Import", Icon = new PropertyIcon(TypeIcon.Upload), Uri = Uri.Root.Append("import") });
                HamburgerMenu.Add(new ControlLink(this) { Text = "Export", Icon = new PropertyIcon(TypeIcon.Download), Uri = Uri.Root.Append("export") });
                
            }
            HamburgerMenu.AddSeperator();
            HamburgerMenu.Add(new ControlLink(this) { Text = "Hilfe", Icon = new PropertyIcon(TypeIcon.InfoCircle), Uri = Uri.Root.Append("help") });

            // ToolBar
            ToolBar.BackgroundColor = new PropertyColorBackground(TypeColorBackground.Secondary);

            // SideBar
            SideBar = new ControlToolBar(this)
            {
                BackgroundColor = new PropertyColorBackground("#553322"),
                HorizontalAlignment = TypeHorizontalAlignment.Left
            };
            SideBar.Classes.Add("sidebar");

            Head.Content.Add(new ControlPanelCenter(this, new ControlText(this)
            {
                Text = Title,
                TextColor = new PropertyColorText(TypeColorText.White),
                Format = TypeFormatText.H1,
                Size = new PropertySizeText(TypeSizeText.Default),
                Padding = new PropertySpacingPadding(PropertySpacing.Space.One),
                Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.Null),
                Styles = new List<string>(new[] { "font-size:190%; height: 50px;" })
            }));

            Main.Classes.Add("content");
            PathCtrl.Classes.Add("content");

            Main.Content.Add(new ControlTabMenu(this));
            Main.Content.Add(new ControlLine(this));

            Foot.Content.Add(new ControlText(this, "now")
            {
                Text = string.Format("{0}", ViewModel.Instance.Now),
                TextColor = new PropertyColorText(TypeColorText.Muted),
                Format = TypeFormatText.Center,
                Size = new PropertySizeText(TypeSizeText.Small)
            });
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();
        }
    }
}
