using InventoryExpress.Model;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;

namespace InventoryExpress.WebControl
{
    public class ControlFormularComment : ControlFormular
    {
        /// <summary>
        /// Liefert oder setzt den Kommentar
        /// </summary>
        public ControlFormularItemInputTextBox Comment { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormularComment(string id = null)
            : base(id)
        {
        }

        /// <summary>
        /// Initialisiert das Formular
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        public override void Initialize(RenderContext context)
        {
            base.Initialize(context);

            Name = "form_comment";
            EnableCancelButton = true;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Three, PropertySpacing.Space.Five, PropertySpacing.Space.None);
            Layout = TypeLayoutFormular.Vertical;
            SubmitButton.Icon = new PropertyIcon(TypeIcon.PaperPlane);
            SubmitButton.Text = context.I18N("inventoryexpress.inventory.comment.submit");
            EnableCancelButton = false;
            //RedirectUri = context.Uri;

            Comment = new ControlFormularItemInputTextBox("comment")
            {
                Name = "comment",
                Label = "inventoryexpress.inventory.comment.label",
                Help = "inventoryexpress.inventory.comment.description",
                Icon = new PropertyIcon(TypeIcon.Comment),
                Format = TypesEditTextFormat.Wysiwyg
            };
            
            Add(Comment);
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
