using WebExpress.Html;
using WebExpress.UI.WebControl;
using WebExpress.WebPage;

namespace InventoryExpress.WebControl
{
    public class ControlFormularComment : ControlForm
    {
        /// <summary>
        /// Liefert oder setzt den Kommentar
        /// </summary>
        public ControlFormItemInputTextBox Comment { get; } = new ControlFormItemInputTextBox("comment")
        {
            Name = "comment",
            Label = "inventoryexpress:inventoryexpress.inventory.comment.label",
            Help = "inventoryexpress:inventoryexpress.inventory.comment.description",
            Icon = new PropertyIcon(TypeIcon.Comment),
            Format = TypesEditTextFormat.Wysiwyg
        };

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlFormularComment(string id = null)
            : base(id)
        {
            Add(Comment);

            Name = "form_comment";
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Three, PropertySpacing.Space.Five, PropertySpacing.Space.None);
            Layout = TypeLayoutFormular.Vertical;
            SubmitButton.Icon = new PropertyIcon(TypeIcon.PaperPlane);
            SubmitButton.Text = "inventoryexpress:inventoryexpress.inventory.comment.submit";
        }

        /// <summary>
        /// Initializes the form.
        /// </summary>
        /// <param name="context">The context in which the control is represented.</param>
        public override void Initialize(RenderContextFormular context)
        {
            base.Initialize(context);
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
