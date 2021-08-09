using InventoryExpress.Model;
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
        /// Liefert oder setzt den Anschaffungswert
        /// </summary>
        public ControlFormularItemInputTextBox CostValue { get; set; }

        /// <summary>
        /// Liefert oder setzt das Anschaffungsdatum
        /// </summary>
        public ControlFormularItemInputDatepicker PurchaseDate { get; set; }

        /// <summary>
        /// Liefert oder setzt das Abgangsdatum
        /// </summary>
        public ControlFormularItemInputDatepicker DerecognitionDate { get; set; }

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
                Label = "inventoryexpress.inventory.manufacturers.label",
                Help = "inventoryexpress.inventory.manufacturer.description",
                Icon = new PropertyIcon(TypeIcon.Industry)
            };

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

            Location = new ControlFormularItemInputComboBox()
            {
                Name = "location",
                Label = "inventoryexpress.inventory.location.label",
                Help = "inventoryexpress.inventory.location.description",
                Icon = new PropertyIcon(TypeIcon.Map)
            };

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

            Supplier = new ControlFormularItemInputComboBox()
            {
                Name = "supplier",
                Label = "inventoryexpress.inventory.supplier.label",
                Help = "inventoryexpress.inventory.supplier.description",
                Icon = new PropertyIcon(TypeIcon.Truck)
            };

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

            LedgerAccount = new ControlFormularItemInputComboBox()
            {
                Name = "ledgeraccount",
                Label = "inventoryexpress.inventory.ledgeraccount.label",
                Help = "inventoryexpress.inventory.ledgeraccount.description",
                Icon = new PropertyIcon(TypeIcon.At)
            };

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

            CostCenter = new ControlFormularItemInputComboBox()
            {
                Name = "costcenter",
                Label = "inventoryexpress.inventory.costcenter.label",
                Help = "inventoryexpress.inventory.costcenter.description",
                Icon = new PropertyIcon(TypeIcon.ShoppingBag)
            };

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

            Condition = new ControlFormularItemInputComboBox()
            {
                Name = "condition",
                Label = "inventoryexpress.inventory.condition.label",
                Help = "inventoryexpress.inventory.condition.description",
                Icon = new PropertyIcon(TypeIcon.Star)
            };

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

            Parent = new ControlFormularItemInputComboBox()
            {
                Name = "parent",
                Label = "inventoryexpress.inventory.parent.label",
                Help = "inventoryexpress.inventory.parent.description",
                Icon = new PropertyIcon(TypeIcon.Link)
            };

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

            Template = new ControlFormularItemInputComboBox()
            {
                Name = "template",
                Label = "inventoryexpress.inventory.template.label",
                Help = "inventoryexpress.inventory.template.description",
                Icon = new PropertyIcon(TypeIcon.Clone),
                OnChange = new PropertyOnChange(TypeOnChange.Submit)
            };

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

            Attributes = new ControlFormularItemGroupVertical()
            {

            };

            CostValue = new ControlFormularItemInputTextBox()
            {
                Name = "costvalue",
                Label = "inventoryexpress.inventory.costvalue.label",
                Help = "inventoryexpress.inventory.costvalue.description",
                Icon = new PropertyIcon(TypeIcon.EuroSign)
            };

            PurchaseDate = new ControlFormularItemInputDatepicker("purchasedate")
            {
                Name = "purchasedate",
                Label = "inventoryexpress.inventory.purchasedate.label",
                Help = "inventoryexpress.inventory.purchasedate.description",
                Icon = new PropertyIcon(TypeIcon.CalendarPlus)
            };

            DerecognitionDate = new ControlFormularItemInputDatepicker("derecognitiondate")
            {
                Name = "derecognitiondate",
                Label = "inventoryexpress.inventory.derecognitiondate.label",
                Help = "inventoryexpress.inventory.derecognitiondate.description",
                Icon = new PropertyIcon(TypeIcon.CalendarMinus)
            };

            Tag = new ControlFormularItemInputTextBox()
            {
                Name = "tag",
                Label = "inventoryexpress.inventory.tags.label",
                Help = "inventoryexpress.inventory.tags.description",
                Icon = new PropertyIcon(TypeIcon.Tag)
            };

            Description = new ControlFormularItemInputTextBox("note")
            {
                Name = "description",
                Label = "inventoryexpress.inventory.description.label",
                Help = "inventoryexpress.inventory.description.description",
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
            Add(CostValue);
            Add(PurchaseDate);
            Add(DerecognitionDate);
            Add(Tag);
            Add(Description);

        }

        /// <summary>
        /// Vorverarbeitung des Formulars
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        public override void PreProcess(RenderContext context)
        {
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
