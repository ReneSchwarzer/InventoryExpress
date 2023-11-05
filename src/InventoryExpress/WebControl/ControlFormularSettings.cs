using WebExpress.WebUI.WebControl;
using WebExpress.WebApp.WebPage;

namespace InventoryExpress.WebControl
{
    public class ControlFormularSettings : ControlForm
    {
        /// <summary>
        /// Liefert die Währung
        /// </summary>
        public ControlFormItemInputTextBox Currency { get; } = new ControlFormItemInputTextBox("currency")
        {
            Name = "currency",
            Label = "inventoryexpress:inventoryexpress.setting.currency.label",
            Help = "inventoryexpress:inventoryexpress.setting.currency.description",
            Icon = new PropertyIcon(TypeIcon.EuroSign),
            Format = TypesEditTextFormat.Default
        };

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Die Formular-Id</param>
        public ControlFormularSettings(string id = null)
            : base(id)
        {
            Name = "form_comment";
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Three, PropertySpacing.Space.Five, PropertySpacing.Space.None);
            BackgroundColor = LayoutSchema.FormularBackground;
            Layout = TypeLayoutForm.Vertical;
            SubmitButton.Icon = new PropertyIcon(TypeIcon.Save);
            SubmitButton.Text = "inventoryexpress:inventoryexpress.setting.submit.label";

            Currency.Validation += OnCurrencyValidation;

            Add(Currency);
        }

        /// <summary>
        /// Validierung der Währungsangabe
        /// </summary>
        /// <param name="sender">Der Auslöser</param>
        /// <param name="e">The event argument.</param>
        private void OnCurrencyValidation(object sender, ValidationEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.Value))
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.setting.currency.validation.null"));
            }
            else if (e.Value.Length > 10)
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.setting.currency.validation.tolong"));
            }
        }
    }
}
