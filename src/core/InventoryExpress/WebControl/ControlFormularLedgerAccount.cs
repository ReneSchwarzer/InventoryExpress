using InventoryExpress.Model;
using System;
using System.Linq;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebApiControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.Wql;

namespace InventoryExpress.WebControl
{
    public class ControlFormularLedgerAccount : ControlFormular
    {
        /// <summary>
        /// Liefert den Namen des Sachkontos
        /// </summary>
        public ControlFormularItemInputTextBox LedgerAccountName { get; } = new ControlFormularItemInputTextBox()
        {
            Name = "name",
            Label = "inventoryexpress:inventoryexpress.ledgeraccount.form.name.label",
            Help = "inventoryexpress:inventoryexpress.ledgeraccount.form.name.description",
            Icon = new PropertyIcon(TypeIcon.Font)
        };

        /// <summary>
        /// Liefert die Beschreibung
        /// </summary>
        public ControlFormularItemInputTextBox Description { get; } = new ControlFormularItemInputTextBox("note")
        {
            Name = "description",
            Label = "inventoryexpress:inventoryexpress.ledgeraccount.form.description.label",
            Help = "inventoryexpress:inventoryexpress.ledgeraccount.form.description.description",
            Format = TypesEditTextFormat.Wysiwyg,
            Icon = new PropertyIcon(TypeIcon.CommentAlt)
        };

        /// <summary>
        /// Liefert das Bild
        /// </summary>
        public ControlFormularItemInputFile Image { get; } = new ControlFormularItemInputFile()
        {
            Name = "image",
            Label = "inventoryexpress:inventoryexpress.ledgeraccount.form.image.label",
            Help = "inventoryexpress:inventoryexpress.ledgeraccount.form.image.description",
            Icon = new PropertyIcon(TypeIcon.Image),
            AcceptFile = new string[] { "image/*" }
        };

        /// <summary>
        /// Liefert die Schlagwörter
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
        /// Bestimmt, ob das Formular zum Bearbeiten oder zum Neuanlegen verwendet werden soll.
        /// </summary>
        public bool Edit { get; set; } = false;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormularLedgerAccount(string id = null)
            : base(id)
        {
            Name = "ledgeraccount";
            EnableCancelButton = false;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Three, PropertySpacing.Space.None, PropertySpacing.Space.None);
            BackgroundColor = LayoutSchema.FormularBackground;
            Layout = TypeLayoutFormular.Horizontal;

            LedgerAccountName.Validation += LedgerAccountNameValidation;

            Add(LedgerAccountName);
            Add(Description);

            if (!Edit)
            {
                Add(Image);
            }

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

        /// <summary>
        /// Wird ausgelöst, wenn das Feld LedgerAccountName validiert werden soll.
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente/param>
        private void LedgerAccountNameValidation(object sender, ValidationEventArgs e)
        {
            var guid = e.Context.Request.GetParameter("LedgerAccountID")?.Value;
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
