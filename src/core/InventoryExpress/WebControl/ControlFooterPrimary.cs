using InventoryExpress.Model;
using WebExpress.Attribute;
using WebExpress.Html;
using WebExpress.UI.Attribute;
using WebExpress.UI.Component;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.Components;

namespace InventoryExpress.WebControl
{
    [Section(Section.FooterPrimary)]
    [Application("InventoryExpress")]
    public sealed class ControlFooterPrimary : ControlText, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlFooterPrimary()
            : base()
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Text = string.Format("{0}", ViewModel.Instance.Now);
            TextColor = new PropertyColorText(TypeColorText.Muted);
            Format = TypeFormatText.Center;
            Size = new PropertySizeText(TypeSizeText.Small);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            return base.Render(context);
        }
    }
}
