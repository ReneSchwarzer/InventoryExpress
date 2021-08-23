using InventoryExpress.Model;
using System;
using System.Linq;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;

namespace InventoryExpress.WebControl
{
    public class ControlModalFormularConditionEdit : ControlModalForm
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
        /// Der zu bearbeitende Status oder null, wenn bei Neuanlage
        /// </summary>
        public Condition Item { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlModalFormularConditionEdit(string id = null)
            : base("edit_" + id)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            ConditionName = new ControlFormularItemInputTextBox()
            {
                Name = "name",
                Label = "inventoryexpress.condition.form.name.label",
                Help = "inventoryexpress.condition.form.name.description",
                Icon = new PropertyIcon(TypeIcon.Font)
            };

            Description = new ControlFormularItemInputTextBox("note")
            {
                Name = "description",
                Label = "inventoryexpress.condition.form.description.label",
                Help = "inventoryexpress.condition.form.description.description",
                Format = TypesEditTextFormat.Wysiwyg,
                Icon = new PropertyIcon(TypeIcon.CommentAlt),
                Rows = 10
            };

            Formular.Add(ConditionName);
            Formular.Add(Description);

            Formular.FillFormular += (s, e) =>
            {
                ConditionName.Value = Item != null ? Item.Name : string.Empty;
                Description.Value = Item != null ? Item.Description : string.Empty;
            };

            Formular.ProcessFormular += (s, e) =>
            {
                lock (ViewModel.Instance.Database)
                {
                    if (Item == null)
                    {
                        // Daten verarbeiten und neuen Zustand anlegen
                        var condition = new Condition()
                        {
                            Name = ConditionName.Value,
                            Description = Description.Value,
                            Grade = ViewModel.Instance.Conditions.Any() ? ViewModel.Instance.Conditions.Select(x => x.Grade).Max() + 1 : 1,
                            Created = DateTime.Now,
                            Updated = DateTime.Now,
                            Guid = Guid.NewGuid().ToString()
                        };

                        // Neuanlage
                        ViewModel.Instance.Conditions.Add(condition);
                    }
                    else
                    {
                        // Update 
                        Item.Name = ConditionName.Value;
                        Item.Description = Description.Value;
                        Item.Updated = DateTime.Now;
                    }

                    ViewModel.Instance.SaveChanges();
                }

                // Formular für die nächste Verwendung bereinigen
                Formular.Reset();
            };
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Header = context.Page.I18N(Item == null? "inventoryexpress.condition.add.header" : "inventoryexpress.condition.edit.header");
            Formular.RedirectUri = context.Uri;

            ConditionName.Validation += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(e.Value))
                {
                    e.Results.Add(new ValidationResult()
                    {
                        Type = TypesInputValidity.Error,
                        Text = context.Page.I18N("inventoryexpress.condition.form.name.validation.empty")
                    });
                }

                lock (ViewModel.Instance.Database)
                {
                    if (Item == null && ViewModel.Instance.Conditions.Where(x => x.Name.ToLower() == e.Value.ToLower()).Any())
                    {
                        e.Results.Add(new ValidationResult()
                        {
                            Type = TypesInputValidity.Error,
                            Text = context.Page.I18N("inventoryexpress.condition.form.name.validation.inuse")
                        });
                    }
                }
            };

            return base.Render(context);
        }
    }
}
