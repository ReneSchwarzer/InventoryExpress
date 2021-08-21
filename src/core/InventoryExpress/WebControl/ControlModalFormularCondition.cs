using WebExpress.WebResource;
using WebExpress.UI.WebControl;
using WebExpress.Html;
using WebExpress.WebApp.WebResource;

namespace InventoryExpress.WebControl
{
    public class ControlModalFormularCondition : ControlModalForm
    {
        /// <summary>
        /// Liefert oder setzt den Namen des Zustandes
        /// </summary>
        public ControlFormularItemInputTextBox ConditionName { get; set; }

        /// <summary>
        /// Liefert oder setzt die Beschreibung
        /// </summary>
        public ControlFormularItemInputTextBox Description { get; set; }

        /// <summary>
        /// Bestimmt, ob das Formular zum Bearbeiten oder zum Neuanlegen verwendet werden soll.
        /// </summary>
        public bool Edit { get; set; } = false;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlModalFormularCondition(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="items">Die Steuerelemente, welche dem Formular zugeordnet werden</param>
        public ControlModalFormularCondition(string id = null, params ControlFormularItem[] items)
            : base(id)
        {
            //(Items as List<ControlFormularItem>).AddRange(items);

            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Name = "condition";
            //EnableCancelButton = false;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Three, PropertySpacing.Space.None, PropertySpacing.Space.None);
            BackgroundColor = LayoutSchema.FormularBackground;
            //Layout = TypeLayoutFormular.Horizontal;

            ConditionName = new ControlFormularItemInputTextBox()
            {
                Name = "name",
                Label = "inventoryexpress.supplier.form.name.label",
                Help = "inventoryexpress.supplier.form.name.description",
                Icon = new PropertyIcon(TypeIcon.Font)
            };

            Description = new ControlFormularItemInputTextBox("note")
            {
                Name = "description",
                Label = "inventoryexpress.supplier.form.description.label",
                Help = "inventoryexpress.supplier.form.description.description",
                Format = TypesEditTextFormat.Wysiwyg,
                Icon = new PropertyIcon(TypeIcon.CommentAlt),
                Rows = 10
            };
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
