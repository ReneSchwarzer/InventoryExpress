using InventoryExpress.Model;
using System.Collections.Generic;
using System.Linq;
using WebExpress.UI.Controls;

namespace InventoryExpress.Controls
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
        private ControlFormularItemInputComboBox Manufactor { get; set; }

        /// <summary>
        /// Liefert oder setzt den Standort
        /// </summary>
        private ControlFormularItemInputComboBox Location { get; set; }

        /// <summary>
        /// Liefert oder setzt den Lieferanten
        /// </summary>
        private ControlFormularItemInputComboBox Supplier { get; set; }

        /// <summary>
        /// Liefert oder setzt das Sachkonto
        /// </summary>
        private ControlFormularItemInputComboBox GlAccount { get; set; }

        /// <summary>
        /// Liefert oder setzt die Kostenstelle
        /// </summary>
        private ControlFormularItemInputComboBox CostCenter { get; set; }

        /// <summary>
        /// Liefert oder setzt den Zustand
        /// </summary>
        private ControlFormularItemInputComboBox Condition { get; set; }

        /// <summary>
        /// Liefert oder setzt die Zugehörigkeit
        /// </summary>
        private ControlFormularItemInputComboBox Parent { get; set; }

        /// <summary>
        /// Liefert oder setzt das Template
        /// </summary>
        private ControlFormularItemInputComboBox Template { get; set; }

        /// <summary>
        /// Liefert oder setzt die Attribute
        /// </summary>
        private List<ControlFormularItemInputTextBox> Attributes { get; set; } = new List<ControlFormularItemInputTextBox>();

        /// <summary>
        /// Liefert oder setzt die Schlagwörter
        /// </summary>
        private ControlFormularItemInputTextBox Tag { get; set; }

        /// <summary>
        /// Liefert oder setzt die Beschreibung
        /// </summary>
        private ControlFormularItemInputTextBox Memo { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormularInventory(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Name = "inventory";
            EnableCancelButton = false;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Three, PropertySpacing.Space.None, PropertySpacing.Space.None);
            BackgroundColor = new PropertyColorBackground(TypeColorBackground.Warning);
            Border = new PropertyBorder(true);
            Padding = new PropertySpacingPadding(PropertySpacing.Space.Two);
            Layout = TypeLayoutFormular.Horizontal;

            InventoryName = new ControlFormularItemInputTextBox()
            {
                Name = "name",
                Label = "inventoryexpress.inventory.name.label",
                Help = "inventoryexpress.inventory.name.discription",
                Icon = new PropertyIcon(TypeIcon.Font)
            };

            Manufactor = new ControlFormularItemInputComboBox()
            {
                Name = "manufactor",
                Label = "inventoryexpress.manufactors.label",
                Help = "inventoryexpress.manufactor.discription",
                Icon = new PropertyIcon(TypeIcon.Industry)
            };

            Manufactor.Items.Add(new ControlFormularItemInputComboBoxItem()
            {
                Text = string.Empty,
                Value = null
            });

            Manufactor.Items.AddRange(ViewModel.Instance.Manufacturers.Select(x => new ControlFormularItemInputComboBoxItem()
            {
                Text = x.Name,
                Value = x.ID.ToString()
            }));

            Location = new ControlFormularItemInputComboBox()
            {
                Name = "location",
                Label = "inventoryexpress.location.label",
                Help = "inventoryexpress.location.discription",
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
                Value = x.ID.ToString()
            }));

            Supplier = new ControlFormularItemInputComboBox()
            {
                Name = "supplier",
                Label = "inventoryexpress.supplier.label",
                Help = "inventoryexpress.supplier.discription",
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
                Value = x.ID.ToString()
            }));

            GlAccount = new ControlFormularItemInputComboBox()
            {
                Name = "glaccount",
                Label = "inventoryexpress.ledgeraccount.label",
                Help = "inventoryexpress.ledgeraccount.discription",
                Icon = new PropertyIcon(TypeIcon.At)
            };

            GlAccount.Items.Add(new ControlFormularItemInputComboBoxItem()
            {
                Text = string.Empty,
                Value = null
            });

            GlAccount.Items.AddRange(ViewModel.Instance.GLAccounts.Select(x => new ControlFormularItemInputComboBoxItem()
            {
                Text = x.Name,
                Value = x.ID.ToString()
            }));

            CostCenter = new ControlFormularItemInputComboBox()
            {
                Name = "costcenter",
                Label = "inventoryexpress.costcenter.label",
                Help = "inventoryexpress.costcenter.discription",
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
                Value = x.ID.ToString()
            }));

            Condition = new ControlFormularItemInputComboBox()
            {
                Name = "state",
                Label = "inventoryexpress.condition.label",
                Help = "inventoryexpress.condition.discription",
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
                Value = x.ID.ToString()
            }));

            Parent = new ControlFormularItemInputComboBox()
            {
                Name = "parent",
                Label = "inventoryexpress.parent.label",
                Help = "inventoryexpress.parent.discription",
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
                Value = x.ID.ToString()
            }));

            Template = new ControlFormularItemInputComboBox()
            {
                Name = "template",
                Label = "inventoryexpress.template.label",
                Help = "inventoryexpress.template.discription",
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
                Value = x.ID.ToString()
            }));

            //foreach (var template in ViewModel.Instance.Templates.Where(x => x.ID.ToString() == Template.SelectedValue))
            //{
            //foreach (var attribute in template?.Attributes)
            //{
            //    Attributes.Add(new ControlFormularItemTextBox(this)
            //    {
            //        Name = "attribute_" + attribute.ID,
            //        Label = attribute.Name,
            //        Help = attribute.Discription
            //    });
            //}
            //}

            Tag = new ControlFormularItemInputTextBox()
            {
                Name = "tag",
                Label = "inventoryexpress.tags.label",
                Help = "inventoryexpress.tags.discription",
                Icon = new PropertyIcon(TypeIcon.Tag)
            };

            Memo = new ControlFormularItemInputTextBox()
            {
                Name = "memo",
                Label = "Beschreibung",
                Help = "",
                Format = TypesEditTextFormat.Wysiwyg,
                Icon = new PropertyIcon(TypeIcon.CommentAlt)
            };

            Add(InventoryName);
            Add(Manufactor);
            Add(Location);
            Add(Supplier);
            Add(GlAccount);
            Add(CostCenter);
            Add(Condition);
            Add(Parent);
            Add(Template);
            Add(Attributes.ToArray());
            Add(Tag);
            Add(Memo);

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
    }
}
