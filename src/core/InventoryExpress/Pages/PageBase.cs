using InventoryExpress.Model;
using WebExpress.Html;
using WebExpress.Internationalization;
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
            Title = title;
            Favicons.Add(new Favicon("/Assets/img/Favicon.png", TypeFavicon.PNG));
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Init()
        {          
            base.Init();

            Footer.Content.Add(new ControlText("now")
            {
                Text = string.Format("{0}", ViewModel.Instance.Now),
                TextColor = new PropertyColorText(TypeColorText.Muted),
                Format = TypeFormatText.Center,
                Size = new PropertySizeText(TypeSizeText.Small)
            });
        }
    }
}
