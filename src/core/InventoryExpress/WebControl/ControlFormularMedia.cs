using WebExpress.Html;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebResource;

namespace InventoryExpress.WebControl
{
    public class ControlFormularMedia : ControlFormular
    {
        /// <summary>
        /// Liefert oder setzt das Bild
        /// </summary>
        public ControlFormularItemInputFile Image { get; set; }

        /// <summary>
        /// Liefert oder setzt die Schlagwörter
        /// </summary>
        public ControlFormularItemInputTag Tag { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormularMedia(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Name = "media";
            EnableCancelButton = false;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Three, PropertySpacing.Space.None, PropertySpacing.Space.None);
            BackgroundColor = LayoutSchema.FormularBackground;
            Layout = TypeLayoutFormular.Vertical;

            Image = new ControlFormularItemInputFile()
            {
                Name = "image",
                Help = "inventoryexpress.media.form.image.description",
                AcceptFile = new string[] { "image/*" },
                Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.Three)
            };

            Tag = new ControlFormularItemInputTag("tags")
            {
                Name = "tag",
                Label = "inventoryexpress.manufacturer.form.tag.label",
                Help = "inventoryexpress.manufacturer.form.tag.description",
                Icon = new PropertyIcon(TypeIcon.Tag)
            };
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Add(Image);
            Add(Tag);

            return base.Render(context);
        }
    }
}
