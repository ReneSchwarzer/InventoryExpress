using InventoryExpress.WebSession;
using WebExpress.WebCore.WebHtml;
using WebExpress.WebCore.WebPage;
using WebExpress.WebUI.WebControl;

namespace InventoryExpress.WebControl
{
    public class ControlFormularToggleView : ControlFormInline
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlFormularToggleView(string id = null)
            : base(id)
        {
        }

        /// <summary>
        /// Initializes the form.
        /// </summary>
        /// <param name="context">The context in which the control is represented.</param>
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
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is represented.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            return base.Render(context);
        }
    }
}
