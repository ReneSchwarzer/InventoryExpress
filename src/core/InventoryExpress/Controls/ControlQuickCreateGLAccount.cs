using InventoryExpress.Pages;
using WebExpress.Html;
using WebExpress.UI.Controls;
using WebExpress.UI.Plugin;

namespace InventoryExpress.Controls
{
    public class ControlQuickCreateGLAccount : ControlLink, IPluginComponentQuickCreateSecondary
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlQuickCreateGLAccount()
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
            Text = "Sachkonten";
            Uri = context.Page.Uri.Root.Append("glaccounts/add");
            Active = context.Page is IPageGLAccount ? TypeActive.Active : TypeActive.None;
            Icon = new PropertyIcon(TypeIcon.At);

            return base.Render(context);
        }

    }
}
