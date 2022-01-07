using InventoryExpress.Model;
using System;
using System.Linq;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebPage;

namespace InventoryExpress.WebControl
{
    public class ControlModalFormularAttributeEdit : ControlModalFormular
    {
        /// <summary>
        /// Liefert oder setzt den Namen des Attributes
        /// </summary>
        public ControlFormularItemInputTextBox AttributeName { get; set; }

        /// <summary>
        /// Liefert oder setzt die Beschreibung
        /// </summary>
        public ControlFormularItemInputTextBox Description { get; set; }

        /// <summary>
        /// Der zu bearbeitende Status oder null, wenn bei Neuanlage
        /// </summary>
        public Model.Entity.Attribute Item { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlModalFormularAttributeEdit(string id = null)
            : base("edit_" + id)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            AttributeName = new ControlFormularItemInputTextBox()
            {
                Name = "name",
                Label = "inventoryexpress:inventoryexpress.attribute.form.name.label",
                Help = "inventoryexpress:inventoryexpress.attribute.form.name.description",
                Icon = new PropertyIcon(TypeIcon.Font)
            };

            Description = new ControlFormularItemInputTextBox("note")
            {
                Name = "description",
                Label = "inventoryexpress:inventoryexpress.attribute.form.description.label",
                Help = "inventoryexpress:inventoryexpress.attribute.form.description.description",
                Format = TypesEditTextFormat.Wysiwyg,
                Icon = new PropertyIcon(TypeIcon.CommentAlt),
                Rows = 10
            };

            Formular.Add(AttributeName);
            Formular.Add(Description);

            Formular.FillFormular += (s, e) =>
            {
                AttributeName.Value = Item != null ? Item.Name : string.Empty;
                Description.Value = Item != null ? Item.Description : string.Empty;
            };

            Formular.ProcessFormular += (s, e) =>
            {
                lock (ViewModel.Instance.Database)
                {
                    if (Item == null)
                    {
                        // Daten verarbeiten und neues Attribut anlegen
                        var attribute = new Model.Entity.Attribute()
                        {
                            Name = AttributeName.Value,
                            Description = Description.Value,
                            Created = DateTime.Now,
                            Updated = DateTime.Now,
                            Guid = Guid.NewGuid().ToString()
                        };

                        // Neuanlage
                        ViewModel.Instance.Attributes.Add(attribute);
                    }
                    else
                    {
                        // Update 
                        Item.Name = AttributeName.Value;
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
            Header = context.Page.I18N(Item == null ? "inventoryexpress:inventoryexpress.attribute.add.header" : "inventoryexpress:inventoryexpress.attribute.edit.header");
            Formular.RedirectUri = context.Uri;

            AttributeName.Validation += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(e.Value))
                {
                    e.Results.Add(new ValidationResult
                    (
                        TypesInputValidity.Error,
                        "inventoryexpress:inventoryexpress.attribute.form.name.validation.empty"
                    ));
                }

                lock (ViewModel.Instance.Database)
                {
                    if (Item == null && ViewModel.Instance.Conditions.Where(x => x.Name.ToLower() == e.Value.ToLower()).Any())
                    {
                        e.Results.Add(new ValidationResult
                        (
                            TypesInputValidity.Error,
                            "inventoryexpress:inventoryexpress.attribute.form.name.validation.inuse"
                        ));
                    }
                }
            };

            return base.Render(context);
        }
    }
}
