using InventoryExpress.Model;
using System;
using System.Linq;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.Wql;

namespace InventoryExpress.WebControl
{
    public class ControlFormularCondition : ControlFormular
    {
        /// <summary>
        /// Liefert den Namen der Kostenstelle
        /// </summary>
        public ControlFormularItemInputTextBox ConditionName { get; } = new ControlFormularItemInputTextBox()
        {
            Name = "name",
            Label = "inventoryexpress:inventoryexpress.condition.form.name.label",
            Help = "inventoryexpress:inventoryexpress.condition.form.name.description",
            Icon = new PropertyIcon(TypeIcon.Font)
        };

        /// <summary>
        /// Liefert die Beschreibung
        /// </summary>
        public ControlFormularItemInputTextBox Description { get; } = new ControlFormularItemInputTextBox("note")
        {
            Name = "description",
            Label = "inventoryexpress:inventoryexpress.condition.form.description.label",
            Help = "inventoryexpress:inventoryexpress.condition.form.description.description",
            Format = TypesEditTextFormat.Wysiwyg,
            Icon = new PropertyIcon(TypeIcon.CommentAlt)
        };

        /// <summary>
        /// Bestimmt, ob das Formular zum Bearbeiten oder zum Neuanlegen verwendet werden soll.
        /// </summary>
        public bool Edit { get; set; } = false;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormularCondition(string id = null)
            : base(id)
        {
            Name = "condition";
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Three, PropertySpacing.Space.None, PropertySpacing.Space.None);
            BackgroundColor = LayoutSchema.FormularBackground;
            Layout = TypeLayoutFormular.Horizontal;

            ConditionName.Validation += ConditionNameValidation;

            Add(ConditionName);
            Add(Description);
        }

        /// <summary>
        /// Initialisiert das Formular
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        public override void Initialize(RenderContextFormular context)
        {
            base.Initialize(context);
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Feld ConditionName validiert werden soll.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">Die Eventargumente/param>
        private void ConditionNameValidation(object sender, ValidationEventArgs e)
        {
            var guid = e.Context.Request.GetParameter("ConditionID")?.Value;
            var condition = ViewModel.GetCondition(guid);

            if (e.Value == null || e.Value.Length < 1)
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.condition.validation.name.invalid"));
            }
            else if
            (
                condition == null &&
                ViewModel.GetConditions(new WqlStatement()).Where(x => x.Name.Equals(e.Value, StringComparison.OrdinalIgnoreCase)).Any()
            )
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.condition.validation.name.used"));
            }
            else if
            (
                condition != null &&
                !condition.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase) &&
                ViewModel.GetConditions(new WqlStatement()).Where(x => x.Name.Equals(e.Value, StringComparison.OrdinalIgnoreCase)).Any()
            )
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.condition.validation.name.used"));
            }
        }
    }
}
