using InventoryExpress.Model;
using System.Linq;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebPage;

namespace InventoryExpress.WebControl
{
    public class ControlFormularInventory : ControlFormular
    {
        /// <summary>
        /// Liefert oder setzt den Namen des Inventargegenstandes
        /// </summary>
        public ControlFormularItemInputTextBox InventoryName { get; } = new ControlFormularItemInputTextBox()
        {
            Name = "name",
            Label = "inventoryexpress:inventoryexpress.inventory.name.label",
            Help = "inventoryexpress:inventoryexpress.inventory.name.description",
            Icon = new PropertyIcon(TypeIcon.Font),
            Format = TypesEditTextFormat.Default
        };

        /// <summary>
        /// Liefert oder setzt den Hersteller
        /// </summary>
        public ControlFormularItemInputComboBox Manufacturer { get; } = new ControlFormularItemInputComboBox()
        {
            Name = "manufacturer",
            Label = "inventoryexpress:inventoryexpress.inventory.manufacturers.label",
            Help = "inventoryexpress:inventoryexpress.inventory.manufacturer.description",
            Icon = new PropertyIcon(TypeIcon.Industry)
        };

        /// <summary>
        /// Liefert oder setzt den Standort
        /// </summary>
        public ControlFormularItemInputComboBox Location { get; } = new ControlFormularItemInputComboBox()
        {
            Name = "location",
            Label = "inventoryexpress:inventoryexpress.inventory.location.label",
            Help = "inventoryexpress:inventoryexpress.inventory.location.description",
            Icon = new PropertyIcon(TypeIcon.Map)
        };

        /// <summary>
        /// Liefert oder setzt den Lieferanten
        /// </summary>
        public ControlFormularItemInputComboBox Supplier { get; } = new ControlFormularItemInputComboBox()
        {
            Name = "supplier",
            Label = "inventoryexpress:inventoryexpress.inventory.supplier.label",
            Help = "inventoryexpress:inventoryexpress.inventory.supplier.description",
            Icon = new PropertyIcon(TypeIcon.Truck)
        };

        /// <summary>
        /// Liefert oder setzt das Sachkonto
        /// </summary>
        public ControlFormularItemInputComboBox LedgerAccount { get; } = new ControlFormularItemInputComboBox()
        {
            Name = "ledgeraccount",
            Label = "inventoryexpress:inventoryexpress.inventory.ledgeraccount.label",
            Help = "inventoryexpress:inventoryexpress.inventory.ledgeraccount.description",
            Icon = new PropertyIcon(TypeIcon.At)
        };

        /// <summary>
        /// Liefert oder setzt die Kostenstelle
        /// </summary>
        public ControlFormularItemInputComboBox CostCenter { get; } = new ControlFormularItemInputComboBox()
        {
            Name = "costcenter",
            Label = "inventoryexpress:inventoryexpress.inventory.costcenter.label",
            Help = "inventoryexpress:inventoryexpress.inventory.costcenter.description",
            Icon = new PropertyIcon(TypeIcon.ShoppingBag)
        };

        /// <summary>
        /// Liefert oder setzt den Zustand
        /// </summary>
        public ControlFormularItemInputComboBox Condition { get; } = new ControlFormularItemInputComboBox()
        {
            Name = "condition",
            Label = "inventoryexpress:inventoryexpress.inventory.condition.label",
            Help = "inventoryexpress:inventoryexpress.inventory.condition.description",
            Icon = new PropertyIcon(TypeIcon.Star)
        };

        /// <summary>
        /// Liefert oder setzt die Zugehörigkeit
        /// </summary>
        public ControlFormularItemInputComboBox Parent { get; } = new ControlFormularItemInputComboBox()
        {
            Name = "parent",
            Label = "inventoryexpress:inventoryexpress.inventory.parent.label",
            Help = "inventoryexpress:inventoryexpress.inventory.parent.description",
            Icon = new PropertyIcon(TypeIcon.Link)
        };

        /// <summary>
        /// Liefert oder setzt das Template
        /// </summary>
        public ControlFormularItemInputComboBox Template { get; } = new ControlFormularItemInputComboBox()
        {
            Name = "template",
            Label = "inventoryexpress:inventoryexpress.inventory.template.label",
            Help = "inventoryexpress:inventoryexpress.inventory.template.description",
            Icon = new PropertyIcon(TypeIcon.Clone),
            OnChange = new PropertyOnChange(TypeOnChange.Submit)
        };

        /// <summary>
        /// Liefert oder setzt die Attribute
        /// </summary>
        public ControlFormularItemGroupVertical Attributes { get; } = new ControlFormularItemGroupVertical();

        /// <summary>
        /// Liefert oder setzt den Anschaffungswert
        /// </summary>
        public ControlFormularItemInputTextBox CostValue { get; } = new ControlFormularItemInputTextBox()
        {
            Name = "costvalue",
            Label = "inventoryexpress:inventoryexpress.inventory.costvalue.label",
            Help = "inventoryexpress:inventoryexpress.inventory.costvalue.description",
            Icon = new PropertyIcon(TypeIcon.EuroSign)
        };

        /// <summary>
        /// Liefert oder setzt das Anschaffungsdatum
        /// </summary>
        public ControlFormularItemInputDatepicker PurchaseDate { get; } = new ControlFormularItemInputDatepicker("purchasedate")
        {
            Name = "purchasedate",
            Label = "inventoryexpress:inventoryexpress.inventory.purchasedate.label",
            Help = "inventoryexpress:inventoryexpress.inventory.purchasedate.description",
            Icon = new PropertyIcon(TypeIcon.CalendarPlus)
        };

