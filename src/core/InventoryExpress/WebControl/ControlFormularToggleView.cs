using InventoryExpress.WebSession;
using WebExpress.Html;
using WebExpress.UI.WebControl;
using WebExpress.WebPage;

namespace InventoryExpress.WebControl
{
    public class ControlFormularToggleView : ControlFormularInline
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlFormularToggleView(string id = null)
            : base(id)
        {
        }

        /// <summary>
        /// Initialisiert das Formular
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        public override void Initialize(RenderContextFormular context)
        {
            base.Initialize(context);

            var property = context.Request.Session.GetProperty<SessionPropertyToggleStatus>();

            SubmitButton.Icon = new PropertyIcon(property == null || property.ViewList ? TypeIcon.Table : TypeIcon.List);
            SubmitButton.Text = string.Empty;
            SubmitButton.TextColor = new PropertyColorText(TypeColorText.Secondary);
            SubmitButton.Color = new PropertyColorButton(TypeColorButton.Light);
            SubmitButton.Border = new PropertyBorder(false);
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
