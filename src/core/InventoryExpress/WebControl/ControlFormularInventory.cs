using InventoryExpress.Model;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebResource;

namespace InventoryExpress.WebControl
{
    public class ControlFormularInventory : ControlFormular
    {
        /// <summary>
        /// Liefert oder setzt den Namen des Inventargegenstandes
        /// </summary>
        public ControlFormularItemInputTextBox InventoryName { get; set; }

        /// <summary>
        /// Liefert oder setzt den Hersteller
        /// </summary>
        public ControlFormularItemInputComboBox Manufacturer { get; set; }

        /// <summary>
        /// Liefert oder setzt den Standort
        /// </summary>
        public ControlFormularItemInputComboBox Location { get; set; }

        /// <summary>
        /// Liefert oder setzt den Lieferanten
        /// </summary>
        public ControlFormularItemInputComboBox Supplier { get; set; }

        /// <summary>
        /// Liefert oder setzt das Sachkonto
        /// </summary>
        public ControlFormularItemInputComboBox LedgerAccount { get; set; }

        /// <summary>
        /// Liefert oder setzt die Kostenstelle
        /// </summary>
        public ControlFormularItemInputComboBox CostCenter { get; set; }

        /// <summary>
        /// Liefert oder setzt den Zustand
        /// </summary>
        public ControlFormularItemInputComboBox Condition { get; set; }

        /// <summary>
        /// Liefert oder setzt die Zugehörigkeit
        /// </summary>
        public ControlFormularItemInputComboBox Parent { get; set; }

        /// <summary>
        /// Liefert oder setzt das Template
        /// </summary>
        public ControlFormularItemInputComboBox Template { get; set; }

        /// <summary>
        /// Liefert oder setzt die Attribute
        /// </summary>
        public ControlFormularItemGroupVertical Attributes { get; set; }

        /// <summary>
        /// Liefert oder setzt die Schlagwörter
        /// </summary>
        public ControlFormularItemInputTextBox Tag { get; set; }

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
        public ControlFormularInventory(string id = null)
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

            Name = "inventory";
            EnableCancelButton = false;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Three, PropertySpacing.Space.None, PropertySpacing.Space.None);
            BackgroundColor = LayoutSchema.FormularBackground;
            Border = new PropertyBorder(true);
            Padding = new PropertySpacingPadding(PropertySpacing.Space.Two);
            Layout = TypeLayoutFormular.Horizontal;

            InventoryName = new ControlFormularItemInputTextBox()
            {
                Name = "name",
                Label = "inventoryexpress.inventory.name.label",
                Help = "inventoryexpress.inventory.name.description",
                Icon = new PropertyIcon(TypeIcon.Font),
                Format = TypesEditTextFormat.Default
            };

            Manufacturer = new ControlFormularItemInputComboBox()
            {
                Name = "manufacturer",
                Label = "inventoryexpress.manufacturers.label",
                Help = "inventoryexpress.manufacturer.description",
                Icon = new PropertyIcon(TypeIcon.Industry)
            };

            Manufacturer.Items.Add(new ControlFormularItemInputComboBoxItem()
            {
                Text = string.Empty,
                Value = null
            });

            Manufacturer.Items.AddRange(ViewModel.Instance.Manufacturers.Select(x => new ControlFormularItemInputComboBoxItem()
            {
                Text = x.Name,
                Value = x.Guid
            }));

            Location = new ControlFormularItemInputComboBox()
            {
                Name = "location",
                Label = "inventoryexpress.location.label",
                Help = "inventoryexpress.location.description",
                Icon = new PropertyIcon(TypeIcon.Map)
            };

            Location.Items.Add(new ControlFormularItemInputComboBoxItem()
            {
                Text = string.Empty,
                Value = null
            });

            Location.Items.AddRange(ViewModel.Instance.Locations.Select(x => new ControlFormularItemInputComboBoxItem()
            {
                Text = x.Name,
                Value = x.Guid
            }));

            Supplier = new ControlFormularItemInputComboBox()
            {
                Name = "supplier",
                Label = "inventoryexpress.supplier.label",
                Help = "inventoryexpress.supplier.description",
                Icon = new PropertyIcon(TypeIcon.Truck)
            };

            Supplier.Items.Add(new ControlFormularItemInputComboBoxItem()
            {
                Text = string.Empty,
                Value = null
            });

            Supplier.Items.AddRange(ViewModel.Instance.Suppliers.Select(x => new ControlFormularItemInputComboBoxItem()
            {
                Text = x.Name,
                Value = x.Guid
            }));

            LedgerAccount = new ControlFormularItemInputComboBox()
            {
                Name = "glaccount",
                Label = "inventoryexpress.ledgeraccount.label",
                Help = "inventoryexpress.ledgeraccount.description",
                Icon = new PropertyIcon(TypeIcon.At)
            };

            LedgerAccount.Items.Add(new ControlFormularItemInputComboBoxItem()
            {
                Text = string.Empty,
                Value = null
            });

            LedgerAccount.Items.AddRange(ViewModel.Instance.LedgerAccounts.Select(x => new ControlFormularItemInputComboBoxItem()
            {
                Text = x.Name,
                Value = x.Guid
            }));

