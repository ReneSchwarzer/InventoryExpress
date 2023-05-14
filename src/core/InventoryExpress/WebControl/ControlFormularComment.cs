using WebExpress.Html;
using WebExpress.UI.WebControl;
using WebExpress.WebPage;

namespace InventoryExpress.WebControl
{
    public class ControlFormularComment : ControlFormular
    {
        /// <summary>
        /// Liefert oder setzt den Kommentar
        /// </summary>
        public ControlFormularItemInputTextBox Comment { get; } = new ControlFormularItemInputTextBox("comment")
        {
            Name = "comment",
            Label = "inventoryexpress:inventoryexpress.inventory.comment.label",
            Help = "inventoryexpress:inventoryexpress.inventory.comment.description",
            Icon = new PropertyIcon(TypeIcon.Comment),
            Format = TypesEditTextFormat.Wysiwyg
        };

        /// <summary>
        /// Konstruktor
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
        /// Initialisiert das Formular
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        public override void Initialize(RenderContextFormular context)
        {
            base.Initialize(context);
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
