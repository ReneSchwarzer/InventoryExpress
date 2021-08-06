using WebExpress.Html;
using WebExpress.UI.WebControl;

namespace InventoryExpress.WebControl
{
    public class ControlFormularFileUpload : ControlFormular
    {
        /// <summary>
        /// Liefert oder setzt das Dokument
        /// </summary>
        public ControlFormularItemInputFile File { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormularFileUpload(string id = null)
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
            EnableCancelButton = true;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Three, PropertySpacing.Space.None, PropertySpacing.Space.None);
            Layout = TypeLayoutFormular.Vertical;

            File = new ControlFormularItemInputFile()
            {
                Name = "file",
                //Label = "inventoryexpress.media.form.image.label",
                Help = "inventoryexpress.media.form.file.description",
                //Icon = new PropertyIcon(TypeIcon.Image),
                //AcceptFile = new string[] { "image/*, video/*, audio/*, .pdf, .doc, .docx, .txt" },
                Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.Three)
            };
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Add(File);

            return base.Render(context);
        }
    }
}
