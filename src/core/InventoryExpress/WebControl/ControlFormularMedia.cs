using WebExpress.UI.WebControl;

namespace InventoryExpress.WebControl
{
    public class ControlFormularMedia : ControlFormular
    {
        /// <summary>
        /// Liefert oder setzt das Bild
        /// </summary>
        public ControlFormularItemInputFile Image { get; set; }

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
            Name = "location";
            EnableCancelButton = false;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Three, PropertySpacing.Space.None, PropertySpacing.Space.None);
            Layout = TypeLayoutFormular.Inline;

            Image = new ControlFormularItemInputFile()
            {
                Name = "image",
                //Label = "inventoryexpress.media.form.image.label",
                Help = "inventoryexpress.media.form.image.description",
                //Icon = new PropertyIcon(TypeIcon.Image),
                AcceptFile = new string[] { "image/*" },
                Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.Three)
        };

            Add(Image);
        }
    }
}
