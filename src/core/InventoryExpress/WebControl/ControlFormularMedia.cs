using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebApiControl;
using WebExpress.WebApp.WebPage;

namespace InventoryExpress.WebControl
{
    public class ControlFormularMedia : ControlFormular
    {
        /// <summary>
        /// Liefert das Bild
        /// </summary>
        public ControlFormularItemInputFile Image { get; } = new ControlFormularItemInputFile()
        {
            Name = "image",
            Help = "inventoryexpress:inventoryexpress.media.form.image.description",
            AcceptFile = new string[] { "image/*" },
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.Three)
        };

        /// <summary>
        /// Liefert die Schlagwörter
        /// </summary>
        public ControlApiFormularItemInputSelection Tag { get; } = new ControlApiFormularItemInputSelection("tags")
        {
            Name = "tag",
            Label = "inventoryexpress:inventoryexpress.manufacturer.form.tag.label",
            Help = "inventoryexpress:inventoryexpress.manufacturer.form.tag.description",
            Icon = new PropertyIcon(TypeIcon.Tag),
            MultiSelect = true
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormularMedia(string id = null)
            : base(id)
        {
            Name = "media";
            EnableCancelButton = false;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Three, PropertySpacing.Space.None, PropertySpacing.Space.None);
            BackgroundColor = LayoutSchema.FormularBackground;
            Layout = TypeLayoutFormular.Vertical;

            Add(Image);
            Add(Tag);
        }

        /// <summary>
        /// Initialisiert das Formular
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        public override void Initialize(RenderContextFormular context)
        {
            base.Initialize(context);

            Tag.RestUri = context.Uri.Root.Append("api/v1/tags");
        }
    }
}