        /// <summary>
        /// Liefert oder setzt das Abgangsdatum
        /// </summary>
        public ControlFormularItemInputDatepicker DerecognitionDate { get; } = new ControlFormularItemInputDatepicker("derecognitiondate")
        {
            Name = "derecognitiondate",
            Label = "inventoryexpress:inventoryexpress.inventory.derecognitiondate.label",
            Help = "inventoryexpress:inventoryexpress.inventory.derecognitiondate.description",
            Icon = new PropertyIcon(TypeIcon.CalendarMinus)
        };

        /// <summary>
        /// Liefert oder setzt die Schlagwörter
        /// </summary>
        public ControlFormularItemInputTag Tag { get; } = new ControlFormularItemInputTag("tags")
        {
            Name = "tag",
            Label = "inventoryexpress:inventoryexpress.inventory.tags.label",
            Help = "inventoryexpress:inventoryexpress.inventory.tags.description",
            Icon = new PropertyIcon(TypeIcon.Tag)
        };

        /// <summary>
        /// Liefert oder setzt die Beschreibung
        /// </summary>
        public ControlFormularItemInputTextBox Description { get; } = new ControlFormularItemInputTextBox("note")
        {
            Name = "description",
            Label = "inventoryexpress:inventoryexpress.inventory.description.label",
            Help = "inventoryexpress:inventoryexpress.inventory.description.description",
            Format = TypesEditTextFormat.Wysiwyg,
            Icon = new PropertyIcon(TypeIcon.CommentAlt),
            Rows = 10,
            AutoInitialize = true
        };

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
            Name = "inventory";
            EnableCancelButton = false;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Three, PropertySpacing.Space.None, PropertySpacing.Space.None);
            BackgroundColor = LayoutSchema.FormularBackground;
            Border = new PropertyBorder(true);
            Padding = new PropertySpacingPadding(PropertySpacing.Space.Two);
            Layout = TypeLayoutFormular.Horizontal;

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
            Add(CostValue);
            Add(PurchaseDate);
            Add(DerecognitionDate);
            Add(Tag);
            Add(Description);
        }

        /// <summary>
        /// Initialisiert das Formular
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        public override void Initialize(RenderContextFormular context)
        {
            base.Initialize(context);

            Manufacturer.Items.Add(new ControlFormularItemInputComboBoxItem()
            {
                Text = string.Empty,
                Value = null
            });

            lock (ViewModel.Instance.Database)
            {
                Manufacturer.Items.AddRange(ViewModel.Instance.Manufacturers.OrderBy(x => x.Name).Select(x => new ControlFormularItemInputComboBoxItem()
                {
                    Text = x.Name,
                    Value = x.Guid
                }));
            }

            Location.Items.Add(new ControlFormularItemInputComboBoxItem()
            {
                Text = string.Empty,
                Value = null
            });

            lock (ViewModel.Instance.Database)
            {
                Location.Items.AddRange(ViewModel.Instance.Locations.OrderBy(x => x.Name).Select(x => new ControlFormularItemInputComboBoxItem()
                {
                    Text = x.Name,
                    Value = x.Guid
                }));
            }

            Supplier.Items.Add(new ControlFormularItemInputComboBoxItem()
            {
                Text = string.Empty,
                Value = null
            });

            lock (ViewModel.Instance.Database)
            {
                Supplier.Items.AddRange(ViewModel.Instance.Suppliers.OrderBy(x => x.Name).Select(x => new ControlFormularItemInputComboBoxItem()
                {
                    Text = x.Name,
                    Value = x.Guid
                }));
            }

            LedgerAccount.Items.Add(new ControlFormularItemInputComboBoxItem()
            {
                Text = string.Empty,
                Value = null
            });

            lock (ViewModel.Instance.Database)
            {
                LedgerAccount.Items.AddRange(ViewModel.Instance.LedgerAccounts.OrderBy(x => x.Name).Select(x => new ControlFormularItemInputComboBoxItem()
                {
                    Text = x.Name,
                    Value = x.Guid
                }));
            }

            CostCenter.Items.Add(new ControlFormularItemInputComboBoxItem()
            {
                Text = string.Empty,
                Value = null
            });

            lock (ViewModel.Instance.Database)
            {
                CostCenter.Items.AddRange(ViewModel.Instance.CostCenters.OrderBy(x => x.Name).Select(x => new ControlFormularItemInputComboBoxItem()
                {
                    Text = x.Name,
                    Value = x.Guid
                }));
            }

            Condition.Items.Add(new ControlFormularItemInputComboBoxItem()
            {
                Text = string.Empty,
                Value = null
            });

            lock (ViewModel.Instance.Database)
            {
                Condition.Items.AddRange(ViewModel.Instance.Conditions.OrderBy(x => x.Grade).Select(x => new ControlFormularItemInputComboBoxItem()
                {
                    Text = string.Format("{0} - {1}", x.Grade, x.Name),
                    Value = x.Guid
                }));
            }

            Parent.Items.Add(new ControlFormularItemInputComboBoxItem()
            {
                Text = string.Empty,
                Value = null
            });

            lock (ViewModel.Instance.Database)
            {
                Parent.Items.AddRange(ViewModel.Instance.Inventories.OrderBy(x => x.Name).Select(x => new ControlFormularItemInputComboBoxItem()
                {
                    Text = x.Name,
                    Value = x.Guid
                }));
            }

            Template.Items.Add(new ControlFormularItemInputComboBoxItem()
            {
                Text = string.Empty,
                Value = null
            });

            lock (ViewModel.Instance.Database)
            {
                Template.Items.AddRange(ViewModel.Instance.Templates.OrderBy(x => x.Name).Select(x => new ControlFormularItemInputComboBoxItem()
                {
                    Text = x.Name,
                    Value = x.Guid
                }));
            }
        }
    }
}
