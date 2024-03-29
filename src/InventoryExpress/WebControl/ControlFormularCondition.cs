﻿using InventoryExpress.Model;
using InventoryExpress.Parameter;
using System;
using System.Linq;
using WebExpress.WebUI.WebControl;
using WebExpress.WebApp.WebPage;

namespace InventoryExpress.WebControl
{
    public class ControlFormularCondition : ControlForm
    {
        /// <summary>
        /// Liefert den Namen der Kostenstelle
        /// </summary>
        public ControlFormItemInputTextBox ConditionName { get; } = new ControlFormItemInputTextBox()
        {
            Name = "name",
            Label = "inventoryexpress:inventoryexpress.condition.form.name.label",
            Help = "inventoryexpress:inventoryexpress.condition.form.name.description",
            Icon = new PropertyIcon(TypeIcon.Font)
        };

        /// <summary>
        /// Liefert die Beschreibung
        /// </summary>
        public ControlFormItemInputTextBox Description { get; } = new ControlFormItemInputTextBox("note")
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
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlFormularCondition(string id = null)
            : base(id)
        {
            Name = "condition";
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Three, PropertySpacing.Space.None, PropertySpacing.Space.None);
            BackgroundColor = LayoutSchema.FormularBackground;
            Layout = TypeLayoutForm.Horizontal;

            ConditionName.Validation += ConditionNameValidation;

            Add(ConditionName);
            Add(Description);
        }

        /// <summary>
        /// Initializes the form.
        /// </summary>
        /// <param name="context">The context in which the control is represented.</param>
        public override void Initialize(RenderContextFormular context)
        {
            base.Initialize(context);
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Feld ConditionName validiert werden soll.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument./param>
        private void ConditionNameValidation(object sender, ValidationEventArgs e)
        {
            var guid = e.Context.Request.GetParameter<ParameterConditionId>()?.Value;
            var condition = ViewModel.GetCondition(guid);

            if (e.Value == null || e.Value.Length < 1)
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.condition.validation.name.invalid"));
            }
            else if
            (
                condition == null &&
                ViewModel.GetConditions().Where(x => x.Name.Equals(e.Value, StringComparison.OrdinalIgnoreCase)).Any()
            )
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.condition.validation.name.used"));
            }
            else if
            (
                condition != null &&
                !condition.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase) &&
                ViewModel.GetConditions().Where(x => x.Name.Equals(e.Value, StringComparison.OrdinalIgnoreCase)).Any()
            )
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "inventoryexpress:inventoryexpress.condition.validation.name.used"));
            }
        }
    }
}
