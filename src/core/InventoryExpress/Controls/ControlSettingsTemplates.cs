using InventoryExpress.Pages;
using WebExpress.Html;
using WebExpress.UI.Controls;
using WebExpress.UI.Plugin;

namespace InventoryExpress.Controls
{
    public class ControlSettingsTemplates : ControlLink, IPluginComponentSettingsPrimary
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlSettingsTemplates()
            : base()
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            TextColor = new PropertyColorText(TypeColorText.Dark);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Text = "Vorlagen";
            Uri = context.Page.Uri.Root.Append("templates");
            Active = context.Page is IPageTemplate ? TypeActive.Active : TypeActive.None;
            //Icon = new PropertyIcon(TypeIcon.Book);

            return base.Render(context);
        }

    }
}
