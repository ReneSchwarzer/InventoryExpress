using InventoryExpress.Pages;
using WebExpress.Html;
using WebExpress.UI.Controls;
using WebExpress.UI.Plugin;

namespace InventoryExpress.Controls
{
    public class ControlAppNavigationLocation : ControlLink, IPluginComponentAppNavigationPrimary
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlAppNavigationLocation()
            : base()
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            TextColor = new PropertyColorText(TypeColorText.Light);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Text = "Standorte";
            Uri = context.Page.Uri.Root.Append("locations");
            Active = context.Page is IPageLocation ? TypeActive.Active : TypeActive.None;
            Icon = new PropertyIcon(TypeIcon.Map);

            return base.Render(context);
        }

    }
}
