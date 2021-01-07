using InventoryExpress.Model;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;

namespace InventoryExpress.WebControl
{
    public class ControlFormularSettings : ControlFormular
    {
        /// <summary>
        /// Liefert oder setzt den Kommentar
        /// </summary>
        public ControlFormularItemInputTextBox Currency { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormularSettings(string id = null)
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
            SubmitButton.Icon = new PropertyIcon(TypeIcon.Save);
            SubmitButton.Text = context.I18N("inventoryexpress.settings.submit.label");
            EnableCancelButton = false;

            Currency = new ControlFormularItemInputTextBox("currency")
            {
                Name = "comment",
                Label = "inventoryexpress.settings.currency.label",
                Help = "inventoryexpress.settings.currency.description",
                Icon = new PropertyIcon(TypeIcon.EuroSign),
                Format = TypesEditTextFormat.Default
            };
            
            Add(Currency);
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