            CostCenter = new ControlFormularItemInputComboBox()
            {
                Name = "costcenter",
                Label = "inventoryexpress.costcenter.label",
                Help = "inventoryexpress.costcenter.description",
                Icon = new PropertyIcon(TypeIcon.ShoppingBag)
            };

            CostCenter.Items.Add(new ControlFormularItemInputComboBoxItem()
            {
                Text = string.Empty,
                Value = null
            });

            CostCenter.Items.AddRange(ViewModel.Instance.CostCenters.Select(x => new ControlFormularItemInputComboBoxItem()
            {
                Text = x.Name,
                Value = x.Guid
            }));

            Condition = new ControlFormularItemInputComboBox()
            {
                Name = "state",
                Label = "inventoryexpress.condition.label",
                Help = "inventoryexpress.condition.description",
                Icon = new PropertyIcon(TypeIcon.Star)
            };

            Condition.Items.Add(new ControlFormularItemInputComboBoxItem()
            {
                Text = string.Empty,
                Value = null
            });

            Condition.Items.AddRange(ViewModel.Instance.Conditions.OrderBy(x => x.Grade).Select(x => new ControlFormularItemInputComboBoxItem()
            {
                Text = string.Format("{0} - {1}", x.Grade, x.Name),
                Value = x.Guid
            }));

            Parent = new ControlFormularItemInputComboBox()
            {
                Name = "parent",
                Label = "inventoryexpress.parent.label",
                Help = "inventoryexpress.parent.description",
                Icon = new PropertyIcon(TypeIcon.Link)
            };

            Parent.Items.Add(new ControlFormularItemInputComboBoxItem()
            {
                Text = string.Empty,
                Value = null
            });

            Parent.Items.AddRange(ViewModel.Instance.Inventories.Select(x => new ControlFormularItemInputComboBoxItem()
            {
                Text = x.Name,
                Value = x.Guid
            }));

            Template = new ControlFormularItemInputComboBox()
            {
                Name = "template",
                Label = "inventoryexpress.template.label",
                Help = "inventoryexpress.template.description",
                Icon = new PropertyIcon(TypeIcon.Clone),
                OnChange = new PropertyOnChange(TypeOnChange.Submit)
            };

            Template.Items.Add(new ControlFormularItemInputComboBoxItem()
            {
                Text = string.Empty,
                Value = null
            });

            Template.Items.AddRange(ViewModel.Instance.Templates.Select(x => new ControlFormularItemInputComboBoxItem()
            {
                Text = x.Name,
                Value = x.Guid
            }));

            Attributes = new ControlFormularItemGroupVertical()
            {
                 
            };

            Tag = new ControlFormularItemInputTextBox()
            {
                Name = "tag",
                Label = "inventoryexpress.tags.label",
                Help = "inventoryexpress.tags.description",
                Icon = new PropertyIcon(TypeIcon.Tag)
            };

            Description = new ControlFormularItemInputTextBox("note")
            {
                Name = "description",
                Label = "inventoryexpress.description.label",
                Help = "inventoryexpress.description.description",
                Format = TypesEditTextFormat.Wysiwyg,
                Icon = new PropertyIcon(TypeIcon.CommentAlt),
                Rows = 10,
                AutoInitialize = true
            };

            Add(InventoryName);
            Add(Manufacturer);
            Add(Location);
            Add(Supplier);
            Add(LedgerAccount);
            Add(CostCenter);
            Add(Condition);
            Add(Parent);
            Add(Template);
            Add(Attributes);
            Add(Tag);
            Add(Description);

        }

        /// <summary>
        /// Vorverarbeitung des Formulars
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        public override void PreProcess(RenderContext context)
        {
            if (context.Page.HasParam(Template.Name))
            {
                var template = context.Page.GetParamValue(Template.Name);
                foreach (var t in ViewModel.Instance.Templates.Where(x => x.Guid == template))
                {
                    var attributes = ViewModel.Instance.TemplateAttributes.Where(x => x.TemplateId == t.Id)
                        .Join(ViewModel.Instance.Attributes, x => x.AttributeId, y => y.Id, (x, y) => y);

                    foreach (var attribute in attributes)
                    {
                        Attributes.Items.Add(new ControlFormularItemInputTextBox()
                        {
                            Name = "attribute_" + attribute.Guid,
                            Label = attribute.Name,
                            Help = attribute.Description
                        });
                    }
                }
            }
            else
            {
                foreach (var t in ViewModel.Instance.Templates.Where(x => x.Guid == Template.Value))
                {
                    var attributes = ViewModel.Instance.TemplateAttributes.Where(x => x.TemplateId == t.Id)
                        .Join(ViewModel.Instance.Attributes, x => x.AttributeId, y => y.Id, (x, y) => y);

                    foreach (var attribute in attributes)
                    {
                        Attributes.Items.Add(new ControlFormularItemInputTextBox()
                        {
                            Name = "attribute_" + attribute.Guid,
                            Label = attribute.Name,
                            Help = attribute.Description
                        });
                    }
                }
            }
        }

        /// <summary>
        ///  Prüft das Eingabeelement auf Korrektheit der Daten
        /// </summary>
        public override void Validate()
        {
            base.Validate();

            InventoryName.Validation += (s, e) =>
            {
                e.Results.Add(new ValidationResult() { Text = "Fehler", Type = TypesInputValidity.Error });
            };
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var html = base.Render(context);
            return html;
        }
    }
}
