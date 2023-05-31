using InventoryExpress.Model;
using System;
using System.Linq;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebApiControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.Wql;

namespace InventoryExpress.WebControl
{
    public class ControlFormularLedgerAccount : ControlForm
    {
        /// <summary>
        /// Liefert den Namen des Sachkontos
        /// </summary>
        public ControlFormItemInputTextBox LedgerAccountName { get; } = new ControlFormItemInputTextBox()
        {
            Name = "name",
            Label = "inventoryexpress:inventoryexpress.ledgeraccount.form.name.label",
            Help = "inventoryexpress:inventoryexpress.ledgeraccount.form.name.description",
            Icon = new PropertyIcon(TypeIcon.Font)
        };

        /// <summary>
        /// Liefert die Beschreibung
        /// </summary>
        public ControlFormItemInputTextBox Description { get; } = new ControlFormItemInputTextBox("note")
        {
            Name = "description",
            Label = "inventoryexpress:inventoryexpress.ledgeraccount.form.description.label",
            Help = "inventoryexpress:inventoryexpress.ledgeraccount.form.description.description",
            Format = TypesEditTextFormat.Wysiwyg,
            Icon = new PropertyIcon(TypeIcon.CommentAlt)
        };

        /// <summary>
        /// Liefert Returns or sets the tags.
        /// </summary>
        public ControlApiFormularItemInputSelection Tag { get; } = new ControlApiFormularItemInputSelection("tags")
        {
            Name = "tag",
            Label = "inventoryexpress:inventoryexpress.ledgeraccount.form.tag.label",
            Help = "inventoryexpress:inventoryexpress.ledgeraccount.form.tag.description",
            Icon = new PropertyIcon(TypeIcon.Tag),
            MultiSelect = true
        };

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Returns or sets the id.</param>
        public ControlFormularLedgerAccount(string id = null)
            : base(id)
        {
            Name = "ledgeraccount";
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Three, PropertySpacing.Space.None, PropertySpacing.Space.None);
            BackgroundColor = LayoutSchema.FormularBackground;
            Layout = TypeLayoutFormular.Horizontal;

            LedgerAccountName.Validation += LedgerAccountNameValidation;

            Add(LedgerAccountName);
            Add(Description);
            Add(Tag);
        }

        /// <summary>
        /// Initializes the form.
        /// </summary>
        /// <param name="context">The context in which the control is represented.</param>
        public override void Initialize(RenderContextFormular context)
        {
            base.Initialize(context);

            Tag.RestUri = context.Uri.ModuleRoot.Append("api/v1/tags");
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Feld LedgerAccountName validiert werden soll.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument./param>
        private void LedgerAccountNameValidation(object sender, ValidationEventArgs e)
        {
            var guid = e.Context.Request.GetParameter("LedgerAccountId")?.Value;
            var ledgeraccount = ViewModel.GetLedgerAccount(guid);

            if (e.Value == null || e.Value.Length < 1)
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.ledgeraccount.validation.name.invalid"));
            }
            else if
            (
                ledgeraccount == null &&
                ViewModel.GetLedgerAccounts(new WqlStatement()).Where(x => x.Name.Equals(e.Value, StringComparison.OrdinalIgnoreCase)).Any()
            )
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.ledgeraccount.validation.name.used"));
            }
            else if
            (
                ledgeraccount != null &&
                !ledgeraccount.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase) &&
                ViewModel.GetLedgerAccounts(new WqlStatement()).Where(x => x.Name.Equals(e.Value, StringComparison.OrdinalIgnoreCase)).Any()
            )
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.ledgeraccount.validation.name.used"));
            }
        }
    }
}
