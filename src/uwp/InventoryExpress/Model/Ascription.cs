using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Zuschreibung
    /// 
    /// Eine Zuschreibung kann auf ein Inventar erfolgen, um diesen zu Erweitern (z.B. Speichererweiterung)
    /// ohne das die Zuschreibung selbst ím Inventar eigeständig geführt wird
    /// </summary>
    public class Ascription : Item 
    {
        /// <summary>
        /// Liefert oder setzt das übergeordnete Inventar
        /// </summary>
        public Inventory Parent { get; set; }

        private Template template;
        private Template _template;
        private List<AttributeTextValue> attributes;
        private List<AttributeTextValue> _attributes;

        /// <summary>
        /// Anschaffungssdatum
        /// </summary>
        private DateTimeOffset? purchaseDate;
        private DateTimeOffset? _purchaseDate;

        /// <summary>
        /// Abgangsdatum
        /// </summary>
        private DateTimeOffset? derecognitionDate;
        private DateTimeOffset? _derecognitionDate;

        /// <summary>
        /// Der Hersteller
        /// </summary>
        private Manufacturer manufacturer;
        private Manufacturer _manufacturer;

        /// <summary>
        /// Der Zustand
        /// </summary>
        private State state;
        private State _state;

        /// <summary>
        /// Der Lieferant
        /// </summary>
        private Supplier supplier;
        private Supplier _supplier;

        /// <summary>
        /// Der Anschaffungswert
        /// </summary>
        private decimal costvalue;
        private decimal _costvalue;

        /// <summary>
        /// Der Anschaffungswert
        /// </summary>
        public decimal CostValue
        {
            get
            {
                return costvalue;
            }
            set
            {
                if (costvalue != value)
                {
                    costvalue = value;

                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Das Template
        /// </summary>
        public Template Template
        {
            get
            {
                return template;
            }
            set
            {
                if (template != value)
                {
                    template = value;

                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Die Attribute des Kontos
        /// </summary>
        public List<AttributeTextValue> Attributes
        {
            get
            {
                return attributes;
            }
            set
            {
                if (attributes != value)
                {
                    attributes = value;

                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Das Anschaffungsdatum
        /// </summary>
        public DateTimeOffset? PurchaseDate
        {
            get
            {
                return purchaseDate;
            }
            set
            {
                if (purchaseDate != value)
                {
                    purchaseDate = value.Value.Date;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("PurchaseDateString");
                }
            }
        }

        /// <summary>
        /// Das Anschaffungsdatum
        /// </summary>
        public string PurchaseDateString
        {
            get
            {
                return purchaseDate.HasValue ? purchaseDate.Value.ToString("dd.MM.yyyy") : "";
            }
        }

        /// <summary>
        /// Das Abgangsdatum
        /// </summary>
        public DateTimeOffset? DerecognitionDate
        {
            get
            {
                return derecognitionDate;
            }
            set
            {
                if (derecognitionDate != value)
                {
                    derecognitionDate = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("DerecognitionDateString");
                    NotifyPropertyChanged("DerecognitionDateVisible");
                    NotifyPropertyChanged("DerecognitionDateEnable");
                }
            }
        }

        /// <summary>
        /// Das Abgangsdatum
        /// </summary>
        public string DerecognitionDateString
        {
            get
            {
                return derecognitionDate.HasValue ? derecognitionDate.Value.ToString("dd.MM.yyyy") : "";
            }
        }

        /// <summary>
        /// Das Abgangsdatum
        /// </summary>
        public Visibility DerecognitionDateVisible
        {
            get
            {
                return derecognitionDate.HasValue ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Das Abgangsdatum
        /// </summary>
        public bool? DerecognitionDateEnable
        {
            get
            {
                return derecognitionDate.HasValue ? true : false;
            }
            set
            {
                if (DerecognitionDateEnable != value)
                {
                    if (value.HasValue && value.Value)
                    {
                        DerecognitionDate = DateTime.Today;
                    }
                    else
                    {
                        DerecognitionDate = null;
                    }
                }
            }
        }

        /// <summary>
        /// Der Hersteller
        /// </summary>
        public Manufacturer Manufacturer
        {
            get
            {
                return manufacturer;
            }
            set
            {
                if (manufacturer != value)
                {
                    manufacturer = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert die Sichtbarkei des Herstellers
        /// Ist kein Hersteller festgelegt, dann können die entsprechenden Steuerelemente ausgeblendet werden
        /// </summary>
        public Visibility ManufacturerVisibility
        {
            get { return Manufacturer != null && !string.IsNullOrEmpty(Manufacturer.Name) ? Visibility.Visible : Visibility.Collapsed; }
        }

        // <summary>
        /// Der Zustand
        /// </summary>
        public State State
        {
            get
            {
                return state;
            }
            set
            {
                if (state != value)
                {
                    state = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert die Sichtbarkei des Zustandes
        /// Ist kein Zustand festgelegt, dann können die entsprechenden Steuerelemente ausgeblendet werden
        /// </summary>
        public Visibility StateVisibility
        {
            get { return State != null && !string.IsNullOrEmpty(State.Name) ? Visibility.Visible : Visibility.Collapsed; }
        }

        /// <summary>
        /// Der Lieferant
        /// </summary>
        public Supplier Supplier
        {
            get
            {
                return supplier;
            }
            set
            {
                if (supplier != value)
                {
                    supplier = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert die Sichtbarkei des Lieferanten
        /// Ist kein Lieferant festgelegt, dann können die entsprechenden Steuerelemente ausgeblendet werden
        /// </summary>
        public Visibility SupplierVisibility
        {
            get { return Supplier != null && !string.IsNullOrEmpty(Supplier.Name) ? Visibility.Visible : Visibility.Collapsed; }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Ascription()
            :base()
        {
            _attributes = new List<AttributeTextValue>();
            attributes = new List<AttributeTextValue>();

            _purchaseDate = DateTime.Today;
            purchaseDate = DateTime.Today;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="xml">
        ///   Die XML-Struktur, aus dem das 
        ///   Objekt erstellt werden soll
        /// </param>
        public Ascription(XElement xml)
            : base(xml)
        {
            _attributes = new List<AttributeTextValue>();

            var template = (from x in xml.Elements("template")
                            select x.Value.Trim()).FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(template))
            {
                Template = (from x in Model.ViewModel.Instance.Templates
                            where !string.IsNullOrWhiteSpace(x.Name) && x.Name.Equals(template, StringComparison.OrdinalIgnoreCase)
                            select x).FirstOrDefault();
            }

            var attributes = new List<AttributeTextValue>();

            foreach (var v in xml.Elements("attribute"))
            {
                var id = v.Attribute("id") != null ? v.Attribute("id").Value : null;
                var name = v.Attribute("name") != null ? v.Attribute("name").Value : null;
                var value = v.Value;

                if (!string.IsNullOrWhiteSpace(name))
                {
                    attributes.AddRange(from x in Model.ViewModel.Instance.Attributes
                                        where x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)
                                        select new AttributeTextValue()
                                        {
                                            ID = !string.IsNullOrWhiteSpace(id) ? id : Guid.NewGuid().ToString(),
                                            Name = name,
                                            Memo = x.Memo,
                                            Tag = x.Tag,
                                            Value = value
                                        });
                }
            }

            Attributes = attributes.Distinct().ToList();

            var costvalue = (from x in xml.Elements("costvalue")
                             select x.Value.Trim()).FirstOrDefault();

            CostValue = !string.IsNullOrWhiteSpace(costvalue) ? Convert.ToDecimal(costvalue, CultureInfo.InvariantCulture) : 0;

            var receiptDate = (from x in xml.Elements("purchasedate")
                               select x.Value.Trim()).FirstOrDefault();

            PurchaseDate = !string.IsNullOrWhiteSpace(receiptDate) ? Convert.ToDateTime(receiptDate) : (DateTime?)null;

            var derecognitionDate = (from x in xml.Elements("derecognitiondate")
                                     select x.Value.Trim()).FirstOrDefault();

            DerecognitionDate = !string.IsNullOrWhiteSpace(derecognitionDate) ? Convert.ToDateTime(derecognitionDate) : (DateTime?)null;

            var manufacturer = (from x in xml.Elements("manufacturer")
                                select x.Value.Trim()).FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(manufacturer))
            {
                Manufacturer = (from x in Model.ViewModel.Instance.Manufacturers
                                where !string.IsNullOrWhiteSpace(x.Name) && x.Name.Equals(manufacturer, StringComparison.OrdinalIgnoreCase)
                                select x).FirstOrDefault();
            }

            var state = (from x in xml.Elements("state")
                         select x.Value.Trim()).FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(state))
            {
                State = (from x in Model.ViewModel.Instance.States
                         where !string.IsNullOrWhiteSpace(x.Name) && x.Name.Equals(state, StringComparison.OrdinalIgnoreCase)
                         select x).FirstOrDefault();
            }

            var supplier = (from x in xml.Elements("supplier")
                            select x.Value.Trim()).FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(supplier))
            {
                Supplier = (from x in Model.ViewModel.Instance.Suppliers
                            where !string.IsNullOrWhiteSpace(x.Name) && x.Name.Equals(supplier, StringComparison.OrdinalIgnoreCase)
                            select x).FirstOrDefault();
            }
        }

        /// <summary>
        /// Übernimmt die geänderten Daten
        /// </summary>
        /// <param name="durable">true wenn die Daten dauerhaft gespeichert werden sollen</param>
        public override void Commit(bool durable)
        {
            if (template != null)
            {
                // Leere Attribute löschen
                var attributes = (from x in Attributes
                                  where !string.IsNullOrWhiteSpace(x.Value)
                                  select x).ToList();

                Attributes = attributes;
            }

            _template = Template;
            _attributes = Attributes;
            _costvalue = CostValue;
            _purchaseDate = PurchaseDate;
            _derecognitionDate = DerecognitionDate;
            _manufacturer = Manufacturer;
            _state = State;
            _supplier = Supplier;

            base.Commit(durable);
        }

        /// <summary>
        /// Verwirft die geänderten Daten
        /// </summary>
        public override void Rollback()
        {
            base.Rollback();

            Template = _template;
            Attributes = _attributes;
            CostValue = _costvalue;
            PurchaseDate = _purchaseDate;
            DerecognitionDate = _derecognitionDate;
            Manufacturer = _manufacturer;
            State = _state;
            Supplier = _supplier;
        }

        /// <summary>
        /// Wandelt das Objekt in XML um
        /// </summary>
        /// <param name="xml"></param>
        public new void ToXML(XElement xml)
        {
            base.ToXML(xml);

            xml.Add(new XElement("template", Template != null && !string.IsNullOrWhiteSpace(Template.Name) ? Template.Name : null));

            foreach (var v in Attributes)
            {
                xml.Add
                (
                    new XElement
                    (
                        "attribute",
                        new XAttribute("id", v.ID),
                        new XAttribute("name", v.Name),
                        v.Value
                    )
                );
            }

            xml.Add(new XElement("costvalue", CostValue.ToString(CultureInfo.InvariantCulture)));
            xml.Add(new XElement("purchasedate", PurchaseDate));
            xml.Add(new XElement("derecognitiondate", DerecognitionDate));
            xml.Add(new XElement("manufacturer", Manufacturer != null && !string.IsNullOrWhiteSpace(Manufacturer.Name) ? Manufacturer.Name : null));
            xml.Add(new XElement("state", State != null && !string.IsNullOrWhiteSpace(State.Name) ? State.Name : null));
            xml.Add(new XElement("supplier", Supplier != null && !string.IsNullOrWhiteSpace(Supplier.Name) ? Supplier.Name : null));
        }
    }
}
