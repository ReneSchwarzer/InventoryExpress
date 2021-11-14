using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebPage;

namespace InventoryExpress.WebControl
{
    public class ControlFormularSettings : ControlFormular
    {
        /// <summary>
        /// Liefert die Währung
        /// </summary>
        public ControlFormularItemInputTextBox Currency { get; } = new ControlFormularItemInputTextBox("currency")
        {
            Name = "currency",
            Label = "inventoryexpress:inventoryexpress.setting.currency.label",
            Help = "inventoryexpress:inventoryexpress.setting.currency.description",
            Icon = new PropertyIcon(TypeIcon.EuroSign),
            Format = TypesEditTextFormat.Default
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die Formular-ID</param>
        public ControlFormularSettings(string id = null)
            : base(id)
        {
            Name = "form_comment";
            EnableCancelButton = true;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Three, PropertySpacing.Space.Five, PropertySpacing.Space.None);
            BackgroundColor = LayoutSchema.FormularBackground;
            Layout = TypeLayoutFormular.Vertical;
            SubmitButton.Icon = new PropertyIcon(TypeIcon.Save);
            SubmitButton.Text = "inventoryexpress:inventoryexpress.setting.submit.label";
            EnableCancelButton = false;

            Currency.Validation += OnCurrencyValidation;

            Add(Currency);
        }

        /// <summary>
        /// Validierung der Währungsangabe
        /// </summary>
        /// <param name="sender">Der Auslöser</param>
        /// <param name="e">Die Eventargumente</param>
        private void OnCurrencyValidation(object sender, ValidationEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.Value))
            {
                e.Results.Add(new ValidationResult() { Text = e.Context.I18N("inventoryexpress:inventoryexpress.setting.currency.validation.null"), Type = TypesInputValidity.Error });
            }
            else if (e.Value.Length > 10)
            {
                e.Results.Add(new ValidationResult() { Text = e.Context.I18N("inventoryexpress:inventoryexpress.setting.currency.validation.tolong"), Type = TypesInputValidity.Error });
            }
        }
    }
}
